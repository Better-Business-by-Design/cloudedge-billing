using System.Text.Json.Serialization;
using AccountsReceivable.BL.Models.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace AccountsReceivable.API.Shared.UiPath;

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

    private async Task<string> GetToken()
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

record TokenResponse
{
    [JsonPropertyName("token_type")]
    public string TokenType { get; init; }
    
    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; }
}