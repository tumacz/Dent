using Xunit;
using TheApp.Application.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheApp.Application.ApplicationUser;
using Moq;
using TheApp.Application.DataTransferObjects;
using AutoMapper;
using TheApp.Domain.Entities;
using FluentAssertions;

namespace TheApp.Application.Mappings.Tests
{
    public class DentalStudioMappingProfileTests
    {
        [Fact()]
        public void MapingProfile_ShouldMapDentalStudioDtoToDentalStudio()
        {
            //arrange
            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "User" }));

            var configuration = new MapperConfiguration(cfg => 
                cfg.AddProfile(new DentalStudioMappingProfile(userContextMock.Object)));

            var mapper = configuration.CreateMapper();

            var dto = new DentalStudioDTO()
            {
                City = "1",
                PhoneNumber = "2",
                PostalCode = "3",
                Street = "4",
                Link = "5",
            };

            //act
            var result = mapper.Map<DentalStudio>(dto);
            
            //assert
            result.Should().NotBeNull();
            result.ContactDetails.City.Should().Be(dto.City);
            result.ContactDetails.PhoneNumber.Should().Be(dto.PhoneNumber);
            result.ContactDetails.PostalCode.Should().Be(dto.PostalCode);
            result.ContactDetails.Street.Should().Be(dto.Street);
            result.ContactDetails.Link.Should().Be(dto.Link);
        }

        [Fact()]
        public void MappingProfile_ShouldMapDentalStudioToDentalStudioDto()
        {
            //arrange
            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "User" }));

            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile(new DentalStudioMappingProfile(userContextMock.Object)));

            var mapper = configuration.CreateMapper();

            var dentalStudio = new DentalStudio()
            {
                Id = 1,
                CreatedById = "1",
                ContactDetails = new DentalStudioContactDetails()
                {
                    City = "City",
                    PhoneNumber = "12345",
                    PostalCode = "12",
                    Street = "Street",
                    Link = "Link"
                }
            };

            //act
            var result = mapper.Map<DentalStudioDTO>(dentalStudio);

            //assert
            result.City.Should().Be(dentalStudio.ContactDetails.City);
            result.PhoneNumber.Should().Be(dentalStudio.ContactDetails.PhoneNumber);
            result.PostalCode.Should().Be(dentalStudio.ContactDetails.PostalCode);
            result.Street.Should().Be(dentalStudio.ContactDetails.Street);
            result.Link.Should().Be(dentalStudio.ContactDetails.Link);
            result.IsEditable.Should().BeTrue();
        }
    }
}