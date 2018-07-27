# Dynamic InternalServerError

A dynamic internal server error filter for ASP.NET Core, targetting Asp.Net Core 2.0, that translate Exceptions (status code 500) to JSON automatically.

Validation errors are also translated automatically, following the same convention, for response with status code 400.

The error model is based on: [Microsoft REST API Guidelines](https://github.com/Microsoft/api-guidelines/blob/vNext/Guidelines.md#7102-error-condition-responses).

## How to use

In your `Startup` class, you must `AddForEvolveDynamicInternalServerError()` to add dependencies and `ConfigureForEvolveDynamicInternalServerError()` to add the filter to MVC.

```CSharp
public void ConfigureServices(IServiceCollection services)
{
    // Add DynamicInternalServerError
    services.AddForEvolveDynamicInternalServerError();

    // Add framework services.
    services.AddMvc(options =>
    {
        options.ConfigureForEvolveDynamicInternalServerError();
    });
}
```

Thats it, now exceptions are translated to JSON.

## How to add Swagger support

By using the `ForEvolve.DynamicInternalServerError.Swagger` package, in the `ConfigureServices` method, you must add `services.AddForEvolveDynamicInternalServerErrorSwagger();`.
In the `services.AddSwaggerGen(c => { ... })` you must register swagger operation filters by calling `c.AddForEvolveDynamicInternalServerError();`.

```CSharp
public void ConfigureServices(IServiceCollection services)
{
    // Add DynamicInternalServerError
    services.AddForEvolveDynamicInternalServerError();
    services.AddForEvolveDynamicInternalServerErrorSwagger();

    // Add framework services.
    services.AddMvc(options =>
    {
        options.ConfigureForEvolveDynamicInternalServerError();
    });

    // Add and configure Swagger.
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });

        // Add and configure DynamicInternalServerError.
        c.AddForEvolveDynamicInternalServerError();
    });
}
```

## Dynamic Validation

`DynamicValidationActionFilter` translates `BadRequestObjectResult` that has a value of type `SerializableError` to an `ErrorResponse` object, following the same convention as `DynamicValidationActionFilter` do.

### How to use

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
