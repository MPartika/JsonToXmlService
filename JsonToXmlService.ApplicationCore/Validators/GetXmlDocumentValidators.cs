using FluentValidation;
using JsonToXmlService.ApplicationCore.Queries;

namespace JsonToXmlService.ApplicationCore.Validators;

public class GetXmlDocumentValidators : AbstractValidator<GetXmlDocumentQuery>
{
    public GetXmlDocumentValidators()
    {
        RuleFor(query => query.DocumentId)
            .GreaterThanOrEqualTo(1).WithMessage("This is not a valid Document Id.");
    }
}