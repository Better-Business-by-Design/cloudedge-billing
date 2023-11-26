// ReSharper disable global InconsistentNaming
using System.Globalization;
using AccountsReceivable.BL.Models.Application;

namespace AccountsReceivable.BL.Models.Json;

public interface IJobArguments
{
    public Dictionary<string, string> GetArguments();
}

public class CloudEdgeBillingJobArguments : IJobArguments, IDataRow
{
    public static string TypeName => "Cloud Edge Billing Job Arguments";

    public DateTime? in_StartRangeDateTime { get; set; } =
        new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1);

    public Dictionary<string, string> GetArguments()
    {
        var startRangeDateTime =
            in_StartRangeDateTime ?? throw new ArgumentNullException(nameof(in_StartRangeDateTime));
        return new Dictionary<string, string>()
        {
            { nameof(in_StartRangeDateTime), startRangeDateTime.ToString(CultureInfo.InvariantCulture)}
        };
    }
} 