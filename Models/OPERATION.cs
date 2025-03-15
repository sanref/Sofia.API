using System;
using System.Collections.Generic;

namespace Sofia.API.Models;

public partial class OPERATION
{
    public int operation_id { get; set; }

    public DateTime date { get; set; }

    public string type { get; set; } = null!;

    public int? broker_id { get; set; }

    public decimal? quantity { get; set; }

    public decimal? price { get; set; }

    public string? currency { get; set; }

    public int? asset_id { get; set; }

    public virtual ASSET? asset { get; set; }

    public virtual BROKER? broker { get; set; }
}
