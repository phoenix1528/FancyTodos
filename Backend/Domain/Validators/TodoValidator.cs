using FluentValidation;

namespace Domain.Validators
{
    public class TodoValidator : AbstractValidator<Todo>
    {
        public TodoValidator()
        {
            RuleFor(todo => todo.Title).NotEmpty();
            RuleFor(todo => todo.Description).NotEmpty();
            RuleFor(todo => todo.Category).NotEmpty();
            RuleFor(todo => todo.City).NotEmpty();
            RuleFor(todo => todo.Venue).NotEmpty();
        }
    }
}
