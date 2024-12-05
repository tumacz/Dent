using Xunit;
using TheApp.Application.DentalStudioServiceDTO.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace TheApp.Application.DentalStudioServiceDTO.Commands.Tests
{
    public class CreateDentalStudioServiceCommandValidatorTests
    {
        [Fact()]
        public void Validate_WithValidCommand_ShouldNotHaveValidationError()
        {
            //arange
            var validator = new CreateDentalStudioServiceCommandValidator();
            var command = new CreateDentalStudioServiceCommand()
            {
                Cost = "10 PLN",
                Description = "Description",
                DentalStudioEncodedName = "name"
            };
            //act
            var result = validator.TestValidate(command);
            //assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        public void Validate_WithInvalidCommand_ShouldHaveValidationError()
        {
            //arange
            var validator = new CreateDentalStudioServiceCommandValidator();
            var command = new CreateDentalStudioServiceCommand()
            {
                Cost = "",
                Description = null,
                DentalStudioEncodedName = ""
            };
            //act
            var result = validator.TestValidate(command);
            //assert
            result.ShouldHaveValidationErrorFor(c => c.Cost);
            result.ShouldHaveValidationErrorFor(c => c.DentalStudioEncodedName);
            result.ShouldHaveValidationErrorFor(c => c.Description);
        }
    }
}