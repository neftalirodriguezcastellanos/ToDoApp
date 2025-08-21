using FluentValidation;

namespace ToDoList.Application.Tasks.Create
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(x => x.Title)
            .NotEmpty().WithMessage("El título es requerido.")
            .MaximumLength(100).WithMessage("El título no debe exceder los 100 caractéres.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("La descripción no debe exceder los 500 caractéres.");

            RuleFor(x => x.DueDate)
                .GreaterThanOrEqualTo(DateTime.Today)
                .When(x => x.DueDate.HasValue)
                .WithMessage("La fecha de vencimiento no debe ser menor a la fecha actual.");
        }
    }
}