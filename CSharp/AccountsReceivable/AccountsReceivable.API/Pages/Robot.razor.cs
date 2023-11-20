using System.Security.Authentication;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;
using RestSharp.Authenticators;

namespace AccountsReceivable.API.Pages;

public partial class Robot : ComponentBase
{
    [Inject] private UiPathClient UiPathClient { get; set; } = null!;

    private UiPathQueueItem? _queueItem;

    protected override async Task OnInitializedAsync()
    {
        var items = await UiPathClient.GetQueueItems();
        _queueItem = items.FirstOrDefault();
        await base.OnInitializedAsync();
    }

}

public class ConfigurationDto
{
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string Scope { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;

    public string FullUrl { get; set; } = string.Empty;
    public string TenantName { get; set; } = string.Empty;
    public string FolderId { get; set; } = string.Empty;
}

record TokenResponse
{
    [JsonPropertyName("token_type")]
    public string TokenType { get; init; }
    
    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; }
}

public class UiPathAuthenticator : AuthenticatorBase
{
    private readonly string _baseUrl;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _scope;

    public UiPathAuthenticator(ConfigurationDto config) : base("")
    {
        _baseUrl = config.BaseUrl;
        _clientId = config.ClientId;
        _clientSecret = config.ClientSecret;
        _scope = config.Scope;
    }

    protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
    {
        Token = string.IsNullOrEmpty(Token) ? await GetToken() : Token;
        return new HeaderParameter(KnownHeaders.Authorization, Token);
    }

    async Task<string> GetToken()
    {
        var options = new RestClientOptions(_baseUrl)
        {
            Authenticator = new HttpBasicAuthenticator(_clientId, _clientSecret)
        };
        using var client = new RestClient(options);

        var request = new RestRequest("identity_/connect/token")
            .AddParameter("grant_type", "client_credentials")
            .AddParameter("scope", _scope);
        
        var response = await client.PostAsync<TokenResponse>(request);
        return $"{response!.TokenType} {response!.AccessToken}";
    }
}

public record UiPathQueueItem(string Key, string Name, int Id);

public interface IUiPathClient
{
    Task<ICollection<UiPathQueueItem>> GetQueueItems();
}

public class UiPathClient : IUiPathClient, IDisposable
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

        // var request = new RestRequest("odata/QueueDefinitions", Method.Get);
        // request.AddHeader(KnownHeaders.ContentType, "application/json");
        // var normalResponse = await _client.ExecuteAsync<UiPathQueueItemCollection>(request);
        
        var response = await _client.GetJsonAsync<UiPathQueueItemCollection>(
            "odata/QueueDefinitions"
            );
        return response!.value;
    }

    record UiPathQueueItemCollection(List<UiPathQueueItem> value);

    public void Dispose()
    {
        _client.Dispose();
        GC.SuppressFinalize(this);
    }
}
