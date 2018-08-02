namespace ForEvolve.Pdf.PhantomJs
{
    public sealed class PhantomJsConstants
    {
        public const string OSNotSupported = "Your operating system is not supported or could not be discovered.";
        public const string OutputDirectoryDoesNotExist = "The specified output directory does not exist.";
        public const string DirectoryDoesNotExist = "The specified directory does not exist.";
        public const string PhantomJSExeNotFound = "The PhantomJS executable file was not found. Please make sure you installed the `ForEvolve.PhantomJs.Dependencies` NuGet package.";
        public const string ImpossibleToFindADefaultPhantomRootDirectory = "The system was unable to find a default Phantom JS root. Please make sure you installed the `ForEvolve.PhantomJs.Dependencies` NuGet package. If the problem persists, please report it on GitHub and specify the root yourself by configuring HtmlToPdfConverterOptions in your composition root.";
    }
}
