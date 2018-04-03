# Markdown File
This library is a wrapper around `lunet-io/markdig`, allowing to easily convert a string to Markdown.


## The core concept
The most important part of this library is the `IMarkdownConverter` interace.

``` csharp
public interface IMarkdownConverter
{
    string ConvertToHtml(string markdown);
}
```

> The library curently depend on `lunet-io/markdig`, but this could be swaped away in the future or use multiple implementations.

## How to use
Register the services in the `ConfigureServices` method by calling the `AddMarkdown()` extension method. 

``` csharp
public void ConfigureServices(IServiceCollection services)
{
    //...
    services.AddMarkdown();
    //...
}
```

That's it! Now you can inject an `IMarkdownConverter` service in your classes.

### Configuring the pipeline
If you want more control over your markdown covnertion, you can configure the pipeline when calling the `AddMarkdown()` extension method.

#### To allow HTML

``` csharp
services.AddMarkdown(options => options.DisableHtml = false);
```

#### To add Extensions
 
``` csharp
services.AddMarkdown(options =>
{
    options.Configure = builder =>
    {
        // Add your customization here...
        builder.UseAdvancedExtensions(); // example
    };
});
```
