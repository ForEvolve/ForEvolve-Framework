# .NET core helpers

This project contains some useful code that I was tired of copying from project to project. 
I decided to create a library with it.

There is much more code that I would like to include in this repository, so there will be more code to come.

## Json Extensions

### Convert an `object` to a Json string `HttpContent`
```CSharp
var myHttpContent = myObject.ToJsonHttpContent();
```

### Serialize an `object` to Json
```CSharp
var myJsonString = myObject.ToJson();
```

### Read and deserialize a Json string from an `HttpContent`
```CSharp
var myObject = myHttpContent.ReadAsJsonObjectAsync<MyObjectType>();
```

### Deserialize a json string
```CSharp
var myObject = anyString.ToObject<MyObjectType>();
```
