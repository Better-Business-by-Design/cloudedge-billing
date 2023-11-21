using Newtonsoft.Json.Linq;

namespace AccountsReceivable.BL.Models.Json;

public record UiPathProcessDtoCollection(
    List<UiPathProcessDto> Value
    );

public record UiPathProcessDto(
    string Key,
    string ProcessKey,
    string ProcessVersion,
    bool IsLatestVersion,
    bool IsProcessDeleted,
    string Description,
    string Name,
    int EntryPointId,
    int Id,
    UiPathProcessArguments Arguments,
    List<UiPathTag> Tags);

public class UiPathProcessArguments
{
    public string Input { get; set; } = string.Empty;
    public string Output { get; set; } = string.Empty;

    public JArray GetInputJArray()
    {
        return JArray.Parse(Input);
    }
}

public record UiPathTag(
    string Name,
    string DisplayName,
    string Value,
    string DisplayValue
    );

    
    