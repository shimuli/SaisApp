using SaisApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Location = SaisApp.Models.Location;

namespace SaisApp.Services;

public interface ILookupService
{
    Task<List<Genders>> GetGendersAsync();

    Task<List<MarriageStatus>> GetMaritualStatusListAsync();
    Task<List<County>> GetCountiesAsync();
    Task<List<SubCounty>> GetSubCountiesAsync(int countyId);

    Task<List<Location>>? GetLocationsAsync(int subcountyid);

    Task<List<SubLocation>> GetSubLocationsAsync(int locationId);

    Task<List<Village>> GetVillagesAsync(int sublocationId);
    Task<List<ProgramsList>> GetProgramsAsync();
}
