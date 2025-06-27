using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaisApp.Models;

public class Applicant
{
    [JsonProperty("applicantId")]
    public int? ApplicantId { get; set; }

    [JsonProperty("firstName")]
    public string? FirstName { get; set; }

    [JsonProperty("middleName")]
    public string? MiddleName { get; set; }

    [JsonProperty("lastName")]
    public string? LastName { get; set; }

    [JsonProperty("idNumber")]
    public string? IdNumber { get; set; }

    [JsonProperty("dateOfBirth")]
    public DateTime? DateOfBirth { get; set; }

    [JsonProperty("postalAddress")]
    public string? PostalAddress { get; set; }

    [JsonProperty("physicalAddress")]
    public string? PhysicalAddress { get; set; }

    [JsonProperty("applicationDate")]
    public DateTime? ApplicationDate { get; set; }

    [JsonProperty("signedDate")]
    public DateTime? SignedDate { get; set; }

    [JsonProperty("genderId")]
    public int? GenderId { get; set; }

    [JsonProperty("maritalStatusId")]
    public int? MaritalStatusId { get; set; }

    [JsonProperty("countyId")]
    public int? CountyId { get; set; }

    [JsonProperty("subCountyId")]
    public int? SubCountyId { get; set; }

    [JsonProperty("locationId")]
    public int? LocationId { get; set; }

    [JsonProperty("subLocationId")]
    public int? SubLocationId { get; set; }

    [JsonProperty("villageId")]
    public int? VillageId { get; set; }

    [JsonProperty("formStatus")]
    public string? FormStatus { get; set; }

    [JsonProperty("declarationChecked")]
    public bool? DeclarationChecked { get; set; }

    [JsonProperty("dataEnteredBy")]
    public string? DataEnteredBy { get; set; }

    [JsonProperty("applicantGender")]
    public ApplicantGender? ApplicantGender { get; set; }

    [JsonProperty("maritalStatus")]
    public MaritalStatus? MaritalStatus { get; set; }

    [JsonProperty("phoneNumbers")]
    public List<PhoneNumber>? PhoneNumbers { get; set; }

    [JsonProperty("village")]
    public Village? Village { get; set; }

    [JsonProperty("subLocation")]
    public SubLocation? SubLocation { get; set; }

    [JsonProperty("location")]
    public Location? Location { get; set; }

    [JsonProperty("subCounty")]
    public SubCounty? SubCounty { get; set; }

    [JsonProperty("county")]
    public County? County { get; set; }


    public int Age => CalculateAge(DateOfBirth);

    private int CalculateAge(DateTime? dob)
    {
        if (dob == null)
            return 0; // or throw an exception if DOB is required

        var today = DateTime.Today;
        var age = today.Year - dob.Value.Year;

        if (dob.Value.Date > today.AddYears(-age))
            age--;

        return age;
    }
}
