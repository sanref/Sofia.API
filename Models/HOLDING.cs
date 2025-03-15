using System;
using System.Collections.Generic;

namespace Sofia.API.Models;

public partial class HOLDING
{
    public int holding_id { get; set; }

    public int asset_id { get; set; }

    public decimal quantity { get; set; }

    public decimal average_price { get; set; }

    public decimal? average_days { get; set; }
}
