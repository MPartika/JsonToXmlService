using FluentValidation;
using FluentValidation.Results;
using JsonToXmlService.ApplicationCore.Decorators;
using MediatR;
using Moq;

namespace JsonToXmlService.Test;

public class TestValidationBehaviors
{
    private Mock<IValidator<IRequest>> _mockValidator;
     private Mock<IRequest> _mockRequest;
     private Mock<RequestHandlerDelegate<object>> _mockNext;
    private ValidationBehavior<IRequest, object> _validationBehavior;


    [SetUp]
    public void Init()
    {
        _mockValidator = new Mock<IValidator<IRequest>>();
        _mockNext = new Mock<RequestHandlerDelegate<object>>();
        _mockRequest = new Mock<IRequest>();
        _validationBehavior = new ValidationBehavior<IRequest, object>([_mockValidator.Object]);
    }

    [Test]
    public async Task ShouldCallValidator()
    {
        
        _mockNext.Setup(x => x()).Returns(Task.FromResult(new object()));
        _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<IRequest>>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());
        await _validationBehavior.Handle(_mockRequest.Object, _mockNext.Object, CancellationToken.None);
        _mockNext.Verify(x => x(), Times.Once);
        _mockValidator.Verify(x => x.ValidateAsync(It.IsAny<ValidationContext<IRequest>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void ShouldThrowValidationException()
    {
        _mockValidator
            .Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<IRequest>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult {Errors = [new() { ErrorMessage = "Test"}]});
        Assert.ThrowsAsync<ValidationException>(async () => await _validationBehavior.Handle(_mockRequest.Object, _mockNext.Object, CancellationToken.None));
    }
}