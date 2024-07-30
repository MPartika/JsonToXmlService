using FluentValidation;
using JsonToXmlService.ApplicationCore.Commands;

namespace JsonToXmlService.ApplicationCore.Validators;

public class ChangeDocumentValidator : AbstractValidator<ChangeDocumentCommand>
{
    public ChangeDocumentValidator()
    {
        RuleFor(query => query.DocumentId)
            .GreaterThanOrEqualTo(1).WithMessage("This is not a valid Document Id.");
        RuleFor(command => command.Tags)
            .NotEmpty().WithMessage("Every document must have at least one tag");
        RuleFor(command => command.Data.Name)
            .NotEmpty().WithMessage("Document must have a name");
        RuleFor(command => command.Data.Author)
            .NotEmpty().WithMessage("Document must have an Author");
    }
}