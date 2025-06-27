using SaisApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaisApp.Services;

public interface IApplicantService
{
    Task<List<Applicant>> GetApplicantsAsync();

    Task<bool> AddApplicantAsync(CreateApplicantRequest request);
}
