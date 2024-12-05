using Xunit;
using TheApp.Application.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheApp.Application.ApplicationUser.Tests
{
    public class CurrentUserTests
    {
        [Fact()]
        public void IsInRole_WitchMatchingRole_ShouldReturnTrue()
        {
            //arange
            var currentUser = new CurrentUser("id", "user@email.com", ["admin", "owner"]);
            //act
            var result = currentUser.IsInRole("admin");
            //assert
            Xunit.Assert.True(result);
        }

        [Fact()]
        public void IsInRole_WitchNonMatchingRole_ShouldReturnFalse()
        {
            //arange
            var currentUser = new CurrentUser("id", "user@email.com", ["admin", "owner"]);
            //act
            var result = currentUser.IsInRole("moderator");
            //assert
            Xunit.Assert.False(result);
        }

        [Fact()]
        public void IsInRole_WitchNonMatchingCaseRole_ShouldReturnFalse()
        {
            //arange
            var currentUser = new CurrentUser("id", "user@email.com", ["Admin", "Owner"]);
            //act
            var result = currentUser.IsInRole("admin");
            //assert
            Xunit.Assert.False(result);
        }
    }
}