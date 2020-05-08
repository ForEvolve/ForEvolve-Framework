# ForEvolve Framework (or toolbox)

<!-- ![Build, Test, and Deploy master](https://github.com/ForEvolve/ForEvolve.Testing/workflows/Build,%20Test,%20and%20Deploy%20master/badge.svg) -->

[![VSTS master build](https://forevolve.visualstudio.com/ForEvolve-Framework/_apis/build/status/ForEvolve.ForEvolve-Framework?branchName=master)](https://forevolve.visualstudio.com/ForEvolve-Framework/_build/latest?definitionId=50&branchName=master)

This project is my personal toolbox, where I add utility classes that I was tired of copying from project to project, that I needed to facilitate my life or for any other reason toward reusability.

Examples of features are Azure table repository, Azure blob repositories (JSON object and files), markdown to HTML, HTML to pdf, and more.

<!-- ![VSTS master build](https://forevolve.visualstudio.com/_apis/public/build/definitions/fdc5922a-3dc1-4827-97a6-0f622b2fd497/26/badge) -->

## Versioning

The packages follows _semantic versioning_ and uses [Nerdbank.GitVersioning](https://github.com/dotnet/Nerdbank.GitVersioning) under the hood to automate versioning based on Git commits.

## NuGet

All packages are available on [https://www.nuget.org/profiles/ForEvolve](https://www.nuget.org/profiles/ForEvolve).

For example, you can load packages like that:

```PowerShell
dotnet add package ForEvolve.AspNetCore
dotnet add package ForEvolve.Azure
dotnet add package ForEvolve.Contracts
dotnet add package ForEvolve.Markdown
dotnet add package ForEvolve.Pdf
```

[![NuGet.org](https://img.shields.io/nuget/vpre/ForEvolve.AspNetCore)](https://www.nuget.org/packages/ForEvolve.AspNetCore/)
[![NuGet.org](https://img.shields.io/nuget/vpre/ForEvolve.Azure)](https://www.nuget.org/packages/ForEvolve.Azure/)
[![NuGet.org](https://img.shields.io/nuget/vpre/ForEvolve.Contacts)](https://www.nuget.org/packages/ForEvolve.Contacts/)
[![NuGet.org](https://img.shields.io/nuget/vpre/ForEvolve.Core)](https://www.nuget.org/packages/ForEvolve.Core/)
[![NuGet.org](https://img.shields.io/nuget/vpre/ForEvolve.EntityFrameworkCore)](https://www.nuget.org/packages/ForEvolve.EntityFrameworkCore/)
[![NuGet.org](https://img.shields.io/nuget/vpre/ForEvolve.Markdown)](https://www.nuget.org/packages/ForEvolve.Markdown/)
[![NuGet.org](https://img.shields.io/nuget/vpre/ForEvolve.Markdown.Abstractions)](https://www.nuget.org/packages/ForEvolve.Markdown.Abstractions/)
[![NuGet.org](https://img.shields.io/nuget/vpre/ForEvolve.Pdf)](https://www.nuget.org/packages/ForEvolve.Pdf/)
[![NuGet.org](https://img.shields.io/nuget/vpre/ForEvolve.Pdf.Abstractions)](https://www.nuget.org/packages/ForEvolve.Pdf.Abstractions/)

## Prerelease packages

[![feedz.io](https://img.shields.io/badge/endpoint.svg?url=https%3A%2F%2Ff.feedz.io%2Fforevolve%toolbox%2Fshield%2FForEvolve.AspNetCore%2Flatest)](https://f.feedz.io/forevolve/toolbox/packages/ForEvolve.AspNetCore/latest/download)
[![feedz.io](https://img.shields.io/badge/endpoint.svg?url=https%3A%2F%2Ff.feedz.io%2Fforevolve%toolbox%2Fshield%2FForEvolve.Azure%2Flatest)](https://f.feedz.io/forevolve/toolbox/packages/ForEvolve.Azure/latest/download)
[![feedz.io](https://img.shields.io/badge/endpoint.svg?url=https%3A%2F%2Ff.feedz.io%2Fforevolve%toolbox%2Fshield%2FForEvolve.Contacts%2Flatest)](https://f.feedz.io/forevolve/toolbox/packages/ForEvolve.Contacts/latest/download)
[![feedz.io](https://img.shields.io/badge/endpoint.svg?url=https%3A%2F%2Ff.feedz.io%2Fforevolve%toolbox%2Fshield%2FForEvolve.Core%2Flatest)](https://f.feedz.io/forevolve/toolbox/packages/ForEvolve.Core/latest/download)
[![feedz.io](https://img.shields.io/badge/endpoint.svg?url=https%3A%2F%2Ff.feedz.io%2Fforevolve%toolbox%2Fshield%2FForEvolve.EntityFrameworkCore%2Flatest)](https://f.feedz.io/forevolve/toolbox/packages/ForEvolve.EntityFrameworkCore/latest/download)
[![feedz.io](https://img.shields.io/badge/endpoint.svg?url=https%3A%2F%2Ff.feedz.io%2Fforevolve%toolbox%2Fshield%2FForEvolve.Markdown%2Flatest)](https://f.feedz.io/forevolve/toolbox/packages/ForEvolve.Markdown/latest/download)
[![feedz.io](https://img.shields.io/badge/endpoint.svg?url=https%3A%2F%2Ff.feedz.io%2Fforevolve%toolbox%2Fshield%2FForEvolve.Markdown.Abstractions%2Flatest)](https://f.feedz.io/forevolve/toolbox/packages/ForEvolve.Markdown.Abstractions/latest/download)
[![feedz.io](https://img.shields.io/badge/endpoint.svg?url=https%3A%2F%2Ff.feedz.io%2Fforevolve%toolbox%2Fshield%2FForEvolve.Pdf%2Flatest)](https://f.feedz.io/forevolve/toolbox/packages/ForEvolve.Pdf/latest/download)
[![feedz.io](https://img.shields.io/badge/endpoint.svg?url=https%3A%2F%2Ff.feedz.io%2Fforevolve%toolbox%2Fshield%2FForEvolve.Pdf.Abstractions%2Flatest)](https://f.feedz.io/forevolve/toolbox/packages/ForEvolve.Pdf.Abstractions/latest/download)

For the pre-release packages, use the ForEvolve/Toolbox [feedz.io](https://f.feedz.io/forevolve/toolbox/nuget/index.json) packages source.

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

## How to contribute?

If you would like to contribute to the Framework, first, thank you for your interest and please read [Contributing to ForEvolve open source projects](https://github.com/ForEvolve/ForEvolve-Framework/tree/master/CONTRIBUTING.md) for more information.

### Contributor Covenant Code of Conduct

Also, please read the [Contributor Covenant Code of Conduct](https://github.com/ForEvolve/ForEvolve-Framework/tree/master/CODE_OF_CONDUCT.md) that applies to all ForEvolve repositories.
