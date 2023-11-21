namespace AccountsReceivable.BL.Models.Json;

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