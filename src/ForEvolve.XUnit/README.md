# ForEvolve.XUnit

**This is still a prerelease project where breaking changes are not reflected by a major version number bump.**

This package is not part of the `ForEvolve.App` meta package but will be part of another meta-package after I find the time to clean it up.

## Content

This project contains some XUnit test utilities like:

-   `IdentityHelper` that creates the plumbing to Mock `SignInManager` and `UserManager`.
-   `HttpContextHelper` that creates the plumbing to Mock `HttpContext` and `HttpRequest`.
-   `MvcContextHelper` at creates the plumbing to Mock `IUrlHelper` and `ActionContext`.
-   `BaseStartupExtensionsTest` is a base class to help test startup extensions.
-   Some `IServiceCollection` and `IServiceProvider` extensions, like `services.AssertSingletonServiceExists<ISomeService>()`.
-   `OperationResultHelper` and `OperationResultFactoryFake` to help deal with `IOperationResult` testing. _These APIs will likely change in the future._
-   Lot more stuff, feel free to dig in and find out by yourself if you dare...

## The future

I also have an HTTP test server in construction (started a while back actually) that allows setting inputs and outputs to test API clients, in-memory.
It is in the `HttpTests` directory, and there is also a branch somewhere that improve it. _This is incomplete, working, but incomplete; both this and the other branch code._

There is much stuff that I want to improve or add to this library in the future, and since I use it myself in my projects, I will probably do.
