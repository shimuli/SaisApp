using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaisApp.Models;

public class CreateApplicantRequest
{
    public CreateApplicant createApplicant { get; set; }
    public string phoneNumbersRaw { get; set; }
    public List<int> selectedProgrammeIds { get; set; }
    public OfficialApproval officialApproval { get; set; }
}

public class CreateApplicant
{
    public string firstName { get; set; }
    public string middleName { get; set; }
    public string lastName { get; set; }
    public string idNumber { get; set; }
    public DateTime dateOfBirth { get; set; }
    public string postalAddress { get; set; }
    public string physicalAddress { get; set; }
    public DateTime applicationDate { get; set; } = DateTime.UtcNow;
    public DateTime signedDate { get; set; } = DateTime.UtcNow;
    public int genderId { get; set; }
    public int maritalStatusId { get; set; }
    public int countyId { get; set; }
    public int subCountyId { get; set; }
    public int locationId { get; set; }
    public int subLocationId { get; set; }
    public int villageId { get; set; }
    public string formStatus { get; set; } = "Submitted";
    public bool declarationChecked { get; set; } = true;
    public string dataEnteredBy { get; set; } = "system_user";
}

public class OfficialApproval
{
    public string officerName { get; set; }
    public string designation { get; set; }
    public string signature { get; set; }
    public DateTime informationCollectedDate { get; set; } = DateTime.UtcNow;
    public int applicantId { get; set; } = 0;
}