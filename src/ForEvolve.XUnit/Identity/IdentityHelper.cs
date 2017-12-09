using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.XUnit.Identity
{
    public class IdentityHelper<TUser>
        where TUser : class
    {
        public Mock<UserManager<TUser>> UserManagerMock { get; }
        public Mock<SignInManager<TUser>> SignInManagerMock { get; }

        private UserManagerDependencies<TUser> UserManagerDependencies { get; }
        private SignInManagerDependencies<TUser> SignInManagerDependencies { get; }

        public UserManagerMockSettings UserManagerMockSettings { get; }
        public SignInManagerMockSettings SignInManagerMockSettings { get; }

        public IdentityHelper(MockBehavior mockBehavior = MockBehavior.Default)
        {
            // Settigns
            UserManagerMockSettings = new UserManagerMockSettings();
            SignInManagerMockSettings = new SignInManagerMockSettings();

            // Create dependencies
            UserManagerDependencies = new UserManagerDependencies<TUser>();
            SignInManagerDependencies = new SignInManagerDependencies<TUser>();

            // Create the UserManager
            UserManagerMock = new Mock<UserManager<TUser>>(
                mockBehavior,
                UserManagerDependencies.StoreMock.Object,
                UserManagerDependencies.OptionsAccessorMock.Object,
                UserManagerDependencies.PasswordHasherMock.Object,
                UserManagerDependencies.UserValidators,
                UserManagerDependencies.PasswordValidators,
                UserManagerDependencies.KeyNormalizerMock.Object,
                UserManagerDependencies.ErrorsMock.Object,
                UserManagerDependencies.ServicesMock.Object,
                UserManagerDependencies.UserManagerLoggerMock.Object
            );
            UserManagerMock.SetupAllProperties();
            UserManagerMock
                .Setup(x => x.CreateAsync(It.IsAny<TUser>()))
                .ReturnsAsync(() => UserManagerMockSettings.CreateAsyncResult)
                .Verifiable();
            UserManagerMock
                .Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>()))
                .ReturnsAsync(() => UserManagerMockSettings.CreateAsyncResult)
                .Verifiable();

            // Create the SignInManager
            SignInManagerMock = new Mock<SignInManager<TUser>>(
                mockBehavior,
                UserManagerMock.Object,
                SignInManagerDependencies.HttpContextAccessorMock.Object,
                SignInManagerDependencies.ClaimsFactoryMock.Object,
                SignInManagerDependencies.OptionsAccessorMock.Object,
                SignInManagerDependencies.SignInManagerLoggerMock.Object,
                SignInManagerDependencies.SchemesMock.Object
            );
            // SignInAsync
            SignInManagerMock
                .Setup(x => x.SignInAsync(It.IsAny<TUser>(), It.IsAny<bool>(), It.IsAny<string>()))
                .Returns(() => SignInManagerMockSettings.SignInAsyncResult)
                .Verifiable();
            SignInManagerMock
                .Setup(x => x.SignInAsync(It.IsAny<TUser>(), It.IsAny<AuthenticationProperties>(), It.IsAny<string>()))
                .Returns(() => SignInManagerMockSettings.SignInAsyncResult)
                .Verifiable();

            // PasswordSignInAsync
            SignInManagerMock
                .Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(() => SignInManagerMockSettings.PasswordSignInAsyncResult)
                .Verifiable();
            SignInManagerMock
                .Setup(x => x.PasswordSignInAsync(It.IsAny<TUser>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(() => SignInManagerMockSettings.PasswordSignInAsyncResult)
                .Verifiable();
        }

    }

    public class UserManagerMockSettings
    {
        public IdentityResult CreateAsyncResult { get; set; } = IdentityResult.Success;
    }

    public class SignInManagerMockSettings
    {
        public Task SignInAsyncResult { get; set; } = Task.CompletedTask;
        public Task<SignInResult> PasswordSignInAsyncResult { get; set; } = Task.FromResult(SignInResult.Success);
    }
}
