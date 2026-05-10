using FluentValidation;
using SchoolManagement.Application.Features.Students.Commands.CreateStudent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Validators
{
    public class CreateStudentCommandValidator
        : AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentCommandValidator()
        {
            RuleFor(x => x.Student.FirstName)
                .NotEmpty()
                .WithMessage("First name is required");

            RuleFor(x => x.Student.LastName)
                .NotEmpty()
                .WithMessage("Last name is required");

            RuleFor(x => x.Student.Email)
                .EmailAddress()
                .WithMessage("Invalid email address");
        }
    }
}
