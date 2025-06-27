using SaisApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SaisApp.Services;

public class LookupService : ILookupService
{
    private readonly HttpClient _httpClient;

    public LookupService()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri("https://visitors.boldet.com/") };
    }

    public async Task<List<Genders>> GetGendersAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Genders>>("api/looksup/getgenders");
    }

    public async Task<List<County>> GetCountiesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<County>>("api/looksup/getcounties");
    }

    public async Task<List<SubCounty>> GetSubCountiesAsync(int countyId)
    {
        return await _httpClient.GetFromJsonAsync<List<SubCounty>>($"api/looksup/getsubcounties?countyid={countyId}");
    }

    public async Task<List<ProgramsList>> GetProgramsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<ProgramsList>>("api/looksup/getprogrammes");
    }

    public async Task<List<MarriageStatus>> GetMaritualStatusListAsync()
    {
        List<MarriageStatus>? data = await _httpClient.GetFromJsonAsync<List<MarriageStatus>>("api/looksup/getmaritalstatuses");
        return data;
    }
       

    public async Task<List<Models.Location>> GetLocationsAsync(int subcountyid)
    {
        return await _httpClient.GetFromJsonAsync<List<Models.Location>>($"api/looksup/getlocations?subcountyId={subcountyid}");
    }

    public async Task<List<SubLocation>> GetSubLocationsAsync(int locationId)
    {
        return await _httpClient.GetFromJsonAsync<List<SubLocation>>($"api/looksup/getsublocations?locationId={locationId}");
    }

    public async Task<List<Village>> GetVillagesAsync(int sublocationId)
    {
        return await _httpClient.GetFromJsonAsync<List<Village>>($"api/looksup/getvillages?sublocationId={sublocationId}");
    }
}