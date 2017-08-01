# ForEvolve .Net Core Framework

This project contains some utilities that I was tired of copying from project to project or that I decided to add to facilitate my life (like Azure table repository - this is a great time saver).

This is a work in progress, so there is more stuff to come!

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
