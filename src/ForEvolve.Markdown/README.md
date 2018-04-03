# Markdown File
This library is an abstraction allowing consumers to easily convert strings to Markdown.
The center piece is the `IMarkdownConverter` interface.

``` csharp
public interface IMarkdownConverter
{
    string ConvertToHtml(string markdown);
}
```

> The library is curently a wrapper around `lunet-io/markdig`, however, in the future, multiple implementations could be used.

## How to use
Register the services in the `ConfigureServices` method by calling the `AddMarkdig()` extension method. 

``` csharp
public void ConfigureServices(IServiceCollection services)
{
    //...
    services.AddMarkdig();
    //...
}
```

That's it! Now you can inject an `IMarkdownConverter` service in your classes.

### Configuring the pipeline
If you want more control over your markdown covnertion, you can configure the pipeline when calling the `AddMarkdig()` extension method.

#### To allow HTML

``` csharp
services.AddMarkdig(options => options.DisableHtml = false);
```

#### To add Extensions
 
``` csharp
services.AddMarkdig(options =>
{
    options.Configure = builder =>
    {
        // Add your customization here...
        builder.UseAdvancedExtensions(); // example
    };
});
```
