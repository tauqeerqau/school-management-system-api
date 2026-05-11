using AutoMapper;
using FluentAssertions;
using Moq;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.Features.Students.Commands.CreateStudent;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Tests.Handlers
{
    public class CreateStudentCommandHandlerTests
    {
        private readonly Mock<IStudentRepository> _studentRepositoryMock;

        private readonly Mock<IMapper> _mapperMock;
        
        private readonly Mock<ICacheService> _cacheServiceMock;

        private readonly CreateStudentCommandHandler _handler;

        public CreateStudentCommandHandlerTests()
        {
            _studentRepositoryMock = new Mock<IStudentRepository>();

            _mapperMock = new Mock<IMapper>();

            _cacheServiceMock = new Mock<ICacheService>();

            _handler =
                new CreateStudentCommandHandler(
                    _studentRepositoryMock.Object,
                    _mapperMock.Object,
                    _cacheServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateStudentSuccessfully()
        {
            // Arrange

            var dto = new CreateStudentDto
            {
                FirstName = "Ali",
                LastName = "Khan",
                Email = "ali@test.com",
                Gender = "Male"
            };

            var command =
                new CreateStudentCommand(dto);

            var student = new Student
            {
                Id = 1,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };

            var studentDto = new StudentDto
            {
                Id = 1,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };

            _mapperMock
                .Setup(x =>
                    x.Map<Student>(dto))
                .Returns(student);

            _mapperMock
                .Setup(x =>
                    x.Map<StudentDto>(student))
                .Returns(studentDto);

            _studentRepositoryMock
                .Setup(x =>
                    x.AddAsync(student))
                .ReturnsAsync(student);

            // Act

            var result =
                await _handler.Handle(
                    command,
                    CancellationToken.None);

            // Assert

            result.Should().NotBeNull();

            result.Success.Should().BeTrue();

            result.Data.Should().NotBeNull();

            result.Data.FirstName.Should().Be("Ali");

            _studentRepositoryMock.Verify(
                x => x.AddAsync(student),
                Times.Once);
        }
    }
}
