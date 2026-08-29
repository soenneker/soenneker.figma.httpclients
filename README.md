[![](https://img.shields.io/nuget/v/soenneker.figma.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.figma.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.figma.httpclients/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.figma.httpclients/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.figma.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.figma.httpclients/)

# Soenneker.Figma.HttpClients

A .NET thread-safe singleton HttpClient for.

## Install

```bash
dotnet add package Soenneker.Figma.HttpClients
```

## Quick start

```csharp
using Soenneker.Figma.HttpClients.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddFigmaOpenApiHttpClientAsSingleton();
```

Adds `FigmaOpenApiHttpClient` as a singleton service.

## What you get

- `IFigmaOpenApiHttpClient` — A .NET thread-safe singleton HttpClient for.
- `FigmaOpenApiHttpClientRegistrar` — Registers the OpenAPI HttpClient wrapper for dependency injection.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `FigmaOpenApiHttpClientRegistrar.AddFigmaOpenApiHttpClientAsSingleton(services)` | Adds `FigmaOpenApiHttpClient` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `FigmaOpenApiHttpClientRegistrar.AddFigmaOpenApiHttpClientAsScoped(services)` | Adds `FigmaOpenApiHttpClient` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Reuse the registered client instead of constructing one per operation.
- Calls that return a cached or singleton value reuse the same instance until the owning service is disposed.
- Dispose instances you own when their scope ends so held resources can be released.
