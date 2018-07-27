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

-   `BaseMiddleware` implement the chaining of middle ware. This is an `abstract class` and must be inherited.
-   `AddRequestHeaderMiddleware` implement the logic allowing to add a custom HTTP header to the current request. _This is an `abstract class` and must be inherited._
-   `AddResponseHeaderMiddleware` implement the logic allowing to add a custom HTTP header to the current response. _This is an `abstract class` and must be inherited._

#### Services

-   `IEmailSenderService` is an email sender implementation. It can be parametered with all `SmtpClient` options and can also automatically convert HTML emails to plain text, adding an `AlternateView`.
-   `IViewRenderer` allow you to render a view to string.
