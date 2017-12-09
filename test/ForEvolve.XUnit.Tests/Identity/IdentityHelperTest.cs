using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.XUnit.Identity
{
    public class IdentityHelperTest
    {
        [Fact]
        public void Should_be_instantiable()
        {
            var helper = new IdentityHelper<FakeUser>();
        }

        public class SignInManagerMock
        {
            public class SignInAsync
            {
                [Fact]
                public async Task Should_be_callable()
                {
                    // Arrange
                    var helper = new IdentityHelper<FakeUser>();
                    var user = new FakeUser();
                    bool isPersistent = false;
                    string authenticationMethod = "SomeMethod";
                    var authenticationProperties = new AuthenticationProperties();

                    // Act & Assert that the mock is well configured
                    await helper.SignInManagerMock.Object.SignInAsync(user, isPersistent);
                    await helper.SignInManagerMock.Object.SignInAsync(user, isPersistent, authenticationMethod);
                    await helper.SignInManagerMock.Object.SignInAsync(user, authenticationProperties);
                    await helper.SignInManagerMock.Object.SignInAsync(user, authenticationProperties, authenticationMethod);
                }
            }

            public class PasswordSignInAsync
            {
                [Fact]
                public async Task Should_be_callable()
                {
                    // Arrange
                    var helper = new IdentityHelper<FakeUser>();
                    var user = new FakeUser();
                    var userName = "";
                    var password = "";
                    var isPersistent = false;
                    var lockoutOnFailure = false;


                    // Act & Assert that the mock is well configured
                    var result1 = await helper.SignInManagerMock.Object.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
                    var result2 = await helper.SignInManagerMock.Object.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);

                    // Assert that the results are SignInManagerMockSettings.PasswordSignInAsyncResult
                    Assert.Same(result1, helper.SignInManagerMockSettings.PasswordSignInAsyncResult.Result);
                    Assert.Same(result2, helper.SignInManagerMockSettings.PasswordSignInAsyncResult.Result);
                }
            }
        }

        public class UserManagerMock
        {
            public class CreateAsync
            {
                [Fact]
                public async Task Should_be_callable()
                {
                    // Arrange
                    var helper = new IdentityHelper<FakeUser>();
                    var user = new FakeUser();
                    var password = "SomePassword";

                    // Act & Assert that the mock is well configured
                    var result1 = await helper.UserManagerMock.Object.CreateAsync(user);
                    var result2 = await helper.UserManagerMock.Object.CreateAsync(user, password);

                    // Assert that the results are UserManagerMockSettings.CreateAsyncResult
                    Assert.Same(result1, helper.UserManagerMockSettings.CreateAsyncResult);
                    Assert.Same(result2, helper.UserManagerMockSettings.CreateAsyncResult);
                }
            }
        }
    }
}
