using Xunit;
using FluentAssertions;

namespace TheApp.Domain.Entities.Tests
{
    public class DentalStudioTests
    {
        [Fact()]
        public void EncodeName_ShouldSetEncodedName()
        {
            // arrange 
            var dentalStudio = new DentalStudio();
            dentalStudio.Name = "Test Studio";
            // act
            dentalStudio.EncodeName();
            // assert
            dentalStudio.EncodedName.Should().Be("test-studio");
        }
        [Fact]
        public void EncodeName_ShouldThrowException_WhenNameIsNull()
        {
            // arrange 
            var dentalStudio = new DentalStudio();
            // act
            Action action = () => dentalStudio.EncodeName();
            // arrange 
            action.Invoking(a => a.Invoke())
                .Should().Throw<NullReferenceException>();
        }
    }
}