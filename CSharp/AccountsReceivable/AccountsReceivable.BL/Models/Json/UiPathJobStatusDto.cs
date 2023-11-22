using AccountsReceivable.BL.Models.Json.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AccountsReceivable.BL.Models.Json;

public record UiPathJobStatusCollection(
    List<UiPathJobStatusDto> Value
    );

public record UiPathJobStatusDto
{
    public string Key {get; set;}
    public     DateTime? StartTime {get; set;}
    public DateTime? EndTime {get; set;}
    
    [JsonProperty(PropertyName = "State"), JsonConverter(typeof(StringEnumConverter))]
    public UiPathJobState State {get; set;}
    
    public string JobPriority {get; set;}
    public int? SpecificPriorityValue {get; set;}
    // public ResourceOverwrites {get; set;}
    public string Source {get; set;}
    public string SourceType {get; set;}
    public string BatchExecutionKey {get; set;}
    public string? Info {get; set;}
    public DateTime CreationTime {get; set;}
    public int? StartingScheduleId {get; set;}
    public string ReleaseName {get; set;}
    public string Type {get; set;}
    public string? InputArguments {get; set;}
    public string? OutputArguments {get; set;}
    public string? HostMachineName {get; set;}
    public bool HasMediaRecorded {get; set;}
    public bool HasVideoRecorded {get; set;}
    public int? PersistenceId {get; set;}
    public string? ResumeVersion {get; set;}
    public string? StopStrategy {get; set;}
    public string RuntimeType {get; set;}
    public bool RequiresUserInteraction {get; set;}
    public int? ReleaseVersionId {get; set;}
    public string? EntryPointPath {get; set;}
    public int OrganizationUnitId {get; set;}
    public string? OrganizationUnitFullyQualifiedName {get; set;}
    public string Reference {get; set;}
    public string ProcessType {get; set;}
    public string? ProfilingOptions {get; set;}
    public bool ResumeOnSameContext {get; set;}
    public string LocalSystemAccount {get; set;}
    public string? OrchestratorUserIdentity {get; set;}
    public string RemoteControlAccess {get; set;}
    public int? MaxExpectedRunningTimeSeconds {get; set;}
    public string? ServerlessJobType {get; set;} 
    public DateTime? ResumeTime {get; set;}
    public DateTime LastModificationTime {get; set;}
    public int Id {get; set;}
}
