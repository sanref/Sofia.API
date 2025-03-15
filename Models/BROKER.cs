using System;
using System.Collections.Generic;

namespace Sofia.API.Models;

public partial class BROKER
{
    public int broker_id { get; set; }

    public string? broker_name { get; set; }

    public byte[]? logo { get; set; }

    public virtual ICollection<OPERATION> OPERATIONs { get; set; } = new List<OPERATION>();
}
