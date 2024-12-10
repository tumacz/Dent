using Moq;
using TheApp.Application.DataTransferObjects.Commands.CreateDentalStudio;
using TheApp.Domain.Interfaces;
using Xunit;
using FluentValidation.TestHelper;

namespace TheApp.ApplicationTests.DentalStudioDTO.Commands.CreateDentalStudio
{
    public class CreateDentalStudioCommandValidatorTests
    {
        [Fact]
        public void Validate_WithValidCommand_ShouldNotHaveValidationError()
        {
            //arrange
            var dentalStudio = new CreateDentalStudioCommand()
            {
                Name = "Test Name",
                Description = "Test Descryption",
                PhoneNumber = "1234567890"
            };

            var mockRepositoty = new Mock<IDentalStudioRepository>();

            var validator = new CreateDentalStudioCommandValidator(mockRepositoty.Object);

            //act
            var result = validator.TestValidate(dentalStudio);

            //assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void Validate_WithInValidCommand_ShouldHaveValidationError()
        {
            //arrange
            var dentalStudio = new CreateDentalStudioCommand()
            {
                Name = "a",
                Description = "",
                PhoneNumber = "12"
            };

            var mockRepositoty = new Mock<IDentalStudioRepository>();

            var validator = new CreateDentalStudioCommandValidator(mockRepositoty.Object);

            //act
            var result = validator.TestValidate(dentalStudio);

            //assert
            result.ShouldHaveValidationErrorFor(c => c.Name);
            result.ShouldHaveValidationErrorFor(c => c.Description);
            result.ShouldHaveValidationErrorFor(c => c.PhoneNumber);
        }
    }
}
