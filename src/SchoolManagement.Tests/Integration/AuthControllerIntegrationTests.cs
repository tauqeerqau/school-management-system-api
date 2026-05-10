using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace SchoolManagement.Tests.Integration
{
    public class AuthControllerIntegrationTests
        : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public AuthControllerIntegrationTests(
            WebApplicationFactory<Program> factory)
        {
            _client = factory
                .WithWebHostBuilder(builder => { })
                .CreateClient();
        }

        [Fact]
        public async Task Register_ShouldReturnSuccess()
        {
            // Arrange

            var request = new
            {
                Username = "testuser",
                Email = $"{Guid.NewGuid()}@test.com",
                Password = "Password123"
            };

            // Act

            var response = await _client.PostAsJsonAsync(
                "/api/auth/register",
                request);

            // Debug Output

            var responseContent =
                await response.Content.ReadAsStringAsync();

            Console.WriteLine("Status Code:");
            Console.WriteLine(response.StatusCode);

            Console.WriteLine("Response Content:");
            Console.WriteLine(responseContent);

            // Assert

            response.StatusCode
                .Should()
                .Be(HttpStatusCode.OK);

            response.IsSuccessStatusCode
                .Should()
                .BeTrue();
        }
    }
}