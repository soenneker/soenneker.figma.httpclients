using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
namespace Soenneker.Figma.HttpClients.Abstract;
/// <summary>
/// Provides the cached, authenticated HTTP client used to call the Figma API.
/// </summary>
public interface IFigmaOpenApiHttpClient: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Returns the cached HTTP client configured for the Figma API.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the shared HTTP client. The caller must not dispose it.</returns>
    ValueTask<HttpClient> Get(CancellationToken cancellationToken = default);
}
