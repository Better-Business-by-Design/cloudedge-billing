using AccountsReceivable.BL.Models.Json;
using Microsoft.Extensions.Options;
using RestSharp;
// ReSharper disable ClassNeverInstantiated.Global

namespace AccountsReceivable.API.Shared.UiPath;

public record UiPathQueueItem(string Key, string Name, int Id);

public record UiPathQueueItemCollection(List<UiPathQueueItem> Value);

public class UiPathClient : IDisposable
{
    private readonly RestClient _client;

    public UiPathClient(IOptions<ConfigurationDto> configOptions)
    {
        var config = configOptions.Value;
        var options = new RestClientOptions(config.FullUrl)
        {
            Authenticator = new UiPathAuthenticator(config)
        };
        _client = new RestClient(options);
        _client.AddDefaultHeader("X-UIPATH-TenantName", config.TenantName);
        _client.AddDefaultHeader("X-UIPATH-OrganizationUnitId", config.FolderId);
    }

    public async Task<ICollection<UiPathQueueItem>> GetQueueItems()
    {
        var response = await _client.GetJsonAsync<UiPathQueueItemCollection>("odata/QueueDefinitions");
        return response!.Value;
    }

    public async Task<ICollection<UiPathProcessDto>> GetProcessesByName(string name)
    {
        var request = new RestRequest("odata/Releases");
        request.AddQueryParameter("$filter", $"ProcessKey eq '{name}'");
        var response = await _client.ExecuteAsync<UiPathProcessDtoCollection>(request);
        return response.Data!.Value;
    }

    public async Task<UiPathJobDto> StartProcess(UiPathJobStartRequestBody startRequestBody)
    {
        var request = new RestRequest("odata/Jobs/UiPath.Server.Configuration.OData.StartJobs", Method.Post);
        request.AddJsonBody(startRequestBody);
        var response = await _client.ExecuteAsync<UiPathJobSingleton>(request);
        return response.Data!.Value.First();
    }
    

    public void Dispose()
    {
        _client.Dispose();
        GC.SuppressFinalize(this);
    }
}