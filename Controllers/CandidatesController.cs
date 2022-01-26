#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           CandidatesController.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 21:39
// Last Updated On:     01-04-2022 16:11
// *****************************************/

#endregion

#region Using

//using Sovren;

#endregion

namespace ProfSvc_WebAPI.Controllers;

/// <summary>   A controller for handling candidates. </summary>
/// <remarks>   Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily, 03-01-2022. </remarks>
[ApiController, Route("api/[controller]")]
public class CandidatesController : ControllerBase
{
    #region Constructors

    /// <summary>   Constructor. </summary>
    /// <remarks>   Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily, 03-01-2022. </remarks>
    /// <param name="configuration">    The configuration. </param>
    /// <param name="env">              The environment. </param>
    public CandidatesController(IConfiguration configuration, IWebHostEnvironment env)
    {
        _config = configuration;
        _hostingEnvironment = env;
    }

    #endregion

    #region Fields

    /// <summary>
    ///     (Immutable) the configuration.
    /// </summary>
    private readonly IConfiguration _config;

    /// <summary>
    ///     (Immutable) the hosting environment.
    /// </summary>
    private readonly IWebHostEnvironment _hostingEnvironment;

    #endregion

    #region Methods

    [HttpGet("GetCandidateDetails")]
    public async Task<ActionResult<Dictionary<string, object>>> GetCandidateDetails([FromQuery] int candidateID)
    {
        using SqlConnection _connection = new(_config.GetConnectionString("DBConnect"));
        CandidateDetails _candidate = null;
        string _candRating = "", _candMPC = "";

        using SqlCommand _command = new("GetDetailCandidate", _connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        _command.Int("@CandidateID", candidateID);

        _connection.Open();
        using SqlDataReader _reader = await _command.ExecuteReaderAsync();
        if (_reader.HasRows) //Candidate Details
        {
            _reader.Read();
            _candidate = new(_reader.NString(0), _reader.NString(1), _reader.NString(2), _reader.NString(3), _reader.NString(4),
                             _reader.NString(5), _reader.GetInt32(6), _reader.NString(7), _reader.NString(8), _reader.NString(9),
                             _reader.NString(10), _reader.NString(11), _reader.NInt16(12).ToString(), _reader.NString(13), _reader.NString(14),
                             _reader.NString(15), _reader.NString(16), _reader.GetInt32(17), _reader.GetBoolean(18), _reader.GetBoolean(19),
                             _reader.NString(20), _reader.NString(21), _reader.NString(22), _reader.NString(23), _reader.NString(24),
                             _reader.NString(25), _reader.NString(26), _reader.GetByte(27), _reader.NString(28), _reader.GetBoolean(29),
                             _reader.NString(30), _reader.GetInt32(31), _reader.GetDecimal(32), _reader.GetDecimal(33), _reader.GetDecimal(34),
                             _reader.GetDecimal(35), _reader.NString(36), _reader.NString(37), _reader.GetBoolean(38), _reader.NString(39),
                             _reader.GetBoolean(40), _reader.NString(41), _reader.NString(42), _reader.NString(43), _reader.NString(44),
                             _reader.NString(45), candidateID, _reader.NString(46));
            _candRating = _reader.NString(28);
            _candMPC = _reader.NString(30);
        }

        _reader.NextResult(); //Notes
        List<CandidateNotes> _notes = new();
        while (_reader.Read())
        {
            _notes.Add(new(_reader.GetInt32(0), _reader.GetDateTime(1), _reader.GetString(2), _reader.GetString(3)));
        }

        _reader.NextResult(); //Skills
        List<CandidateSkills> _skills = new();
        while (_reader.Read())
        {
            _skills.Add(new(_reader.GetInt32(0), _reader.GetString(1), _reader.GetInt16(2), _reader.GetInt16(3), _reader.GetString(4)));
        }

        _reader.NextResult(); //Education
        List<CandidateEducation> _education = new();
        while (_reader.Read())
        {
            _education.Add(new(_reader.GetInt32(0), _reader.GetString(1), _reader.GetString(2), _reader.GetString(3), _reader.GetString(4),
                               _reader.GetString(5), _reader.GetString(6)));
        }
        ;
        _reader.NextResult(); //Experience
        List<CandidateExperience> _experience = new();
        while (_reader.Read())
        {
            _experience.Add(new(_reader.GetInt32(0), _reader.GetString(1), _reader.GetString(2), _reader.GetString(3), _reader.GetString(4),
                                _reader.GetString(5), _reader.GetString(6), _reader.GetString(7)));
        }

        _reader.NextResult(); //Activity
        List<CandidateActivity> _activity = new();
        while (_reader.Read())
        {
            _activity.Add(new(_reader.GetString(0), _reader.GetDateTime(1), _reader.GetString(2), _reader.GetInt32(3), _reader.GetInt32(4),
                              _reader.GetString(5), _reader.GetString(6), _reader.GetInt32(7), _reader.GetBoolean(8), _reader.GetString(9),
                              _reader.GetString(10), _reader.GetString(11), _reader.GetBoolean(12), _reader.GetString(13), _reader.GetInt32(14),
                              _reader.GetString(15), _reader.GetInt32(16)));
        }

        //Candidate Rating
        List<CandidateRating> _rating = new();
        if (!_candRating.NullOrWhiteSpace())
        {
            string[] _ratingArray = _candRating.Split('?');
            _rating.AddRange(_ratingArray
                            .Select(str => new
                            {
                                _str = str,
                                _innerArray = str.Split('^')
                            })
                            .Where(t => t._innerArray.Length == 4)
                            .Select(t => new CandidateRating(t._innerArray[0].ToDateTime(), t._innerArray[1], t._innerArray[2].ToByte(),
                                                             t._innerArray[3])));

            _rating = _rating.OrderByDescending(x => x.Date).ToList();
        }

        //Candidate MPC
        List<CandidateMPC> _mpc = new();
        if (_candMPC.NullOrWhiteSpace())
        {
            return new Dictionary<string, object>
                   {
                       {
                           "Candidate", _candidate
                       },
                       {
                           "Notes", _notes
                       },
                       {
                           "Skills", _skills
                       },
                       {
                           "Education", _education
                       },
                       {
                           "Experience", _experience
                       },
                       {
                           "Activity", _activity
                       },
                       {
                           "Rating", _rating
                       },
                       {
                           "MPC", _mpc
                       },
                       {
                           "RatingMPC", null
                       }
                   };
        }

        string[] _mpcArray = _candMPC.Split('?');
        _mpc.AddRange(_mpcArray
                     .Select(str => new
                     {
                         _str = str,
                         _innerArray = str.Split('^')
                     })
                     .Where(t => t._innerArray.Length == 4)
                     .Select(t => new CandidateMPC(t._innerArray[0].ToDateTime(), t._innerArray[1], t._innerArray[2].ToBoolean(),
                                                   t._innerArray[3])));

        _mpc = _mpc.OrderByDescending(x => x.Date).ToList();

        int _ratingFirst = 0;
        bool _mpcFirst = false;
        string _ratingComments = "", _mpcComments = "";
        if (!_candRating.NullOrWhiteSpace())
        {
            CandidateRating _ratingFirstCandidate = _rating.FirstOrDefault();
            if (_ratingFirstCandidate != null)
            {
                _ratingFirst = _ratingFirstCandidate.Rating;
                _ratingComments = _ratingFirstCandidate.Comments;
            }
        }

        if (!_candMPC.NullOrWhiteSpace())
        {
            CandidateMPC _mpcFirstCandidate = _mpc.FirstOrDefault();
            if (_mpcFirstCandidate != null)
            {
                _mpcFirst = _mpcFirstCandidate.MPC;
                _mpcComments = _mpcFirstCandidate.Comments;
            }
        }

        CandidateRatingMPC _ratingMPC = new(candidateID, _ratingFirst, _ratingComments, _mpcFirst, _mpcComments);

        return new Dictionary<string, object>
               {
                   {
                       "Candidate", _candidate
                   },
                   {
                       "Notes", _notes
                   },
                   {
                       "Skills", _skills
                   },
                   {
                       "Education", _education
                   },
                   {
                       "Experience", _experience
                   },
                   {
                       "Activity", _activity
                   },
                   {
                       "Rating", _rating
                   },
                   {
                       "MPC", _mpc
                   },
                   {
                       "RatingMPC", _ratingMPC
                   }
               };
    }

    /// <summary>
    ///     Gets the Grid Candidates List.
    /// </summary>
    /// <remarks>
    ///     Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily, 03-01-2022.
    /// </remarks>
    /// <param name="count">
    ///     (Optional)
    /// </param>
    /// <param name="page">
    ///     (Optional)
    /// </param>
    /// <param name="sortRow">
    ///     (Optional)
    /// </param>
    /// <param name="sortOrder">
    ///     (Optional)
    /// </param>
    /// <param name="getStates">
    ///     (Optional)
    /// </param>
    /// <param name="name">
    ///     (Optional)
    /// </param>
    /// <returns>
    ///     JSON containing JobOptions object and Count.
    /// </returns>
    [HttpGet("GetGridCandidates")]
    public ActionResult<Dictionary<string, object>> GetGridCandidates(int count = 25, int page = 1, int sortRow = 1, int sortOrder = 0, bool getStates = false,
                                                                      string name = "")
    {
        using SqlConnection _connection = new(_config.GetConnectionString("DBConnect"));
        List<Candidates> _candidates = new();
        using SqlCommand _command = new("GetGridCandidates", _connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        _command.Int("@Count", count);
        _command.Int("@Page", page);
        _command.Int("@SortRow", sortRow);
        _command.TinyInt("@SortOrder", sortOrder);
        _command.Bit("@GetStates", getStates);
        _command.Varchar("@Name", 255, name);

        _connection.Open();
        using SqlDataReader _reader = _command.ExecuteReader();

        _reader.Read();
        int _count = _reader.GetInt32(0);

        _reader.NextResult();

        while (_reader.Read())
        {
            _candidates.Add(new(_reader.GetInt32(0), _reader.GetString(1), _reader.GetString(2), _reader.GetString(3), _reader.GetString(4),
                                _reader.GetString(5), _reader.GetString(6), _reader.GetBoolean(7), _reader.GetByte(8), _reader.GetBoolean(9),
                                _reader.GetBoolean(10)));
        }

        List<IntValues> _states = new();
        List<IntValues> _eligibility = new();
        List<IntValues> _experience = new();
        List<KeyValues> _taxTerms = new();
        List<KeyValues> _jobOptions = new();
        List<KeyValues> _statusCodes = new();

        _reader.NextResult();
        if (_reader.HasRows)
        {
            while (_reader.Read())
            {
                _states.Add(new(_reader.GetInt32(0), _reader.GetString(1)));
            }
        }

        _reader.NextResult();
        if (_reader.HasRows)
        {
            while (_reader.Read())
            {
                _eligibility.Add(new(_reader.GetInt32(0), _reader.GetString(1)));
            }
        }

        _reader.NextResult();
        if (_reader.HasRows)
        {
            while (_reader.Read())
            {
                _experience.Add(new(_reader.GetInt32(0), _reader.GetString(1)));
            }
        }

        _reader.NextResult();
        if (_reader.HasRows)
        {
            while (_reader.Read())
            {
                _taxTerms.Add(new(_reader.GetString(0), _reader.GetString(1)));
            }
        }

        _reader.NextResult();
        if (_reader.HasRows)
        {
            while (_reader.Read())
            {
                _jobOptions.Add(new(_reader.GetString(0), _reader.GetString(1)));
            }
        }

        _reader.NextResult();
        if (_reader.HasRows)
        {
            while (_reader.Read())
            {
                _statusCodes.Add(new(_reader.GetString(0), _reader.GetString(1)));
            }
        }

        return new Dictionary<string, object>
               {
                   {
                       "Candidates", _candidates
                   },
                   {
                       "Count", _count
                   },
                   {
                       "States", _states
                   },
                   {
                       "Eligibility", _eligibility
                   },
                   {
                       "Experience", _experience
                   },
                   {
                       "TaxTerms", _taxTerms
                   },
                   {
                       "JobOptions", _jobOptions
                   },
                   {
                       "StatusCodes", _statusCodes
                   }
               };
    }

    /// <summary>
    ///     Saves the AdminList object.
    /// </summary>
    /// <remarks>
    ///     Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily, 03-01-2022.
    /// </remarks>
    /// <param name="candidateDetails">
    ///     Object of type AdminList.
    /// </param>
    /// <returns>
    ///     An ActionResult&lt;int&gt;
    /// </returns>
    [HttpPost("SaveCandidate")]
    public ActionResult<int> SaveCandidate(CandidateDetails candidateDetails)
    {
        if (candidateDetails == null)
        {
            return -1;
        }
        using SqlConnection _con = new(_config.GetConnectionString("DBConnect"));
        _con.Open();
        int _returnCode = 0;
        try
        {
            using SqlCommand _command = new("SaveCandidate", _con)
            {
                CommandType = CommandType.StoredProcedure
            };
            _command.Int("@ID", candidateDetails.CandidateID, true);
            _command.Varchar("@FirstName", 50, candidateDetails.FirstName);
            _command.Varchar("@MiddleName", 50, candidateDetails.MiddleName);
            _command.Varchar("@LastName", 50, candidateDetails.LastName);
            _command.Varchar("@Title", 50, candidateDetails.Title);
            _command.Int("@Eligibility", candidateDetails.EligibilityID);
            _command.Decimal("@HourlyRate", 6, 2, candidateDetails.HourlyRate);
            _command.Decimal("@HourlyRateHigh", 6, 2, candidateDetails.HourlyRateHigh);
            _command.Decimal("@SalaryLow", 9, 2, candidateDetails.SalaryLow);
            _command.Decimal("@SalaryHigh", 9, 2, candidateDetails.SalaryHigh);
            _command.Int("@Experience", candidateDetails.ExperienceID);
            _command.Bit("@Relocate", candidateDetails.Relocate);
            _command.Varchar("@JobOptions", 50, candidateDetails.JobOptions);
            _command.Char("@Communication", 1, candidateDetails.Communication);
            _command.Varchar("@TextResume", -1, candidateDetails.TextResume);
            _command.Varchar("@OriginalResume", 255, candidateDetails.OriginalResume);   //TODO:
            _command.Varchar("@FormattedResume", 255, candidateDetails.FormattedResume); //TODO:
            _command.Varchar("@Keywords", 500, candidateDetails.Keywords);
            _command.Varchar("@Status", 3, "AVL");
            _command.UniqueIdentifier("@OriginalFileID", DBNull.Value);
            _command.UniqueIdentifier("@FormattedFileID", DBNull.Value);
            _command.Varchar("@Address1", 255, candidateDetails.Address1);
            _command.Varchar("@Address2", 255, candidateDetails.Address2);
            _command.Varchar("@City", 50, candidateDetails.City);
            _command.Int("@StateID", candidateDetails.StateID);
            _command.Varchar("@ZipCode", 20, candidateDetails.ZipCode);
            _command.Varchar("@Email", 255, candidateDetails.Email);
            _command.Varchar("@Phone1", 15, candidateDetails.Phone1);
            _command.Varchar("@Phone2", 15, candidateDetails.Phone2);
            _command.Varchar("@Phone3", 15, candidateDetails.Phone3);
            _command.SmallInt("@Phone3Ext", candidateDetails.PhoneExt.ToInt16());
            _command.VarcharD("@OriginalFileType", 10);      //TODO:
            _command.VarcharD("@OriginalContentType", 255);  //TODO:
            _command.VarcharD("@FormattedFileType", 10);     //TODO:
            _command.VarcharD("@FormattedContentType", 255); //TODO:
            _command.Varchar("@LinkedIn", 255, candidateDetails.LinkedIn);
            _command.Varchar("@Facebook", 255, candidateDetails.Facebook);
            _command.Varchar("@Twitter", 255, candidateDetails.Twitter);
            _command.Varchar("@Google", 255, candidateDetails.GooglePlus);
            _command.Bit("@Refer", candidateDetails.Refer);                                 //TODO:
            _command.Varchar("@ReferAccountMgr", 10, candidateDetails.ReferAccountManager); //TODO:
            _command.Varchar("@TaxTerm", 10, candidateDetails.TaxTerm);
            _command.Bit("@Background", candidateDetails.Background);
            _command.Varchar("@Summary", -1, candidateDetails.Summary);
            _command.Varchar("@Objective", -1, "");
            _command.Bit("@EEO", candidateDetails.Eeo);
            _command.Varchar("@EEOFile", 255, candidateDetails.EeoFile);
            _command.VarcharD("@EEOFileType", 10);     //TODO:
            _command.VarcharD("@EEOContentType", 255); //TODO:
            _command.Varchar("@RelocNotes", 200, candidateDetails.RelocationNotes);
            _command.Varchar("@SecurityClearanceNotes", 200, candidateDetails.SecurityNotes);
            _command.Varchar("@User", 10, "ADMIN");

            using SqlDataReader _reader = _command.ExecuteReader();

            _reader.Read();
            if (_reader.HasRows)
            {
                _returnCode = _reader.GetInt32(0);
            }
        }
        catch
        {
            // ignored
        }

        return _returnCode;
    }

    /// <summary>
    ///     (An Action that handles HTTP POST requests) parse resume.
    /// </summary>
    /// <remarks>
    ///     Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily, 03-01-2022.
    /// </remarks>
    [HttpPost("[action]")]
    //public void ParseResume(IList<IFormFile> chunkFile, IList<IFormFile> UploadFiles)
    public void ParseResume()
    {
        /*
         // DAXTRA
         string _name = Request.Form["filename"].ToString();
         string _filename = _hostingEnvironment.ContentRootPath + $@"Upload\{_name}";

         using MemoryStream _stream = new();
         using FileStream _fs = System.IO.File.Open(_filename, FileMode.Create, FileAccess.Write);
         try
         {
             Request.Form.Files[0].CopyTo(_fs);
             Request.Form.Files[0].CopyTo(_stream);
             _fs.Flush();
             _fs.Close();
         }
         catch
         {
             _fs.Close();
         }

         RestClient _client = new("https://cvxdemo.daxtra.com/cvx/rest/api/v1/profile/full/json")
                              {
                                  Timeout = -1
                              };
         RestRequest request = new(Method.POST)
                               {
                                   AlwaysMultipartFormData = true
                               };
         request.AddParameter("account", "TitanTech");
         request.AddParameter("file", Convert.ToBase64String(_stream.ToArray()));
         IRestResponse response = _client.Execute(request);

         using FileStream _fs1 = System.IO.File.Open(@"C:\Projects\ProfSvc_WebAPI\Upload\ParsedJSON3.txt", FileMode.Create, FileAccess.Write);
         using StreamWriter _streamWriter = new(_fs1);
         _streamWriter.WriteLine(response.Content);
         _streamWriter.Flush();
         _streamWriter.Close();
         _fs1.Close();

         return;*/

        // SOVREN
        /*string _name = Request.Form["filename"].ToString();
        string _filename = _hostingEnvironment.ContentRootPath + $@"Upload\{_name}";

        using MemoryStream _stream = new();
        using FileStream _fs = System.IO.File.Open(_filename, FileMode.Create, FileAccess.Write);
        try
        {
            Request.Form.Files[0].CopyTo(_fs);
            Request.Form.Files[0].CopyTo(_stream);
            _fs.Flush();
            _fs.Close();
        }
        catch
        {
            _fs.Close();
        }

        SovrenClient _clientSovren = new("40288999", "xKxv4d/+q7Pb15JWHSmVoWh0w49k1teWHWbsFa4i", DataCenter.US);
        Document _doc = new(_stream.ToArray(), DateTime.Today);

        ParseRequest _parseRequest = new(_doc, new());*/

        try
        {
            //Task<ParseResumeResponse> _parseResumeTask = _clientSovren.ParseResume(_parseRequest);
            //ParseResumeResponse _parseResume = _parseResumeTask.Result;
            //_parseResume.EasyAccess().SaveResumeJsonToFile(@"C:\Projects\ProfSvc_WebAPI\Upload\ParsedJSON4_Sovren.txt",true, false);
            //using FileStream _fs1 = System.IO.File.Open(@"C:\Projects\ProfSvc_WebAPI\Upload\ParsedJSON4_Sovren.txt", FileMode.Create, FileAccess.Write);
            //using StreamWriter _streamWriter = new(_fs1);
            //_streamWriter.WriteLine(_sovrenJson);
            //_streamWriter.Flush();
            //_streamWriter.Close();
            //_fs1.Close();
        }
        catch //(SovrenException)
        {
            //
        }

        //return;

        using FileStream _fs = System.IO.File.Open(@"C:\Projects\ProfSvc_WebAPI\Upload\ParsedJSON3.txt", FileMode.OpenOrCreate, FileAccess.Read);
        using StreamReader _streamReader = new(_fs);
        string _jsonFile = _streamReader.ReadToEnd();
        _streamReader.Close();
        _fs.Close();

        JObject _parsedData = JObject.Parse(_jsonFile);
        JToken _personToken = _parsedData.SelectToken("Resume.StructuredResume.PersonName");
        string _firstName, _lastName, _middleName, _emailAddress, _phoneMain, _altPhone, _address1, _city, _state, _zipCode;
        if (_personToken != null)
        {
            if (_personToken["GivenName"] != null)
            {
                _firstName = _personToken["GivenName"].ToString();
            }

            if (_personToken["MiddleName"]?[0] != null)
            {
                _middleName = _personToken["MiddleName"][0].ToString();
            }

            if (_personToken["FamilyName"] != null)
            {
                _lastName = _personToken["FamilyName"].ToString();
            }
        }

        JToken _contactToken = _parsedData.SelectToken("Resume.StructuredResume.ContactMethod");
        Regex _pattern = new("[^0-9]");

        if (_contactToken != null)
        {
            if (_contactToken["InternetEmailAddress_main"] != null)
            {
                _emailAddress = _contactToken["InternetEmailAddress_main"].ToString();
            }

            if (_contactToken["Telephone_mobile"] != null)
            {
                _phoneMain = _pattern.Replace(_contactToken["Telephone_mobile"]!.ToString(), "");
                if (_phoneMain.Length > 10)
                {
                    _phoneMain = _phoneMain[^10..];
                }
            }

            if (_contactToken["Telephone_home"] != null || _contactToken["Telephone_work"] != null || _contactToken["Telephone_alt"] != null ||
                _contactToken["Telephone_work"] != null)
            {
                _altPhone = _contactToken!["Telephone_home"] == null
                                ? _contactToken["Telephone_work"] == null
                                      ? _contactToken["Telephone_alt"] == null ? "" : _pattern.Replace(_contactToken["Telephone_alt"].ToString(), "")
                                      : _pattern.Replace(_contactToken["Telephone_work"].ToString(), "")
                                : _pattern.Replace(_contactToken["Telephone_home"].ToString(), "");
                if (_altPhone.Length > 10)
                {
                    _altPhone = _altPhone[^10..];
                }
            }

            JToken _addressToken = _contactToken["PostalAddress_main"];
            if (_addressToken != null)
            {
                if (_addressToken["AddressLine"] != null)
                {
                    _address1 = _addressToken["AddressLine"].ToString();
                }

                if (_addressToken["Municipality"] != null)
                {
                    _city = _addressToken["Municipality"].ToString();
                }

                if (_addressToken["Region"] != null)
                {
                    _state = _addressToken["Region"].ToString();
                }

                if (_addressToken["PostalCode"] != null)
                {
                    _zipCode = _addressToken["PostalCode"].ToString();
                }
            }
        }

        JToken _educationToken = _parsedData.SelectToken("Resume.StructuredResume.EducationHistory");
        DataTable _tableEducation = new();
        _tableEducation.Columns.Add("Degree", typeof(string));
        _tableEducation.Columns.Add("College", typeof(string));
        _tableEducation.Columns.Add("State", typeof(string));
        _tableEducation.Columns.Add("Country", typeof(string));
        _tableEducation.Columns.Add("Year", typeof(string));
        if (_educationToken != null)
        {
            for (int i = 0; i < _educationToken.Count(); i++)
            {
                JToken _forToken = _educationToken[i];
                DataRow _dr = _tableEducation.NewRow();
                if (_forToken == null)
                {
                    continue;
                }

                if (_forToken["Degree"]?["DegreeName"] != null)
                {
                    _dr["Degree"] = _forToken["Degree"]["DegreeName"].ToString();
                }

                if (_forToken["SchoolName"] != null)
                {
                    _dr["College"] = _forToken["SchoolName"].ToString();
                }

                if (_forToken["LocationSummary"]?["Region"] != null)
                {
                    _dr["State"] = _forToken["LocationSummary"]["Region"].ToString();
                }

                if (_forToken["LocationSummary"]?["CountryCode"] != null)
                {
                    _dr["Country"] = _forToken["LocationSummary"]["CountryCode"].ToString();
                }

                if (_forToken["Degree"]?["DegreeDate"] != null)
                {
                    _dr["Year"] = _forToken["Degree"]["DegreeDate"].ToString();
                }

                _tableEducation.Rows.Add(_dr);
            }
        }

        JToken _employmentToken = _parsedData.SelectToken("Resume.StructuredResume.EmploymentHistory");
        DataTable _tableEmployer = new();
        _tableEmployer.Columns.Add("Employer", typeof(string));
        _tableEmployer.Columns.Add("Start", typeof(string));
        _tableEmployer.Columns.Add("End", typeof(string));
        _tableEmployer.Columns.Add("Location", typeof(string));
        _tableEmployer.Columns.Add("Title", typeof(string));
        _tableEmployer.Columns.Add("Description", typeof(string));
        if (_employmentToken != null)
        {
            for (int i = 0; i < _employmentToken.Count(); i++)
            {
                JToken _forToken = _employmentToken[i];
                DataRow _dr = _tableEmployer.NewRow();
                if (_forToken == null)
                {
                    continue;
                }

                if (_forToken["OrgName"] != null)
                {
                    _dr["Employer"] = _forToken["OrgName"].ToString();
                }

                if (_forToken["StartDate"] != null)
                {
                    _dr["Start"] = _forToken["StartDate"].ToString();
                }

                if (_forToken["EndDate"] != null)
                {
                    _dr["End"] = _forToken["EndDate"].ToString();
                }

                if (_forToken["LocationSummary"] != null)
                {
                    string _location = "";
                    if (_forToken["LocationSummary"]["Municipality"] != null)
                    {
                        _location += ", " + _forToken["LocationSummary"]["Municipality"];
                    }

                    if (_forToken["LocationSummary"]["Region"] != null)
                    {
                        _location += ", " + _forToken["LocationSummary"]["Region"];
                    }

                    if (_forToken["LocationSummary"]["CountryCode"] != null)
                    {
                        _location += ", " + _forToken["LocationSummary"]["CountryCode"];
                    }

                    if (_location != "")
                    {
                        _location = _location[2..];
                    }

                    _dr["Location"] = _location;
                }

                if (_forToken["Title"]?[0] != null)
                {
                    _dr["Title"] = _forToken["Title"][0].ToString();
                }

                if (_forToken["Description"] != null)
                {
                    _dr["Description"] = _forToken["Description"].ToString();
                }

                _tableEmployer.Rows.Add(_dr);
            }
        }

        JToken _skillsToken = _parsedData.SelectToken("Resume.StructuredResume.Competency");
        DataTable _tableSkills = new();
        _tableSkills.Columns.Add("Skill", typeof(string));
        _tableSkills.Columns.Add("LastUsed", typeof(int));
        _tableSkills.Columns.Add("Month", typeof(int));
        if (_skillsToken != null)
        {
            for (int i = 0; i < _skillsToken.Count(); i++)
            {
                JToken _forToken = _skillsToken[i];
                DataRow _dr = _tableSkills.NewRow();
                if (_forToken != null)
                {
                    if (_forToken["skillName"] != null)
                    {
                        _dr["Skill"] = _forToken["skillName"].ToString();
                    }

                    if (_forToken["lastUsed"] != null)
                    {
                        _dr["LastUsed"] = _forToken["lastUsed"].ToInt32();
                    }

                    if (_forToken["skillUsed"]?["type"] != null && _forToken["skillUsed"]["value"] != null)
                    {
                        _dr["LastUsed"] = _forToken["skillUsed"]["type"].ToString() != "Months" ? _forToken["skillUsed"]["value"].ToInt32() * 12
                                              : _forToken["skillUsed"]["value"].ToInt32();
                    }
                }

                _tableSkills.Rows.Add(_dr);
            }
        }
    }

    /// <summary>
    ///     (An Action that handles HTTP POST requests) cancel parse resume.
    /// </summary>
    /// <remarks>
    ///     Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily, 03-01-2022.
    /// </remarks>
    /// <param name="uploadFiles">
    ///     .
    /// </param>
    [HttpPost("[action]")]
    public void CancelParseResume(IList<IFormFile> uploadFiles)
    {
        try
        {
            string filename = _hostingEnvironment.ContentRootPath + $@"\{uploadFiles[0].FileName}";
            if (System.IO.File.Exists(filename))
            {
                System.IO.File.Delete(filename);
            }
        }
        catch (Exception e)
        {
            Response.Clear();
            Response.StatusCode = 200;
            Response.HttpContext.Features.Get<IHttpResponseFeature>()!.ReasonPhrase = "File removed successfully";
            Response.HttpContext.Features.Get<IHttpResponseFeature>()!.ReasonPhrase = e.Message;
        }
    }

    [HttpPost("SaveMPC")]
    public ActionResult<Dictionary<string, object>> SaveMPC(CandidateRatingMPC mpc, [FromQuery] string user)
    {
        string _mpcNotes = "";
        List<CandidateMPC> _mpc = new();
        if (mpc != null)
        {
            using SqlConnection _con = new(_config.GetConnectionString("DBConnect"));
            _con.Open();
            try
            {
                using SqlCommand _command = new("ChangeMPC", _con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                _command.Int("@CandidateId", mpc.ID);
                _command.Bit("@MPC", mpc.MPC);
                _command.Varchar("@Notes", -1, mpc.MPCComments);
                _command.Varchar("@From", 10, "JOLLY");
                _mpcNotes = _command.ExecuteScalar().ToString();
            }
            catch
            {
                //
            }
            _con.Close();

            string[] _mpcArray = _mpcNotes.Split('?');
            _mpc.AddRange(_mpcArray
                         .Select(str => new
                         {
                             _str = str,
                             _innerArray = str.Split('^')
                         })
                         .Where(t => t._innerArray.Length == 4)
                         .Select(t => new CandidateMPC(t._innerArray[0].ToDateTime(), t._innerArray[1], t._innerArray[2].ToBoolean(), t._innerArray[3])));

            _mpc = _mpc.OrderByDescending(x => x.Date).ToList();
            bool _mpcFirst = false;
            string _mpcComments = "";

            if (!_mpcNotes.NullOrWhiteSpace())
            {
                CandidateMPC _mpcFirstCandidate = _mpc.FirstOrDefault();
                if (_mpcFirstCandidate != null)
                {
                    _mpcFirst = _mpcFirstCandidate.MPC;
                    _mpcComments = _mpcFirstCandidate.Comments;
                }
            }
            mpc.MPC = _mpcFirst;
            mpc.MPCComments = _mpcComments;
        }

        return new Dictionary<string, object>
                    {
                        {
                            "MPCList", _mpc
                        },
                        {
                            "FirstMPC", mpc
                        }
                    };
    }

    [HttpPost("[action]")]
    public ActionResult<Dictionary<string, object>> SaveRating(CandidateRatingMPC rating, [FromQuery] string user)
    {
        string _ratingNotes = "";
        List<CandidateRating> _rating = new();
        if (rating != null)
        {
            using SqlConnection _con = new(_config.GetConnectionString("DBConnect"));
            _con.Open();
            try
            {
                using SqlCommand _command = new("ChangeRating", _con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                _command.Int("@CandidateId", rating.ID);
                _command.TinyInt("@Rating", rating.Rating);
                _command.Varchar("@Notes", -1, rating.RatingComments);
                _command.Varchar("@From", 10, "JOLLY");
                _ratingNotes = _command.ExecuteScalar().ToString();
            }
            catch
            {
                //
            }
            _con.Close();

            string[] _ratingArray = _ratingNotes.Split('?');
            _rating.AddRange(_ratingArray
                         .Select(str => new
                         {
                             _str = str,
                             _innerArray = str.Split('^')
                         })
                         .Where(t => t._innerArray.Length == 4)
                         .Select(t => new CandidateRating(t._innerArray[0].ToDateTime(), t._innerArray[1], t._innerArray[2].ToByte(), t._innerArray[3])));

            _rating = _rating.OrderByDescending(x => x.Date).ToList();
            int _ratingFirst = 0;
            string _ratingComments = "";

            if (!_ratingNotes.NullOrWhiteSpace())
            {
                CandidateRating _ratingFirstCandidate = _rating.FirstOrDefault();
                if (_ratingFirstCandidate != null)
                {
                    _ratingFirst = _ratingFirstCandidate.Rating;
                    _ratingComments = _ratingFirstCandidate.Comments;
                }
            }
            rating.Rating = _ratingFirst;
            rating.RatingComments = _ratingComments;
        }

        return new Dictionary<string, object>
                    {
                        {
                            "RatingList", _rating
                        },
                        {
                            "FirstRating", rating
                        }
                    };
    }

    #endregion
}