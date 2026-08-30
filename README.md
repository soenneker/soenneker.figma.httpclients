[![](https://img.shields.io/nuget/v/soenneker.figma.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.figma.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.figma.httpclients/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.figma.httpclients/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.figma.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.figma.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.figma.httpclients/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.figma.httpclients/actions/workflows/codeql.yml)

# Soenneker.Figma.HttpClients

A cached, authenticated `HttpClient` for the Figma REST API.

## Installation

```bash
dotnet add package Soenneker.Figma.HttpClients
```

## Register the client

```csharp
using Soenneker.Figma.HttpClients.Registrars;
using Microsoft.Extensions.DependencyInjection;

services.AddFigmaOpenApiHttpClientAsSingleton();
```

Register the wrapper as a singleton when it is shared by scoped Figma utilities. Disposing a scoped utility must not tear down the cached transport used by later scopes.

## Configuration

```json
{
  "Figma": {
    "ApiKey": "your-figma-token"
  }
}
```

`Figma:ApiKey` is required. Requests use `https://api.figma.com` and the `X-Figma-Token` header by default. These optional settings override them:

- `Figma:ClientBaseUrl` changes the API base address, which is useful for a test server.
- `Figma:AuthHeaderName` changes the authentication header name.
- `Figma:AuthHeaderValueTemplate` changes the header value and replaces `{token}` with the configured API key. For example, `Bearer {token}`.

Do not commit the API key to configuration files. Supply it through user secrets, environment-specific secret storage, or your deployment platform.

## Use the client

```csharp
public sealed class FigmaFilesClient(IFigmaOpenApiHttpClient client)
{
    public async Task<string> GetFile(string fileKey, CancellationToken cancellationToken)
    {
        HttpClient httpClient = await client.Get(cancellationToken);
        return await httpClient.GetStringAsync($"/v1/files/{Uri.EscapeDataString(fileKey)}", cancellationToken);
    }
}
```

`Get()` returns the cached `HttpClient`; callers do not own that instance and should not dispose it. DI disposes the wrapper, which removes the cached client. The registrar uses `TryAdd`, so an application can register its own `IFigmaOpenApiHttpClient` first.

This package configures transport and authentication only. It does not validate Figma file keys, retry requests, handle rate limits, or deserialize API responses.
