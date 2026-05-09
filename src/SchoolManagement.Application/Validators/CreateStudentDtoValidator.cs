using FluentValidation;
using SchoolManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Validators
{
    public class CreateStudentDtoValidator : AbstractValidator<CreateStudentDto>
    {
        public CreateStudentDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Gender)
                .NotEmpty();

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Today);
        }
    }
}
