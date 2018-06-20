# ForEvolve .Net Core Framework
This project contains some utilities that I was tired of copying from project to project or that I decided to add to facilitate my life (like Azure table repository, this is a great time saver and Dynamic InternalServerError, that handle Exceptions in web APIs for me).

- `master` build: ![VSTS master build](https://forevolve.visualstudio.com/_apis/public/build/definitions/fdc5922a-3dc1-4827-97a6-0f622b2fd497/26/badge)
- `dev` build: ![VSTS dev build](https://forevolve.visualstudio.com/_apis/public/build/definitions/fdc5922a-3dc1-4827-97a6-0f622b2fd497/25/badge)

## Work in progress
This is a work in progress.

I do not manage version number yet; it is still too early. 
Version numbers are autoincremented by automatic builds.

## NuGet feed
ForEvolve [NuGet V3 feed URL](https://www.myget.org/F/forevolve/api/v3/index.json) packages source. See the [Table of content](https://github.com/ForEvolve/Toc) project for more info.

## The projects
### ForEvolve
You can find the meta-package that references all `ForEvolve.*` projects at the following repo: [ForEvolve/MetaPackages](https://github.com/ForEvolve/MetaPackages).

### ForEvolve.ApplicationInsights
This project aims at adding features over ApplicationInsights.
For now, it contains only a `TrackExceptionsFilterAttribute`.

### ForEvolve.AspNetCore
This project aims at adding features over Asp.Net Core.

#### Json Extensions
##### Convert an `object` to a Json string `HttpContent`
```CSharp
var myHttpContent = myObject.ToJsonHttpContent();
```

##### Serialize an `object` to Json
```CSharp
var myJsonString = myObject.ToJson();
```

##### Read and deserialize a Json string from an `HttpContent`
```CSharp
var myObject = myHttpContent.ReadAsJsonObjectAsync<MyObjectType>();
```

##### Deserialize a json string
```CSharp
var myObject = anyString.ToObject<MyObjectType>();
```

#### Middleware
* `AddRequestIdMiddleware` is a middleware that adds an HTTP Header containing a request id. This is useful if your system must do multiple remote HTTP calls, like in a Microservices architecture or a distributed system.
* `SetRequestIdAsTraceIdentifier` is a middleware that set the `HttpContext.TraceIdentifier` to the request id HTTP Header (set by `AddRequestIdMiddleware`). It also allows transferring the request id header to the HTTP response (for client-side tracing).

#### Services
* `IViewRenderer` Allow you to render a view to string.

### ForEvolve.Azure
This project aims at adding features over the Azure SDK like Object (Blob), Queue, Table and KeyVault repositories.

*I will write more docs later for this one...*

### ForEvolve.Core
This project holds shared classes. For now, it only contains `ForEvolveException`.

### Dynamic InternalServerError
A dynamic internal server error filter for ASP.NET Core, targetting Asp.Net Core 2.0, that translate Exceptions (status code 500) to JSON automatically.

Validation errors are also translated automatically, following the same convention, for response with status code 400.

The error model is based on: [Microsoft REST API Guidelines](https://github.com/Microsoft/api-guidelines/blob/vNext/Guidelines.md#7102-error-condition-responses).

#### How to use
In your `Startup` class, you must `AddDynamicInternalServerError()` to add dependencies and `ConfigureDynamicInternalServerError()` to add the filter to MVC.

```CSharp
public void ConfigureServices(IServiceCollection services)
{
    // Add DynamicInternalServerError
    services.AddDynamicInternalServerError();

    // Add framework services.
    services.AddMvc(options =>
    {
        options.ConfigureDynamicInternalServerError();
    });
}
```

Thats it, now exceptions are translated to JSON.

#### How to add Swagger support
In the `ConfigureServices` method, you must add `services.AddDynamicInternalServerErrorSwagger();`.
In the `services.AddSwaggerGen(c => { ... })` you must register swagger operation filters by calling `c.AddDynamicInternalServerError();`.

```CSharp
public void ConfigureServices(IServiceCollection services)
{
    // Add DynamicInternalServerError
    services.AddDynamicInternalServerError();
    services.AddDynamicInternalServerErrorSwagger();

    // Add framework services.
    services.AddMvc(options =>
    {
        options.ConfigureDynamicInternalServerError();
    });
    
    // Add and configure Swagger.
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });

        // Add and configure DynamicInternalServerError.
        c.AddDynamicInternalServerError();
    });
}
```

#### Dynamic Validation
`DynamicValidationActionFilter` translates `BadRequestObjectResult` that has a value of type `SerializableError` to an `ErrorResponse` object, following the same convention as `DynamicValidationActionFilter` do. 

##### How to use
By default, by registering `DynamicInternalServerError` you also register `DynamicValidation`.
Return `BadRequest(ModelState);` in a controller action. To define `ErrorResponse` automatically in the swagger definition file, simply decorat the action with `[ProducesResponseType(400)]` (do not specify a type).

```CSharp
[HttpPost]
[ProducesResponseType(400)]
public IActionResult Post([FromBody]string value)
{
    if(!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }
    // ...
}
```

## ForEvolve.XUnit
This project contains some XUnit test utilities like:

- `IdentityHelper` that creates the plumbing to Mock `SignInManager` and `UserManager`.
- `HttpContextHelper` that creates the plumbing to Mock `HttpContext` and `HttpRequest`.
- `MvcContextHelper` at creates the plumbing to Mock `IUrlHelper` and `ActionContext`.
- `BaseStartupExtensionsTest` is a base class to help test startup extensions.

## Whats next
I plan on evolving theses libraries as I use them in multiple projects.

Examples of what I want to add:

* Azure table repository with memory cache (to save HTTP calls)
* Azure table repository with distributed cache (to save HTTP calls)
* Azure table repository with both memory and distributed cache (decorators?)
* Azure table batch operations
* Azure cognitive services helpers
* Easy Azure Cosmos DB support
* ...

If you would like to contribute to the Framework, feel free to contact me.
