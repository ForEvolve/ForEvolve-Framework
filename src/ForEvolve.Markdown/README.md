# ForEvolve.Markdown
This library contains implementations of the `ForEvolve.Markdown.Abstractions` library.
It allows consumers to easily convert strings to Markdown.
The center piece is the `IMarkdownConverter` interface.

``` csharp
public interface IMarkdownConverter
{
    string ConvertToHtml(string markdown);
}
```

> **Note**
>
> Right now, the library contains only a wrapper around `lunet-io/markdig`, however, in the future, multiple implementations could be used.

## NuGet feed
ForEvolve [NuGet V3 feed URL](https://www.myget.org/F/forevolve/api/v3/index.json) packages source. See the [Table of content](https://github.com/ForEvolve/Toc) project for more info.

## How to use (Markdig)
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
If you want more control over your markdown covertion, you can configure the pipeline when calling the `AddMarkdig()` extension method.

#### Allow HTML
By default, the HTML parsing is disabled.

> The `DisableHtml()` method is called on the `MarkdownPipelineBuilder`.

``` csharp
services.AddMarkdig(options => options.DisableHtml = false);
```

#### To add Extensions
The `MarkdownPipelineBuilder` is exposed in the options; you can leverage it as follow.
 
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

## References
- [Markdig](https://github.com/lunet-io/markdig)