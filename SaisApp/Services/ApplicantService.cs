using Newtonsoft.Json;
using SaisApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SaisApp.Services;

public class ApplicantService : IApplicantService
{
    private readonly HttpClient _httpClient;
    private const string _baseUrl = "https://visitors.boldet.com";

    public ApplicantService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(_baseUrl)
        };
    }

    public async Task<List<Applicant>> GetApplicantsAsync()
    {
        var response = await _httpClient.GetAsync("/api/register/getapplications");

        if (!response.IsSuccessStatusCode)
            throw new Exception("Failed to fetch applicants");

        return await response.Content.ReadFromJsonAsync<List<Applicant>>();
    }

    public async Task<bool> AddApplicantAsync(CreateApplicantRequest request)
    {
        var url = "api/register/registerapplicant";

        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to add applicant: {error}");
        }

        return true;
    }
}