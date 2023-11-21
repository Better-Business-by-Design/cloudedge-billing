namespace AccountsReceivable.BL.Models.Json;

public record UiPathJobStatusCollection(
    List<UiPathJobStatusDto> Value
    );

public record UiPathJobStatusDto(
    string Key,
    DateTime? StartTime,
    DateTime? EndTime,
    string State,
    string JobPriority,
    int? SpecificPriorityValue,
    // ResourceOverwrites
    string Source,
    string SourceType,
    string BatchExecutionKey,
    string? Info,
    DateTime CreationTime,
    int? StartingScheduleId,
    string ReleaseName,
    string Type,
    string? InputArguments,
    string? OutputArguments,
    string? HostMachineName,
    bool HasMediaRecorded,
    bool HasVideoRecorded,
    int? PersistenceId,
    string? ResumeVersion,
    string? StopStrategy,
    string RuntimeType,
    bool RequiresUserInteraction,
    int? ReleaseVersionId,
    string? EntryPointPath,
    int OrganizationUnitId,
    string? OrganizationUnitFullyQualifiedName,
    string Reference,
    string ProcessType,
    string? ProfilingOptions,
    bool ResumeOnSameContext,
    string LocalSystemAccount,
    string? OrchestratorUserIdentity,
    string RemoteControlAccess,
    int? MaxExpectedRunningTimeSeconds,
    string? ServerlessJobType,
    DateTime? ResumeTime,
    DateTime LastModificationTime,
    int Id
    );