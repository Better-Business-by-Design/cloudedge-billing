using AccountsReceivable.BL.Models.Json.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AccountsReceivable.BL.Models.Json;

public record UiPathJobSingleton(List<UiPathJobDto> Value);

/// <summary>
/// Represents a scheduled or manual execution of a process on a robot.
/// </summary>
/// <remarks>
/// Missing implementation of optional fields: Robot, Release, and Machine.
/// </remarks>
public record UiPathJobDto
{
    /// <summary>
    /// The unique job identifier.
    /// </summary>
    public Guid Key {get; set;}
    
    /// <summary>
    /// The date and time when the job execution started or null if the job hasn't started yet.
    /// </summary>
    public DateTime? StartTime {get; set;}
    
    /// <summary>
    /// The date and time when the job execution ended or null if the job hasn't ended yet.
    /// </summary>
    public DateTime? EndTime {get; set;}
    
    /// <summary>
    /// The state in which the job is.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public UiPathJobState State {get; set;}
    
    /// <summary>
    /// Execution priority.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public UiPathJobPriority JobPriority {get; set;}
    
    /// <summary>
    /// Value for more granular control over execution priority. Range 1 - 100.
    /// </summary>
    public int SpecificPriorityValue {get; set;}
    
    /// <summary>
    /// Job overwrites.
    /// </summary>
    public string? ResourceOverwrites {get; set;}
    
    /// <summary>
    /// The Source name of the job.
    /// </summary>
    public string Source {get; set;}
    
    /// <summary>
    /// The Source type of the job.
    /// </summary>
    public string SourceType {get; set;}
    
    /// <summary>
    /// The unique identifier grouping multiple jobs. It is usually generated when the job is created by a schedule.
    /// </summary>
    public string BatchExecutionKey {get; set;}
    
    /// <summary>
    /// Additional information about the current job.
    /// </summary>
    public string Info {get; set;}
    
    /// <summary>
    /// The date and time when the job was created.
    /// </summary>
    public DateTime CreationTime {get; set;}
    
    /// <summary>
    /// The Id of the schedule that started the job, or null if the job was started by the user.
    /// </summary>
    public int? StartingScheduleId {get; set;}
    
    /// <summary>
    /// The name of the release associated with the current name.
    /// </summary>
    public string ReleaseName {get; set;}
    
    /// <summary>
    /// The type of the job, Attended if started via the robot, Unattended otherwise.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public UiPathJobType Type {get; set;}
    
    /// <summary>
    /// Input parameters in JSON format to be passed to job execution.
    /// </summary>
    public string? InputArguments {get; set;}
    
    /// <summary>
    /// Output parameters in JSON format resulted from job execution.
    /// </summary>
    public string? OutputArguments {get; set;}
    
    /// <summary>
    /// The name of the machine where the Robot ran/is running the job.
    /// </summary>
    public string? HostMachineName {get; set;}
    
    /// <summary>
    /// The persistence instance id for a suspended job.
    /// </summary>
    public Guid? PersistenceId {get; set;}
    
    /// <summary>
    /// Distinguishes between multiple job suspend/resume cycles.
    /// </summary>
    public int? ResumeVersion {get; set;}
    
    /// <summary>
    /// No description given by swagger documentation.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public UiPathJobStopStrategy? StopStrategy {get; set;}
    
    /// <summary>
    /// The runtime type of the robot which can pick up the job
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public UiPathJobRuntimeType RuntimeType {get; set;}
    
    /// <summary>
    /// No description given by swagger documentation.
    /// </summary>
    public bool RequiresUserInteraction {get; set;}
    
    /// <summary>
    /// No description given by swagger documentation.
    /// </summary>
    public long ReleaseVersionId {get; set;}
    
    /// <summary>
    /// Path to the entry point workflow (XAML) that will be executed by the robot.
    /// </summary>
    /// <remarks>
    /// Max length: 512
    /// </remarks>
    public string EntryPointPath {get; set;}
    
    /// <summary>
    /// Id of the folder this job is part of.
    /// </summary>
    public long OrganizationUnitId {get; set;}
    
    /// <summary>
    /// Fully qualified name of the folder this job is part of.
    /// </summary>
    public string? OrganizationUnitFullyQualifiedName {get; set;}
    
    /// <summary>
    /// Reference identifier for the job.
    /// </summary>
    public string Reference {get; set;}
    
    /// <summary>
    /// No description given by swagger documentation.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public UiPathJobProcessType ProcessType {get; set;}
    
    /// <summary>
    /// Options to instruct the robot what profiling info to collect (code coverage, CPU / memory utilization, etc)
    /// </summary>
    public string? ProfilingOptions {get; set;}
    
    /// <summary>
    /// Flag for honoring initial machine and robot choice upon resumption of job if suspended.
    /// If set, the job will resume on the same robot-machine pair on which it initially ran.
    /// </summary>
    public bool ResumeOnSameContext {get; set;}
    
    /// <summary>
    /// The account under which the robot executor will run the job.
    /// </summary>
    public string LocalSystemAccount {get; set;}
    
    /// <summary>
    /// The orchestrator identity used to make API calls.
    /// </summary>
    public string? OrchestratorUserIdentity {get; set;}
    
    /// <summary>
    /// No description given by swagger documentation.
    /// </summary>
    public string RemoteControlAccess {get; set;}
    
    /// <summary>
    /// Expected running time in seconds.
    /// </summary>
    public long? MaxExpectedRunningTimeSeconds {get; set;}
    
    /// <summary>
    /// The type of the serverless job, RobotJob or Generic.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public UiPathServerlessJobType? ServerlessJobType {get; set;} 
    
    /// <summary>
    /// No description given by swagger documentation.
    /// </summary>
    public DateTime? ResumeTime {get; set;}
    
    /// <summary>
    /// No description given by swagger documentation.
    /// </summary>
    public DateTime LastModificationTime {get; set;}
    
    /// <summary>
    /// The unique job identifier, but as a number this time because why not have two...
    /// </summary>
    public long Id {get; set;}
    
}

public record UiPathJobStartRequestBody
{
    [JsonProperty(PropertyName = "startInfo")] 
    public UiPathJobStartInfo StartInfo { get; set; } = null!;
}

public static class UiPathJobStartInfoFactory
{
    public static UiPathJobStartRequestBody BuildJobStartInfo(UiPathProcessDto process, IJobArguments arguments)
    {
        return new UiPathJobStartRequestBody
        {
            StartInfo = new UiPathJobStartInfo(
                process.Key,
                "ModernJobsCount",
                1,
                JsonConvert.SerializeObject(arguments.GetArguments())
            )
        };

    }
}