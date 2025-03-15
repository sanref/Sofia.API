using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using Sofia.API.Models;

namespace Sofia.API.Models;

public partial class ASSET
{
    public int asset_id { get; set; }

    public string asset_name { get; set; } = null!;

    public string symbol { get; set; } = null!;

    public string? company { get; set; }

    public virtual ICollection<OPERATION> OPERATIONs { get; set; } = new List<OPERATION>();
}


