﻿namespace AccountsReceivable.BL.Models.Application;

public class Plant
{
    public string Name { get; set; } = null!;
    
    public ushort FarmStoreId { get; set; }

    public string MeatworkName { get; set; } = null!;
    public virtual Meatwork Meatwork { get; set; } = null!;
}