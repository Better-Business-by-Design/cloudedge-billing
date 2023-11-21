using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AccountsReceivable.BL.Models.Json;

public record UiPathJobSingleton(List<UiPathJobDto> Value);

public record UiPathJobDto(
    string Key,
    DateTime? StartTime,
    DateTime? EndTime,
    string State,
    string JobPriority,
    string Source,
    string SourceType,
    string BatchExecutionKey,
    string? Info,
    DateTime CreationTime,
    int? StartingScheduleId,
    string ReleaseName,
    string Type,
    // InputArguments
    // OutputArguments
    // HostMachineName
    bool HasMediaRecorded,
    // PersistenceId
    // ResumeVersion
    // StopStrategy
    string RuntimeType,
    bool RequiresUserInteraction,
    int? ReleaseVersionId,
    // EntryPointPath
    int OrganizationUnitId,
    string? OrganizationUnitFullyQualifiedName,
    string Reference,
    string ProcessType,
    int Id
);

public record UiPathJobStartRequestBody
{
    [JsonPropertyName("startInfo")] 
    public UiPathJobStartInfo StartInfo { get; set; } = null!;
}

public record UiPathJobStartInfo(
    string ReleaseKey,
    string Strategy,
    int JobsCount,
    string? InputArguments
    );

public static class UiPathJobStartInfoFactory
{
    public static UiPathJobStartRequestBody BuildJobStartInfo(string releaseKey, string? strategy = null,
        int? jobsCount = null, Dictionary<string, string>? inputArguments = null)
    {
        return new UiPathJobStartRequestBody
        {
            StartInfo = new UiPathJobStartInfo(
                releaseKey,
                strategy ?? "ModernJobsCount",
                jobsCount ?? 1,
                JsonConvert.SerializeObject(inputArguments ?? new Dictionary<string, string>())
            )
        };

    }
}