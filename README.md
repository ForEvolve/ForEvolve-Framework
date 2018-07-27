# ForEvolve Framework (or toolbox)

This project started as my personal toolbox, where I added utility classes that I was tired of copying from project to project, that I needed to facilitate my life or for any other reason toward reusability.

Then I cleaned this up a little to make it decent for a release version where other people would be able to depend on it; and here we are: `v1.0.0`.
Each project is loadable on its own, or you can load bundles using meta-packages.

Examples of features are Azure table repository, Azure blob repositories (JSON object and files), automatic conversion of `Exception` to JSON, markdown to HTML, HTML to pdf, generic operation results, test utilities, and more.

Feel free to use the NuGet packages, fork it, contribute to it and post issues. These are ever-evolving projects!

![VSTS master build](https://forevolve.visualstudio.com/_apis/public/build/definitions/fdc5922a-3dc1-4827-97a6-0f622b2fd497/26/badge)

## Versioning

As of `v1.0.0` and onward, the project follows `semver` to manage version numbers.
All version numbers are linked and located in the `src/Directory.Build.props` file.

## NuGet

All packages are available on [https://www.nuget.org/profiles/ForEvolve](https://www.nuget.org/profiles/ForEvolve).

To load all application packages, use the `ForEvolve.App` package:

```PowerShell
Install-Package ForEvolve.App
```

or

```PowerShell
dotnet add package ForEvolve.App
```

See [ForEvolve/MetaPackages](https://github.com/ForEvolve/MetaPackages) for more information.

### Individual packages (App)

To load individual packages, you can:

```PowerShell
Install-Package ForEvolve.AspNetCore
Install-Package ForEvolve.Azure
Install-Package ForEvolve.Contracts
Install-Package ForEvolve.Core
Install-Package ForEvolve.DynamicInternalServerError.Swagger
Install-Package ForEvolve.DynamicInternalServerError
Install-Package ForEvolve.AspNetCore.Localization
Install-Package ForEvolve.Markdown.Abstractions
Install-Package ForEvolve.Markdown
Install-Package ForEvolve.Pdf.Abstractions
Install-Package ForEvolve.Pdf
Install-Package ForEvolve.XUnit # Not included in ForEvolve.App metapackage
```

or

```PowerShell
dotnet add package ForEvolve.AspNetCore
dotnet add package ForEvolve.Azure
dotnet add package ForEvolve.Contracts
dotnet add package ForEvolve.Core
dotnet add package ForEvolve.DynamicInternalServerError.Swagger
dotnet add package ForEvolve.DynamicInternalServerError
dotnet add package ForEvolve.AspNetCore.Localization
dotnet add package ForEvolve.Markdown.Abstractions
dotnet add package ForEvolve.Markdown
dotnet add package ForEvolve.Pdf.Abstractions
dotnet add package ForEvolve.Pdf
dotnet add package ForEvolve.XUnit # Not included in ForEvolve.App metapackage
```

## Prerelease MyGet

For the pre-release packages, use the ForEvolve [NuGet V3 feed URL](https://www.myget.org/F/forevolve/api/v3/index.json) packages source. See the [Table of content](https://github.com/ForEvolve/Toc) project for more info.

### Pre-release build number

Pre-release builds number are autoincremented.

## The projects

In this section are quick descriptions of assemblies and links to their README file. _This is until I find the time to build a documentation website._

### ForEvolve

You can find the meta-package that references all `ForEvolve.*` projects at the following repo: [ForEvolve/MetaPackages](https://github.com/ForEvolve/MetaPackages).

### ForEvolve.AspNetCore

This project aims at adding features over Asp.Net Core.
Example: `myObject.ToJsonHttpContent()`, `myObject.ToJson()`, `myHttpContent.ReadAsJsonObjectAsync<MyObjectType>()`, `anyString.ToObject<MyObjectType>()`, `IViewRendererService`, `IEmailSenderService`, `IHttpHeaderValueAccessor`, `IUserIdAccessor`, some base middlewares and more.

See [ForEvolve.AspNetCore](https://github.com/ForEvolve/ForEvolve-Framework/tree/master/src/ForEvolve.AspNetCore) for more information.

### ForEvolve.Azure

This project aims at adding features over the Azure SDK like Object (Blob), Queue, Table and KeyVault repositories.

See [ForEvolve.Azure](https://github.com/ForEvolve/ForEvolve-Framework/tree/master/src/ForEvolve.Azure) for more information.

### ForEvolve.Contracts

This project contains shared models.

See [ForEvolve.Contracts](https://github.com/ForEvolve/ForEvolve-Framework/tree/master/src/ForEvolve.Contracts) for more information.

### ForEvolve.Core

This project stand as the root of all projects. For now, it only contains the `ForEvolveException` class.

See [ForEvolve.Core](https://github.com/ForEvolve/ForEvolve-Framework/tree/master/src/ForEvolve.Core) for more information.

### ForEvolve.DynamicInternalServerError

A dynamic internal server error filter for ASP.NET Core, that translate Exceptions (status code 500) to JSON automatically.

Validation errors are also translated automatically, following the same convention, for response with status code 400.

See [ForEvolve.DynamicInternalServerError](https://github.com/ForEvolve/ForEvolve-Framework/tree/master/src/ForEvolve.DynamicInternalServerError) for more information.

### ForEvolve.DynamicInternalServerError.Swagger

Add `Swagger` support to `ForEvolve.DynamicInternalServerError`.

See [ForEvolve.DynamicInternalServerError](https://github.com/ForEvolve/ForEvolve-Framework/tree/master/src/ForEvolve.DynamicInternalServerError) for more information.

### ForEvolve.Markdown

Allows consumers to easily convert strings to Markdown using the `IMarkdownConverter` interface.

See [ForEvolve.Markdown](https://github.com/ForEvolve/ForEvolve-Framework/tree/master/src/ForEvolve.Markdown) for more information.

### ForEvolve.Markdown.Abstractions

This is the abstractions library. Only use this if you want to create your own custom implementation of the `IMarkdownConverter`.

See the [`ForEvolve.Markdown`](https://github.com/ForEvolve/ForEvolve-Framework/tree/master/src/ForEvolve.Markdown) project for more info.

### ForEvolve.Pdf

This library contains implementations of the `ForEvolve.Pdf.Abstractions.IHtmlToPdfConverter` interface, allowing convertion of HTML to a PDF.

See the [`ForEvolve.Pdf`](https://github.com/ForEvolve/ForEvolve-Framework/tree/master/src/ForEvolve.Pdf) project for more info.

### ForEvolve.Pdf.Abstractions

This is the abstractions library. Only use this if you want to create your own custom implementation of the `IHtmlToPdfConverter`.

See the [`ForEvolve.Pdf.Abstractions`](https://github.com/ForEvolve/ForEvolve-Framework/tree/master/src/ForEvolve.Pdf.Abstractions) project for more info.

### ForEvolve.XUnit

This project contains some testing utilities, mocks, extension methods, etc.

This package is not part of the `ForEvolve.App` metapackage.

_This package is still a prerelease project where breaking changes are not reflected by a major version number bump._

See the [`ForEvolve.XUnit`](https://github.com/ForEvolve/ForEvolve-Framework/tree/master/src/ForEvolve.XUnit) project for more info.

## Whats next

I plan on evolving theses libraries as I use them in my projects.

## How to contribute?

If you would like to contribute to the Framework, first, thank you for your interest and please read [Contributing to ForEvolve open source projects](https://github.com/ForEvolve/ForEvolve-Framework/tree/master/CONTRIBUTING.md) for more information.

### Contributor Covenant Code of Conduct

Also, please read the [Contributor Covenant Code of Conduct](https://github.com/ForEvolve/ForEvolve-Framework/tree/master/CODE_OF_CONDUCT.md) that applies to all ForEvolve repositories.
