using AccountsReceivable.BL.Models.Json.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AccountsReceivable.BL.Models.Json;

public record UiPathStartJobsRequest
{
    public UiPathStartProcessDto startInfo { get; set; }
}

public record UiPathStartProcessDto
{
    public string ReleaseKey { get; set; }
    public UiPathJobRobotStrategy? Strategy { get; set; }
    public long[]? RobotIds { get; set; }
    public long[]? MachineSessionIds { get; set; }
    public int? NoOfRobots { get; set; }
    
    // Deprecated
    // public int JobsCount { get; set; }
    
    public UiPathJobSourceType? Source { get; set; }
    public UiPathJobPriority? Priority { get; set; }
    public int? SpecificPriorityValue { get; set; }
    public UiPathJobRuntimeType? RuntimeType { get; set; }
    public string InputArguments
    
    
}

public class ArgumentConverter : JsonConverter<Dictionary<string, string>>
{
    public override void WriteJson(JsonWriter writer, Dictionary<string, string>? value, JsonSerializer serializer)
    {
        writer.WriteValue(JsonConvert.SerializeObject(value));
    }

    public override Dictionary<string, string>? ReadJson(JsonReader reader, Type objectType, Dictionary<string, string>? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        var jsonString = (string?) reader.Value;
        if (jsonString is null) return null;

        var valueJArray =  JArray.Parse(jsonString);

        var argumentDictionary = new Dictionary<string, string>();
        foreach (var token in valueJArray)
        {
            argumentDictionary[token.Value<string>]
        }
        
    }
}