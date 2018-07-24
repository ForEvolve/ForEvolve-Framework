using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.Pdf.PhantomJs
{
    public class ExecutableNameFinderTest
    {
        public class Ctor
        {
            [Fact]
            public void Should_guard_against_null()
            {
                // Arrange
                var nullOperatingSystemFinder = default(IOperatingSystemFinder);

                // Act & Assert
                Assert.Throws<ArgumentNullException>("operatingSystemFinder", () => new ExecutableNameFinder(nullOperatingSystemFinder));
            }
        }

        public class Find
        {
            private readonly ExecutableNameFinder sut;
            private readonly Mock<IOperatingSystemFinder> _operatingSystemFinderMock;

            public Find()
            {
                _operatingSystemFinderMock = new Mock<IOperatingSystemFinder>();
                sut = new ExecutableNameFinder(_operatingSystemFinderMock.Object);
            }

            public static TheoryData<OperatingSystem, string> ExeData = new TheoryData<OperatingSystem, string>
            {
                { OperatingSystem.LINUX, "linux64_phantomjs.exe" },
                { OperatingSystem.WINDOWS, "windows_phantomjs.exe" },
                { OperatingSystem.OSX, "osx_phantomjs.exe" },
            };

            [Theory]
            [MemberData(nameof(ExeData))]
            public void Should_return_the_expected_exe_name(OperatingSystem os, string expectedExe)
            {
                // Arrange
                _operatingSystemFinderMock
                    .Setup(x => x.Find())
                    .Returns(os);

                // Act
                var result = sut.Find();

                // Assert
                Assert.Equal(expectedExe, result);
            }

            [Fact]
            public void Should_throw_a_NotSupportedException_when_the_OS_is_not_supported()
            {
                // Arrange
                _operatingSystemFinderMock
                    .Setup(x => x.Find())
                    .Returns((OperatingSystem)100);

                // Act & Assert
                Assert.Throws<NotSupportedException>(() => sut.Find());
            }

        }
    }
}
