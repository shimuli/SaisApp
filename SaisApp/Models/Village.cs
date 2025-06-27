using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaisApp.Models;

public class Village
{
    [JsonProperty("villageId")]
    public int? VillageId { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }
}