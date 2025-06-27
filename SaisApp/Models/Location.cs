using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaisApp.Models;

public class Location
{
    [JsonProperty("locationId")]
    public int LocationId { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("subCountyId")]
    public int? SubCountyId { get; set; }
}