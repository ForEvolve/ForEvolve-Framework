using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;

namespace ForEvolve.XUnit.Identity
{
    public class UserManagerDependencies<TUser>
        where TUser : class
    {
        public Mock<IOptions<IdentityOptions>> OptionsAccessorMock { get; }
        public Mock<IUserStore<TUser>> StoreMock { get; }
        public Mock<IPasswordHasher<TUser>> PasswordHasherMock { get; }
        public List<IUserValidator<TUser>> UserValidators { get; }
        public List<IPasswordValidator<TUser>> PasswordValidators { get; }
        public Mock<ILookupNormalizer> KeyNormalizerMock { get; }
        public Mock<IdentityErrorDescriber> ErrorsMock { get; }
        public Mock<IServiceProvider> ServicesMock { get; }
        public Mock<ILogger<UserManager<TUser>>> UserManagerLoggerMock { get; }

        public UserManagerDependencies()
        {
            OptionsAccessorMock = new Mock<IOptions<IdentityOptions>>();
            StoreMock = new Mock<IUserStore<TUser>>();
            PasswordHasherMock = new Mock<IPasswordHasher<TUser>>();
            UserValidators = new List<IUserValidator<TUser>>();
            PasswordValidators = new List<IPasswordValidator<TUser>>();
            KeyNormalizerMock = new Mock<ILookupNormalizer>();
            ErrorsMock = new Mock<IdentityErrorDescriber>();
            ServicesMock = new Mock<IServiceProvider>();
            UserManagerLoggerMock = new Mock<ILogger<UserManager<TUser>>>();
        }
    }
}
