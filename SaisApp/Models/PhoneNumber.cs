using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaisApp.Models;


public class PhoneNumber
{
    [JsonProperty("id")]
    public int? Id { get; set; }

    [JsonProperty("number")]
    public string? Number { get; set; }
}