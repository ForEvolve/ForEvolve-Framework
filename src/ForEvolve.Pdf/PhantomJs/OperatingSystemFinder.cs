using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace ForEvolve.Pdf.PhantomJs
{
    public class OperatingSystemFinder : IOperatingSystemFinder
    {
        [ExcludeFromCodeCoverage]
        public OperatingSystem Find()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return OperatingSystem.WINDOWS;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return OperatingSystem.LINUX;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return OperatingSystem.OSX;
            }
            throw new NotSupportedException(PhantomJsConstants.OSNotSupported);
        }
    }
}
