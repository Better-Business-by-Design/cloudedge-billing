using CloudEdgeBilling.BL.Models.Application;
using Microsoft.AspNetCore.Components;

namespace CloudEdgeBilling.API.Shared.EditTemplates.Select;

/// <inheritdoc />
public abstract partial class DataRowSelectEditor<TGrid, TValue> : DataRowEditor<TGrid, TValue> where TGrid : IDataRow
{
    /// <summary>
    /// The set of all possible/valid values for the user to select between.
    /// </summary>
    /// <remarks>
    /// Directly passing in the <c>DbSet</c> is not recommended as it will cause an SQL query to be sent for every
    /// single one of these selectors. A more efficient solution is to capture the DbSet as an immutable
    /// list on component initialization and then feed that list to this class.
    /// That way the SQL query will only be sent once.
    /// </remarks>
    [Parameter]
    public IEnumerable<TValue> Values { get; set; } = default!;
    
    /// <summary>
    /// Whether or not the MudSelect should be clearable.
    /// </summary>
    /// <remarks>
    /// Setting this to true for an inheritor with a non-nullable <c>TValue</c> type will likely cause the website to
    /// crash.
    /// </remarks>
    [Parameter]
    public virtual bool Clearable { get; set; }
    
    /// <summary>
    /// Virtual method to allow for inheritors to override the generic ToString() representation of values in the
    /// selector.
    /// </summary>
    /// <param name="value">The value visible to the user in the selector.</param>
    /// <returns>A human-readable representation of the value that distinguishes it from other similar objects.</returns>
    /// <remarks>Rather than overriding this method, overriding ToString() in the DTO class is probably more efficient.</remarks>
    protected virtual string GetPrintValue(TValue? value)
    {
        return value?.ToString() ?? string.Empty;
    }
    
}

