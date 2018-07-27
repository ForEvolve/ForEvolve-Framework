using System;

namespace ForEvolve.Pdf.PhantomJs
{
    public class ExecutableNameFinder : IExecutableNameFinder
    {
        private readonly IOperatingSystemFinder _operatingSystemFinder;

        public ExecutableNameFinder(IOperatingSystemFinder operatingSystemFinder)
        {
            _operatingSystemFinder = operatingSystemFinder ?? throw new ArgumentNullException(nameof(operatingSystemFinder));
        }

        public string Find()
        {
            var os = _operatingSystemFinder.Find();
            switch (os)
            {
                case OperatingSystem.LINUX:
                    return "linux64_phantomjs.exe";
                case OperatingSystem.WINDOWS:
                    return "windows_phantomjs.exe";
                case OperatingSystem.OSX:
                    return "osx_phantomjs.exe";
            }
            throw new NotSupportedException(PhantomJsConstants.OSNotSupported);
        }
    }
}
