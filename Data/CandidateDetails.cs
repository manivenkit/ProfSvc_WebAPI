#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           CandidateDetails.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:12
// *****************************************/

#endregion

namespace ProfSvc_WebAPI.Data;

public class CandidateDetails
{
    #region Constructors

    /// <summary>
    /// </summary>
    public CandidateDetails()
    {
        ClearData();
    }

    /// <summary>
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="middleName"></param>
    /// <param name="lastName"></param>
    /// <param name="address1"></param>
    /// <param name="address2"></param>
    /// <param name="city"></param>
    /// <param name="stateID"></param>
    /// <param name="zipCode"></param>
    /// <param name="email"></param>
    /// <param name="phone1"></param>
    /// <param name="phone2"></param>
    /// <param name="phone3"></param>
    /// <param name="phoneExt"></param>
    /// <param name="linkedIn"></param>
    /// <param name="facebook"></param>
    /// <param name="twitter"></param>
    /// <param name="title"></param>
    /// <param name="eligibilityID"></param>
    /// <param name="relocate"></param>
    /// <param name="background"></param>
    /// <param name="jobOptions"></param>
    /// <param name="taxTerm"></param>
    /// <param name="originalResume"></param>
    /// <param name="formattedResume"></param>
    /// <param name="textResume"></param>
    /// <param name="keywords"></param>
    /// <param name="communication"></param>
    /// <param name="rateCandidate"></param>
    /// <param name="rateNotes"></param>
    /// <param name="mpc"></param>
    /// <param name="mpcNotes"></param>
    /// <param name="experienceID"></param>
    /// <param name="hourlyRate"></param>
    /// <param name="hourlyRateHigh"></param>
    /// <param name="salaryHigh"></param>
    /// <param name="salaryLow"></param>
    /// <param name="relocationNotes"></param>
    /// <param name="securityNotes"></param>
    /// <param name="refer"></param>
    /// <param name="referAccountManager"></param>
    /// <param name="eeo"></param>
    /// <param name="eeoFile"></param>
    /// <param name="summary"></param>
    /// <param name="googlePlus"></param>
    /// <param name="created"></param>
    /// <param name="updated"></param>
    ///// <param name="eligibility"></param>
    ///// <param name="state"></param>
    ///// <param name="experience"></param>
    ///// <param name="taxTermDescription"></param>
    ///// <param name="jobOptionDescription"></param>
    /// <param name="candidateID"></param>
    /// <param name="status"></param>
    public CandidateDetails(string firstName, string middleName, string lastName, string address1, string address2, string city, int stateID, string zipCode,
                            string email,
                            string phone1, string phone2, string phone3, string phoneExt, string linkedIn, string facebook, string twitter, string title,
                            int eligibilityID,
                            bool relocate, bool background, string jobOptions, string taxTerm, string originalResume, string formattedResume, string textResume,
                            string keywords, string communication, byte rateCandidate, string rateNotes, bool mpc, string mpcNotes, int experienceID,
                            decimal hourlyRate,
                            decimal hourlyRateHigh, decimal salaryHigh, decimal salaryLow, string relocationNotes, string securityNotes, bool refer,
                            string referAccountManager,
                            bool eeo, string eeoFile, string summary, string googlePlus, string created, string updated, int candidateID, string status)
        /*, string eligibility, string state, string experience, string taxTermDescription, string jobOptionDescription*/
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Address1 = address1;
        Address2 = address2;
        City = city;
        StateID = stateID;
        ZipCode = zipCode;
        Email = email;
        Phone1 = phone1;
        Phone2 = phone2;
        Phone3 = phone3;
        PhoneExt = phoneExt;
        LinkedIn = linkedIn;
        Facebook = facebook;
        Twitter = twitter;
        Title = title;
        EligibilityID = eligibilityID;
        Relocate = relocate;
        Background = background;
        JobOptions = jobOptions;
        TaxTerm = taxTerm;
        OriginalResume = originalResume;
        FormattedResume = formattedResume;
        TextResume = textResume;
        Keywords = keywords;
        Communication = communication;
        RateCandidate = rateCandidate;
        RateNotes = rateNotes;
        Mpc = mpc;
        MpcNotes = mpcNotes;
        ExperienceID = experienceID;
        HourlyRate = hourlyRate;
        HourlyRateHigh = hourlyRateHigh;
        SalaryHigh = salaryHigh;
        SalaryLow = salaryLow;
        RelocationNotes = relocationNotes;
        SecurityNotes = securityNotes;
        Refer = refer;
        ReferAccountManager = referAccountManager;
        Eeo = eeo;
        EeoFile = eeoFile;
        Summary = summary;
        GooglePlus = googlePlus;
        Created = created;
        Updated = updated;
        //Eligibility = eligibility;
        //State = state;
        //Experience = experience;
        //TaxTermDescription = taxTermDescription;
        //JobOptionDescription = jobOptionDescription;
        CandidateID = candidateID;
        Status = status;
    }

    #endregion

    #region Properties

    /// <summary>
    /// </summary>
    public bool Background
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public bool Eeo
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public bool Mpc
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public bool Refer
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public bool Relocate
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public decimal HourlyRate
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public decimal HourlyRateHigh
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public decimal SalaryHigh
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public decimal SalaryLow
    {
        get;
        set;
    }

    ///// <summary>
    ///// </summary>
    //public string Eligibility
    //{
    //	get;
    //	set;
    //}

    /// <summary>
    /// </summary>
    public int EligibilityID
    {
        get;
        set;
    }

    ///// <summary>
    ///// </summary>
    //public string Experience
    //{
    //	get;
    //	set;
    //}

    /// <summary>
    /// </summary>
    public int ExperienceID
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public int RateCandidate
    {
        get;
        set;
    }

    ///// <summary>
    ///// </summary>
    //public string State
    //{
    //	get;
    //	set;
    //}

    /// <summary>
    /// </summary>
    public int StateID
    {
        get;
        set;
    }

    public int CandidateID
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string Address1
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>

    public string Address2
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string City
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string Communication
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string Created
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string EeoFile
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string Email
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string Facebook
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string FirstName
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string FormattedResume
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string GooglePlus
    {
        get;
        set;
    }

    //public string JobOptionDescription
    //{
    //	get;
    //	set;
    //}

    /// <summary>
    /// </summary>
    public string JobOptions
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string Keywords
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string LastName
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string LinkedIn
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string MiddleName
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string MpcNotes
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string OriginalResume
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string Phone1
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string Phone2
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string Phone3
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string PhoneExt
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string RateNotes
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string ReferAccountManager
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string RelocationNotes
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string SecurityNotes
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string Summary
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string TaxTerm
    {
        get;
        set;
    }

    //public string TaxTermDescription
    //{
    //	get;
    //	set;
    //}

    /// <summary>
    /// </summary>
    public string TextResume
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string Title
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string Twitter
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string Updated
    {
        get;
        set;
    }

    public string Status
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>

    public string ZipCode
    {
        get;
        set;
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Creates a shallow copy of the current object.
    /// </summary>
    /// <returns></returns>
    public CandidateDetails Copy() => (CandidateDetails)MemberwiseClone();

    public void ClearData()
    {
        FirstName = "";
        MiddleName = "";
        LastName = "";
        Address1 = "";
        Address2 = "";
        City = "";
        StateID = 0;
        ZipCode = "";
        Email = "";
        Phone1 = "";
        Phone2 = "";
        Phone3 = "";
        PhoneExt = "";
        LinkedIn = "";
        Facebook = "";
        Twitter = "";
        Title = "";
        EligibilityID = 0;
        Relocate = false;
        Background = false;
        JobOptions = "";
        TaxTerm = "";
        OriginalResume = "";
        FormattedResume = "";
        TextResume = "";
        Keywords = "";
        Communication = "";
        RateCandidate = 3;
        RateNotes = "";
        Mpc = false;
        MpcNotes = "";
        ExperienceID = 0;
        HourlyRate = decimal.Zero;
        HourlyRateHigh = decimal.Zero;
        SalaryHigh = decimal.Zero;
        SalaryLow = decimal.Zero;
        RelocationNotes = "";
        SecurityNotes = "";
        Refer = false;
        ReferAccountManager = "";
        Eeo = false;
        EeoFile = "";
        Summary = "";
        GooglePlus = "";
        Created = "";
        Updated = "";
        //Eligibility = "";
        //State = "";
        //Experience = "";
        //TaxTermDescription = "";
        //JobOptionDescription = "";
        CandidateID = 0;
        Status = "AVL";
    }

    #endregion
}