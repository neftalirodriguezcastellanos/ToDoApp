using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace ToDoList.Application.Users.Update
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage("User ID is required.")
                .Must(guid => guid != Guid.Empty)
                .WithMessage("User ID must not be an empty GUID.");

            RuleFor(c => c.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Email must be a valid email address.")
                .MaximumLength(255)
                .WithMessage("Email must not exceed 255 characters.");

            RuleFor(c => c.FullName)
                .NotEmpty()
                .WithMessage("Full name is required.")
                .MaximumLength(100)
                .WithMessage("Full name must not exceed 100 characters.");

            RuleFor(c => c.PasswordHash)
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters long.")
                .When(c => !string.IsNullOrEmpty(c.PasswordHash));
        }
    }
}