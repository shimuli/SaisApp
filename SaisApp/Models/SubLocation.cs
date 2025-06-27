using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaisApp.Models;

public class SubLocation
{
    [JsonProperty("subLocationId")]
    public int SubLocationId { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }
}
