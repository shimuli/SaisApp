using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaisApp.Models;

public class Genders
{
    [JsonProperty("id")]
    public int? Id { get; set; }

    [JsonProperty("value")]
    public string Value { get; set; } = null!;
}