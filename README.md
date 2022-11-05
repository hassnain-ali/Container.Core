# Container.Core
## A helper wrapper for Autofac

Container.Core is an Autofac configration container for startup projects which has some boilerplate code
## Features
- Dependency container
- Dependency resolver
- Cache (In memory/ Distributed cache)
- Swagger configration options for versioned API
- Swagger operational Filters
- Singleton services / Container
- Types resolver/ Types finder
- Files provider
- Enum Helpers
- Common Helpers and validation methods

## Installation
In Program file add the following lines register Services

```
AssemblyInformation.Set(typeof(Program).Assembly);
var builder = WebApplication.CreateBuilder(args);
...
builder.Host
        .ConfigureContainer<ContainerBuilder>(container => EngineContext.Create().RegisterDependencies(container, builder.Services))
        .UseServiceProviderFactory(new AutofacServiceProviderFactory());

...

builder.Services.AddContainerCore()
...
builder.Services.AddVersionApiSwaggerAPI();
...
CommonHelper.DefaultFileProvider = new ContainerFileProvider(builder.Environment.WebRootPath);
var app = builder.Build();
```

Then After build add pipelines

```
if (app.Environment.IsDevelopment())
        {
            _ = app.UseSwagger();
            _ = app.UseSwaggerUI(c =>
            {
                //c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1.0");
                c.OAuthAppName("<OAuth App>");
                c.OAuthClientId("<client_id>");
                c.OAuthClientSecret("<client_secret>");
                c.OAuthScopes("<scopes>");
                c.OAuthUsePkce();
            });
        }
...
app.UseVersionedSwaggerAPI();
```

## Plugins

Plugins used in this package.

| Plugin |
| ------ |
| [Autofac](https://github.com/autofac/Autofac) |
| [Autofac.Extensions.DependencyInjection](https://github.com/autofac/Autofac.Extensions.DependencyInjection) |
| [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) |

## License

MIT
**Free Software, Hell Yeah!**

Credits: 
- Autofac
- .NET Boxed 
- Nop Commerce