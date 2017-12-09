using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Security.Claims;

namespace ForEvolve.XUnit.Identity
{
    public class SignInManagerDependencies<TUser>
        where TUser : class
    {
        public Mock<IHttpContextAccessor> HttpContextAccessorMock { get; }
        public Mock<HttpContext> HttpContentMock { get; }
        public Mock<IUserClaimsPrincipalFactory<TUser>> ClaimsFactoryMock { get; }
        public Mock<ILogger<SignInManager<TUser>>> SignInManagerLoggerMock { get; }
        public Mock<IAuthenticationSchemeProvider> SchemesMock { get; }
        public Mock<IOptions<IdentityOptions>> OptionsAccessorMock { get; }

        public Mock<IAuthenticationService> AuthenticationServiceMock { get; }

        //public Mock<ClaimsPrincipal> ClaimsPrincipalMock { get; set; }

        public SignInManagerDependencies()
        {
            //
            // TODO: clean this up
            // There is probably multiple useless mock
            //
            var claimPrincipal = new ClaimsPrincipal();
            ClaimsFactoryMock = new Mock<IUserClaimsPrincipalFactory<TUser>>();
            ClaimsFactoryMock
                .Setup(x => x.CreateAsync(It.IsAny<TUser>()))
                .ReturnsAsync(claimPrincipal);
            SignInManagerLoggerMock = new Mock<ILogger<SignInManager<TUser>>>();
            SchemesMock = new Mock<IAuthenticationSchemeProvider>();
            OptionsAccessorMock = new Mock<IOptions<IdentityOptions>>();

            // Create the IHttpContextAccessor mock & friends
            var serviceProviderMock = new Mock<IServiceProvider>();

            HttpContentMock = new Mock<HttpContext>()
                .SetupAllProperties();
            HttpContentMock
                .Setup(x => x.RequestServices)
                .Returns(serviceProviderMock.Object);


            AuthenticationServiceMock = new Mock<IAuthenticationService>()
                .SetupAllProperties();
            AuthenticationServiceMock
                .Setup(x => x.SignInAsync(
                    HttpContentMock.Object, 
                    It.IsAny<string>(), 
                    claimPrincipal, 
                    It.IsAny<AuthenticationProperties>()
                ));

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IAuthenticationService)))
                .Returns(AuthenticationServiceMock.Object);

            HttpContextAccessorMock = new Mock<IHttpContextAccessor>();
            HttpContextAccessorMock
                .Setup(x => x.HttpContext)
                .Returns(HttpContentMock.Object);

            // Resources
            // AuthenticationHttpContextExtensions: https://github.com/aspnet/HttpAbstractions/blob/271faf11bbd5b05cd758f1c7e83eb59d45b6db59/src/Microsoft.AspNetCore.Authentication.Abstractions/AuthenticationHttpContextExtensions.cs
            // SignInManager: https://github.com/aspnet/Identity/blob/eb3ff7fc32dbfff65a1ba6dfdca16487e0f6fc41/src/Microsoft.AspNetCore.Identity/SignInManager.cs
        }
    }
}
