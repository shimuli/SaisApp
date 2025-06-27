using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaisApp.Models;

public class MarriageStatus
{
    [JsonProperty("maritalStatusId")]
    public int? MaritalStatusId { get; set; }

    [JsonProperty("value")]
    public string Value { get; set; } = null!;
}