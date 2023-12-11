using CloudEdgeBilling.BAL.Data;

namespace CloudEdgeBilling.API.Shared.DataRowChange;

public interface IDataRowChange
{
    Task ApplyChange();

    Task RevertChange();
}