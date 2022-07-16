#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           CandidatesController.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-26-2022 19:30
// Last Updated On:     03-30-2022 15:45
// *****************************************/

#endregion

#region Using

using Sovren;
using Sovren.Models;
using Sovren.Models.API.Parsing;
using Sovren.Models.Resume.ContactInfo;
using Sovren.Models.Resume.Education;
using Sovren.Models.Resume.Skills;

#endregion

namespace ProfSvc_WebAPI.Controllers;

[ApiController, Route("api/[controller]/[action]")]
public class CandidatesController : ControllerBase
{
    /// <summary>
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="env"></param>
    public CandidatesController(IConfiguration configuration, IWebHostEnvironment env)
    {
        _configuration = configuration;
        _hostingEnvironment = env;
    }

    private readonly IConfiguration _configuration;

    private readonly IWebHostEnvironment _hostingEnvironment;

    /// <summary>
    /// </summary>
    /// <param name="uploadFiles"></param>
    [HttpPost]
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

    /// <summary>
    /// </summary>
    /// <param name="documentID"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<Dictionary<string, object>> DeleteCandidateDocument([FromQuery] int documentID, [FromQuery] string user)
    {
        await Task.Delay(1);
        await using SqlConnection _connection = new(_configuration.GetConnectionString("DBConnect"));
        await _connection.OpenAsync();
        List<CandidateDocument> _documents = new();
        try
        {
            await using SqlCommand _command = new("DeleteCandidateDocument", _connection)
                                              {
                                                  CommandType = CommandType.StoredProcedure
                                              };
            _command.Int("CandidateDocumentId", documentID);
            _command.Varchar("User", 10, user);
            await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
            if (_reader.HasRows)
            {
                while (_reader.Read())
                {
                    _documents.Add(new(_reader.GetInt32(0), _reader.GetString(1), _reader.GetString(2), _reader.GetString(3), $"{_reader.NDateTime(4)} [{_reader.NString(5)}]",
                                       _reader.GetString(6), _reader.GetString(7), _reader.GetInt32(8)));
                }
            }

            await _reader.CloseAsync();
        }
        catch
        {
            //
        }

        return new()
               {
                   {
                       "Document", _documents
                   }
               };
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="candidateID"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<Dictionary<string, object>> DeleteEducation([FromQuery] int id, [FromQuery] int candidateID, [FromQuery] string user)
    {
        await Task.Delay(1);
        List<CandidateEducation> _education = new();
        if (id == 0)
        {
            return new()
                   {
                       {
                           "Education", _education
                       }
                   };
        }

        await using SqlConnection _connection = new(_configuration.GetConnectionString("DBConnect"));
        await _connection.OpenAsync();
        try
        {
            await using SqlCommand _command = new("DeleteCandidateEducation", _connection)
                                              {
                                                  CommandType = CommandType.StoredProcedure
                                              };
            _command.Int("Id", id);
            _command.Int("candidateId", candidateID);
            _command.Varchar("User", 10, user);
            await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
            if (_reader.HasRows)
            {
                while (_reader.Read())
                {
                    _education.Add(new(_reader.GetInt32(0), _reader.GetString(1), _reader.GetString(2), _reader.GetString(3), _reader.GetString(4),
                                       _reader.GetString(5), _reader.GetString(6)));
                }
            }

            await _reader.CloseAsync();
        }
        catch
        {
            //
        }

        await _connection.CloseAsync();

        return new()
               {
                   {
                       "Education", _education
                   }
               };
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="candidateID"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<Dictionary<string, object>> DeleteExperience([FromQuery] int id, [FromQuery] int candidateID, [FromQuery] string user)
    {
        await Task.Delay(1);
        List<CandidateExperience> _experiences = new();
        if (id == 0)
        {
            return new()
                   {
                       {
                           "Experience", _experiences
                       }
                   };
        }

        await using SqlConnection _connection = new(_configuration.GetConnectionString("DBConnect"));
        await _connection.OpenAsync();
        try
        {
            await using SqlCommand _command = new("DeleteCandidateExperience", _connection)
                                              {
                                                  CommandType = CommandType.StoredProcedure
                                              };
            _command.Int("Id", id);
            _command.Int("candidateId", candidateID);
            _command.Varchar("User", 10, user);
            await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
            if (_reader.HasRows)
            {
                while (_reader.Read())
                {
                    _experiences.Add(new(_reader.GetInt32(0), _reader.GetString(1), _reader.GetString(2), _reader.GetString(3), _reader.GetString(4),
                                         _reader.GetString(5), _reader.GetString(6), _reader.GetString(7)));
                }
            }

            await _reader.CloseAsync();
        }
        catch
        {
            //
        }

        await _connection.CloseAsync();

        return new()
               {
                   {
                       "Experience", _experiences
                   }
               };
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="candidateID"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<Dictionary<string, object>> DeleteNotes([FromQuery] int id, [FromQuery] int candidateID, [FromQuery] string user)
    {
        await Task.Delay(1);
        List<CandidateNotes> _notes = new();
        if (id == 0)
        {
            return new()
                   {
                       {
                           "Notes", _notes
                       }
                   };
        }

        await using SqlConnection _connection = new(_configuration.GetConnectionString("DBConnect"));
        await _connection.OpenAsync();
        try
        {
            await using SqlCommand _command = new("DeleteCandidateNotes", _connection)
                                              {
                                                  CommandType = CommandType.StoredProcedure
                                              };
            _command.Int("Id", id);
            _command.Int("candidateId", candidateID);
            _command.Varchar("User", 10, user);
            await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
            if (_reader.HasRows)
            {
                while (_reader.Read())
                {
                    _notes.Add(new(_reader.GetInt32(0), _reader.GetDateTime(1), _reader.GetString(2), _reader.GetString(3)));
                }
            }

            await _reader.CloseAsync();
        }
        catch
        {
            //
        }

        await _connection.CloseAsync();

        return new()
               {
                   {
                       "Notes", _notes
                   }
               };
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="candidateID"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<Dictionary<string, object>> DeleteSkill([FromQuery] int id, [FromQuery] int candidateID, [FromQuery] string user)
    {
        await Task.Delay(1);
        List<CandidateSkills> _skills = new();
        if (id == 0)
        {
            return new()
                   {
                       {
                           "Skills", _skills
                       }
                   };
        }

        await using SqlConnection _connection = new(_configuration.GetConnectionString("DBConnect"));
        await _connection.OpenAsync();
        try
        {
            await using SqlCommand _command = new("DeleteCandidateSkill", _connection)
                                              {
                                                  CommandType = CommandType.StoredProcedure
                                              };
            _command.Int("Id", id);
            _command.Int("candidateId", candidateID);
            _command.Varchar("User", 10, user);
            await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
            if (_reader.HasRows)
            {
                while (_reader.Read())
                {
                    _skills.Add(new(_reader.GetInt32(0), _reader.GetString(1), _reader.GetInt16(2), _reader.GetInt16(3), _reader.GetString(4)));
                }
            }

            await _reader.CloseAsync();
        }
        catch
        {
            //
        }

        await _connection.CloseAsync();

        return new()
               {
                   {
                       "Skills", _skills
                   }
               };
    }

    /// <summary>
    /// </summary>
    /// <param name="candidateID"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<Dictionary<string, object>>> GetCandidateDetails([FromQuery] int candidateID)
    {
        await using SqlConnection _connection = new(_configuration.GetConnectionString("DBConnect"));
        CandidateDetails _candidate = null;
        string _candRating = "", _candMPC = "";

        await using SqlCommand _command = new("GetDetailCandidate", _connection)
                                          {
                                              CommandType = CommandType.StoredProcedure
                                          };
        _command.Int("@CandidateID", candidateID);

        await _connection.OpenAsync();
        await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
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
                              _reader.GetString(15), _reader.GetInt32(16), _reader.GetString(17), _reader.GetBoolean(18),
                              _reader.NDateTime(19), _reader.GetString(20), _reader.NString(21), _reader.NString(22),
                              _reader.GetBoolean(23)));
        }

        _reader.NextResult(); //Managers

        _reader.NextResult(); //Documents
        List<CandidateDocument> _documents = new();
        while (_reader.Read())
        {
            _documents.Add(new(_reader.GetInt32(0), _reader.GetString(1), _reader.GetString(2), _reader.GetString(3), $"{_reader.NDateTime(4)} [{_reader.NString(5)}]",
                               _reader.GetString(6), _reader.GetString(7), _reader.GetInt32(8)));
        }

        await _reader.CloseAsync();

        await _connection.CloseAsync();

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
                       },
                       {
                           "Document", _documents
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
                   },
                   {
                       "Document", _documents
                   }
               };
    }

    /// <summary>
    /// </summary>
    /// <param name="searchModel"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<Dictionary<string, object>> GetGridCandidates([FromBody] CandidateSearch searchModel)
    {
        await using SqlConnection _connection = new(_configuration.GetConnectionString("DBConnect"));
        List<Candidates> _candidates = new();
        await using SqlCommand _command = new("GetGridCandidates", _connection)
                                          {
                                              CommandType = CommandType.StoredProcedure
                                          };
        _command.Int("Count", searchModel.ItemCount);
        _command.Int("Page", searchModel.Page);
        _command.Int("SortRow", searchModel.SortField);
        _command.TinyInt("SortOrder", searchModel.SortDirection);
        _command.Varchar("Name", 255, searchModel.Name);
        _command.Bit("MyCandidates", searchModel.MyCandidates);
        _command.Bit("IncludeAdmin", searchModel.IncludeAdmin);
        _command.Varchar("Keywords", 2000, searchModel.Keywords);
        _command.Varchar("Skill", 2000, searchModel.Skills);
        _command.Bit("SearchState", !searchModel.CityZip);
        _command.Varchar("City", 30, searchModel.CityName);
        _command.Varchar("State", 1000, searchModel.StateID);
        _command.Int("Proximity", searchModel.Proximity);
        _command.TinyInt("ProximityUnit", searchModel.ProximityUnit);
        _command.Varchar("Eligibility", 10, searchModel.Eligibility);
        _command.Varchar("Reloc", 10, searchModel.Relocate);
        _command.Varchar("JobOptions", 10, searchModel.JobOptions);
        //_command.Varchar("Communications",10, searchModel.Communication);
        _command.Varchar("Security", 10, searchModel.SecurityClearance);
        _command.Varchar("User", 10, searchModel.User);

        await _connection.OpenAsync();
        await using SqlDataReader _reader = await _command.ExecuteReaderAsync();

        _reader.Read();
        int _count = _reader.GetInt32(0);

        _reader.NextResult();

        while (_reader.Read())
        {
            _candidates.Add(new(_reader.GetInt32(0), _reader.GetString(1), _reader.GetString(2), _reader.GetString(3), _reader.GetString(4),
                                _reader.GetString(5), _reader.GetString(6), _reader.GetBoolean(7), _reader.GetByte(8), _reader.GetBoolean(9),
                                _reader.GetBoolean(10)));
        }

        await _reader.CloseAsync();

        await _connection.CloseAsync();

        return new()
               {
                   {
                       "Candidates", _candidates
                   },
                   {
                       "Count", _count
                   }
               };
    }

    /// <summary>
    /// </summary>
    [HttpPost]
    public async Task ParseResume()
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
        string _fileData = Request.Form["fileData"].ToString();
        string _name = _fileData.Split('^')[0];
        string _filename = _hostingEnvironment.ContentRootPath + $@"Upload\{_name}";

        await using MemoryStream _stream = new();
        await using FileStream _fs = System.IO.File.Open(_filename, FileMode.Create, FileAccess.Write);
        try
        {
            await Request.Form.Files[0].CopyToAsync(_fs);
            await Request.Form.Files[0].CopyToAsync(_stream);
            _fs.Flush();
            _fs.Close();
        }
        catch
        {
            _fs.Close();
        }

        SovrenClient _clientSovren = new("40423445", "L5VLzl7TeAukjSXJLIGNRVA1f8bVFYFbr5GWlUip", DataCenter.US);
        Document _doc = new(_stream.ToArray(), DateTime.Today);

        ParseRequest _parseRequest = new(_doc, new());

        ParseResumeResponse _parseResume = null;
        try
        {
            Task<ParseResumeResponse> _parseResumeTask = _clientSovren.ParseResume(_parseRequest);
            _parseResume = _parseResumeTask.Result;
            _parseResume.EasyAccess().SaveResumeJsonToFile(_filename + ".json", true, false);
            /*using FileStream _fs1 = System.IO.File.Open(_filename + ".json", FileMode.Create, FileAccess.Write);
            using StreamWriter _streamWriter = new(_fs1);
            _streamWriter.WriteLine(_sovrenJson);
            _streamWriter.Flush();
            _streamWriter.Close();
            _fs1.Close();*/
        }
        catch //(SovrenException)
        {
            //
        }

        //return;

        if (_parseResume != null)
        {
            /*using FileStream _fs1 = System.IO.File.Open(_filename + ".json", FileMode.OpenOrCreate, FileAccess.Read);
            using StreamReader _streamReader = new(_fs1);
            string _jsonFile = _streamReader.ReadToEnd();
            _streamReader.Close();
            _fs.Close();
    
            JObject _parsedData = JObject.Parse(_jsonFile);
            JToken _personToken = _parsedData.SelectToken("Resume.StructuredResume.PersonName");*/
            ParseResumeResponseExtensions _parseData = _parseResume.EasyAccess();
            string _firstName = "", _lastName = "", _middleName = "", _emailAddress = "", _phoneMain = "", _altPhone = "", _address1 = "", _address2 = "", _city = "", _state = "", _zipCode = "";
            string _keywords = "", _experienceSummary = "", _backgroundNotes = "", _textResume = "", _objective = "", _user = _fileData.Split('^')[3];
            if (_parseData.GetCandidateName() != null)
            {
                PersonName _candidateName = _parseData.GetCandidateName();
                _firstName = _candidateName.GivenName ?? "";
                _middleName = _candidateName.MiddleName ?? "";
                _lastName = _candidateName.FamilyName ?? "";
            }

            bool _background = _parseData.HasSecurityClearance();

            //_parseResume
            /*JToken _contactToken = _parsedData.SelectToken("Resume.StructuredResume.ContactMethod");
            Regex _pattern = new("[^0-9]");*/

            if (_parseData.GetContactInfo() != null)
            {
                ContactInformation _contactInfo = _parseData.GetContactInfo();
                if (_contactInfo.EmailAddresses?.Count > 0)
                {
                    _emailAddress = _contactInfo.EmailAddresses[0];
                }

                if (_contactInfo.Telephones?.Count > 0)
                {
                    _phoneMain = $"{_contactInfo.Telephones[0].AreaCityCode}{_contactInfo.Telephones[0].SubscriberNumber}".Replace("-", "").Replace(" ", "");
                }

                if (_contactInfo.Telephones?.Count > 1)
                {
                    _altPhone = $"{_contactInfo.Telephones[1].AreaCityCode}{_contactInfo.Telephones[1].SubscriberNumber}".Replace("-", "").Replace(" ", "");
                }

                if (_contactInfo.Location != null)
                {
                    Location _location = _contactInfo.Location;
                    if (_location.StreetAddressLines?.Count > 0)
                    {
                        _address1 = _location.StreetAddressLines[0];
                    }

                    if (_location.StreetAddressLines?.Count > 1)
                    {
                        _address2 = _location.StreetAddressLines[0];
                    }

                    _city = _location.Municipality ?? "";
                    if (_location.Regions.Count > 0)
                    {
                        _state = _location.Regions[0];
                    }

                    _zipCode = _location.PostalCode ?? "";
                }
            }

            _experienceSummary = _parseResume.Value.ResumeData.ProfessionalSummary;

            DataTable _tableEducation = new();
            _tableEducation.Columns.Add("Degree", typeof(string));
            _tableEducation.Columns.Add("College", typeof(string));
            _tableEducation.Columns.Add("State", typeof(string));
            _tableEducation.Columns.Add("Country", typeof(string));
            _tableEducation.Columns.Add("Year", typeof(string));

            //_parseResume.
            List<EducationDetails> _educationDetails = _parseResume.Value.ResumeData?.Education?.EducationDetails;
            if (_educationDetails != null)
            {
                foreach (EducationDetails _education in _educationDetails)
                {
                    DataRow _dr = _tableEducation.NewRow();
                    if (_education == null)
                    {
                        return;
                    }

                    _dr["Degree"] = _education.Degree?.Name?.Normalized ?? string.Empty;
                    _dr["College"] = _education.SchoolName?.Normalized ?? string.Empty;
                    if (_education.Location?.Regions?.Count > 0)
                    {
                        _dr["State"] = _education.Location.Regions[0];
                    }

                    _dr["Country"] = _education.Location?.CountryCode ?? string.Empty;
                    _dr["Year"] = _education.LastEducationDate?.Date.Year.ToString() ?? string.Empty;
                    _tableEducation.Rows.Add(_dr);
                }
            }

            DataTable _tableEmployer = new();
            _tableEmployer.Columns.Add("Employer", typeof(string));
            _tableEmployer.Columns.Add("Start", typeof(string));
            _tableEmployer.Columns.Add("End", typeof(string));
            _tableEmployer.Columns.Add("Location", typeof(string));
            _tableEmployer.Columns.Add("Title", typeof(string));
            _tableEmployer.Columns.Add("Description", typeof(string));

            _parseResume.Value.ResumeData?.EmploymentHistory?.Positions?.ForEach(position =>
                                                                                 {
                                                                                     DataRow _dr = _tableEmployer.NewRow();
                                                                                     if (position != null)
                                                                                     {
                                                                                         _dr["Employer"] = position.Employer?.Name?.Normalized ?? string.Empty;

                                                                                         _dr["Start"] = position.StartDate?.Date.ToString("d") ?? string.Empty;
                                                                                         _dr["End"] = position.EndDate?.Date.ToString("d") ?? string.Empty;

                                                                                         string _location = "";
                                                                                         if (position.Employer?.Location != null)
                                                                                         {
                                                                                             Location _positionLocation = position.Employer?.Location;
                                                                                             if (_positionLocation != null)
                                                                                             {
                                                                                                 _location += ", " + _positionLocation.Municipality;
                                                                                                 if (_positionLocation.Regions.Any())
                                                                                                 {
                                                                                                     _location += ", " + _positionLocation.Regions.FirstOrDefault();
                                                                                                 }

                                                                                                 _location += ", " + _positionLocation.CountryCode;
                                                                                             }

                                                                                             if (_location != "")
                                                                                             {
                                                                                                 _location = _location[2..];
                                                                                             }
                                                                                         }

                                                                                         _dr["Location"] = _location;
                                                                                         _dr["Title"] = position.JobTitle?.Normalized ?? string.Empty;
                                                                                         _dr["Description"] = position.Description;
                                                                                     }

                                                                                     _tableEmployer.Rows.Add(_dr);
                                                                                 });

            DataTable _tableSkills = new();
            _tableSkills.Columns.Add("Skill", typeof(string));
            _tableSkills.Columns.Add("LastUsed", typeof(int));
            _tableSkills.Columns.Add("Month", typeof(int));

            List<ResumeTaxonomyRoot> _skillsData = _parseResume.Value.ResumeData?.SkillsData;

            if (_skillsData is {Count: > 0})
            {
                try
                {
                    foreach (ResumeTaxonomyRoot _skillResume in _skillsData)
                    {
                        List<ResumeTaxonomy> _taxonomies = _skillResume?.Taxonomies;
                        if (_taxonomies is not {Count: > 0})
                        {
                            return;
                        }

                        foreach (ResumeTaxonomy _taxonomy in _taxonomies)
                        {
                            if (_taxonomy == null)
                            {
                                continue;
                            }

                            _keywords += ", " + _taxonomy.Name;
                            List<ResumeSubTaxonomy> _subTaxonomies = _taxonomy.SubTaxonomies;
                            if (_subTaxonomies is not {Count: > 0})
                            {
                                return;
                            }

                            foreach (ResumeSubTaxonomy _subTaxonomy in _subTaxonomies)
                            {
                                List<ResumeSkill> _skills = _subTaxonomy?.Skills;
                                if (_skills is not {Count: > 0})
                                {
                                    return;
                                }

                                foreach (ResumeSkill _skillDetails in _skills)
                                {
                                    DataRow _dr = _tableSkills.NewRow();
                                    _dr["Skill"] = _skillDetails.Name ?? "";
                                    _dr["LastUsed"] = _skillDetails.LastUsed?.Value.Year.ToString() ?? "";
                                    _dr["Month"] = _skillDetails.MonthsExperience?.Value.ToString() ?? "0";
                                    _tableSkills.Rows.Add(_dr);
                                }
                            }
                        }
                    }
                }
                catch
                {
                    await Task.Delay(1);
                }
            }

            if (!_keywords.NullOrWhiteSpace())
            {
                _keywords = _keywords[2..(_keywords.Length > 502 ? 502 : _keywords.Length)].Trim();
            }

            if (_parseResume.Value.ResumeData?.SecurityCredentials?.Any() ?? true)
            {
                _parseResume.Value.ResumeData.SecurityCredentials?.ForEach(security => { _backgroundNotes += ", " + security.Name; });
            }

            if (!_backgroundNotes.NullOrWhiteSpace())
            {
                _backgroundNotes = _backgroundNotes[2..].Trim();
            }

            _objective = _parseResume.Value.ResumeData?.Objective ?? "";
            _textResume = _parseResume.Value.ResumeData?.ResumeMetadata.PlainText ?? "";

            await using SqlConnection _connection = new(_configuration.GetConnectionString("DBConnect"));
            await _connection.OpenAsync();
            //int _returnCode = 0;
            try
            {
                await using SqlCommand _command = new("SaveParsedCandidate", _connection)
                                                  {
                                                      CommandType = CommandType.StoredProcedure
                                                  };
                _command.Int("@ID", DBNull.Value, true);
                _command.Varchar("@FirstName", 50, _firstName);
                _command.Varchar("@MiddleName", 50, _middleName);
                _command.Varchar("@LastName", 50, _lastName);
                _command.Varchar("@Address", 255, _address1);
                _command.Varchar("@Address2", 255, _address2);
                _command.Varchar("@City", 50, _city);
                _command.Varchar("@State", 50, _state);
                _command.Varchar("@Zip", 20, _zipCode);
                _command.Varchar("@Email", 255, _emailAddress);
                _command.Bit("@Background", _background);
                _command.Varchar("@SecurityNotes", -1, _backgroundNotes);
                _command.Varchar("@Phone1", 15, _phoneMain);
                _command.Varchar("@Phone2", 15, _altPhone);
                _command.Varchar("@ExperienceSummary", -1, _experienceSummary);
                _command.Int("@Experience", _parseResume.Value.ResumeData.EmploymentHistory?.ExperienceSummary?.MonthsOfWorkExperience ?? 0);
                _command.Varchar("@Summary", -1, _parseResume.Value.ResumeData.EmploymentHistory?.ExperienceSummary?.Description ?? "");
                _command.Varchar("@Objective", -1, _objective);
                _command.Varchar("@Keywords", 500, _keywords);
                _command.Varchar("@JobOptions", 5, "F");
                _command.Varchar("@TaxTerm", 10, "E");
                _command.Varchar("@TextResume", -1, _textResume);
                _command.Varchar("@OriginalResume", 255, _name);
                _command.Varchar("@User", 10, _user);
                _command.Parameters.AddWithValue("@Education", _tableEducation);
                _command.Parameters.AddWithValue("@Employer", _tableEmployer);
                _command.Parameters.AddWithValue("@Skills", _tableSkills);

                await _command.ExecuteNonQueryAsync();

                /*_command.Varchar("@Title", 50, candidateDetails.Title);
                _command.Int("@Eligibility", candidateDetails.EligibilityID);
                _command.Decimal("@HourlyRate", 6, 2, candidateDetails.HourlyRate);
                _command.Decimal("@HourlyRateHigh", 6, 2, candidateDetails.HourlyRateHigh);
                _command.Decimal("@SalaryLow", 9, 2, candidateDetails.SalaryLow);
                _command.Decimal("@SalaryHigh", 9, 2, candidateDetails.SalaryHigh);
                _command.Varchar("@JobOptions", 50, candidateDetails.JobOptions);
                _command.Char("@Communication", 1, candidateDetails.Communication);
                _command.Varchar("@TextResume", -1, candidateDetails.TextResume);
                _command.Varchar("@OriginalResume", 255, candidateDetails.OriginalResume);   //TODO:
                _command.Varchar("@FormattedResume", 255, candidateDetails.FormattedResume); //TODO:
                _command.Varchar("@Keywords", 500, candidateDetails.Keywords);
                _command.Varchar("@Status", 3, "AVL");
                _command.UniqueIdentifier("@OriginalFileID", DBNull.Value);
                _command.UniqueIdentifier("@FormattedFileID", DBNull.Value);
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
                _command.Varchar("@SecurityClearanceNotes", 200, candidateDetails.SecurityNotes);*/
                /*_command.Varchar("@User", 10, "ADMIN");

                await using SqlDataReader _reader = await _command.ExecuteReaderAsync();

                _reader.Read();
                if (_reader.HasRows)
                {
                    _returnCode = _reader.GetInt32(0);
                }

                await _reader.CloseAsync();*/
            }
            catch //(Exception exception)
            {
                await Task.Delay(1);
                // ignored
            }

            await _connection.CloseAsync();
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="candidateDetails"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> SaveCandidate(CandidateDetails candidateDetails)
    {
        if (candidateDetails == null)
        {
            return -1;
        }

        await using SqlConnection _connection = new(_configuration.GetConnectionString("DBConnect"));
        await _connection.OpenAsync();
        int _returnCode = 0;
        try
        {
            await using SqlCommand _command = new("SaveCandidate", _connection)
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

            await using SqlDataReader _reader = await _command.ExecuteReaderAsync();

            _reader.Read();
            if (_reader.HasRows)
            {
                _returnCode = _reader.GetInt32(0);
            }

            await _reader.CloseAsync();
        }
        catch
        {
            // ignored
        }

        await _connection.CloseAsync();

        return _returnCode;
    }

    /// <summary>
    /// </summary>
    /// <param name="activity"></param>
    /// <param name="candidateID"></param>
    /// <param name="user"></param>
    /// <param name="roleID"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<Dictionary<string, object>> SaveCandidateActivity(CandidateActivity activity, [FromQuery] int candidateID, [FromQuery] string user, [FromQuery] string roleID = "RS")
    {
        await Task.Delay(1);
        List<CandidateActivity> _activities = new();
        if (activity == null)
        {
            return new()
                   {
                       {
                           "Activity", _activities
                       }
                   };
        }

        await using SqlConnection _connection = new(_configuration.GetConnectionString("DBConnect"));
        await _connection.OpenAsync();
        try
        {
            await using SqlCommand _command = new("SaveCandidateActivity", _connection)
                                              {
                                                  CommandType = CommandType.StoredProcedure
                                              };
            _command.Int("SubmissionId", activity.ID);
            _command.Int("CandidateID", candidateID);
            _command.Int("RequisitionID", activity.RequisitionID);
            _command.Varchar("Notes", 1000, activity.Notes);
            _command.Char("Status", 3, activity.NewStatusCode);
            _command.Varchar("User", 10, user);
            _command.Bit("ShowCalendar", activity.ShowCalendar);
            _command.Date("DateTime", activity.DateTimeInterview == DateTime.MinValue ? DBNull.Value : activity.DateTimeInterview);
            _command.Char("Type", 1, activity.TypeOfInterview);
            _command.Varchar("PhoneNumber", 20, activity.PhoneNumber);
            _command.Varchar("InterviewDetails", 2000, activity.InterviewDetails);
            _command.Bit("UpdateSchedule", false);
            _command.Bit("CandScreen", true);
            _command.Char("RoleID", 2, roleID);
            await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
            if (_reader.HasRows)
            {
                while (_reader.Read())
                {
                    _activities.Add(new(_reader.GetString(0), _reader.GetDateTime(1), _reader.GetString(2), _reader.GetInt32(3), _reader.GetInt32(4),
                                        _reader.GetString(5), _reader.GetString(6), _reader.GetInt32(7), _reader.GetBoolean(8), _reader.GetString(9),
                                        _reader.GetString(10), _reader.GetString(11), _reader.GetBoolean(12), _reader.GetString(13), _reader.GetInt32(14),
                                        _reader.GetString(15), _reader.GetInt32(16), _reader.GetString(17), _reader.GetBoolean(18),
                                        _reader.NDateTime(19), _reader.GetString(20), _reader.NString(21), _reader.NString(22),
                                        _reader.GetBoolean(23)));
                }
            }

            await _reader.CloseAsync();
        }
        catch
        {
            //
        }

        await _connection.CloseAsync();

        return new()
               {
                   {
                       "Activity", _activities
                   }
               };
    }

    /// <summary>
    /// </summary>
    /// <param name="education"></param>
    /// <param name="candidateID"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<Dictionary<string, object>> SaveEducation(CandidateEducation education, [FromQuery] int candidateID, [FromQuery] string user)
    {
        await Task.Delay(1);
        List<CandidateEducation> _education = new();
        if (education == null)
        {
            return new()
                   {
                       {
                           "Education", _education
                       }
                   };
        }

        await using SqlConnection _connection = new(_configuration.GetConnectionString("DBConnect"));
        await _connection.OpenAsync();
        try
        {
            await using SqlCommand _command = new("SaveEducation", _connection)
                                              {
                                                  CommandType = CommandType.StoredProcedure
                                              };
            _command.Int("Id", education.ID);
            _command.Int("CandidateID", candidateID);
            _command.Varchar("Degree", 100, education.Degree);
            _command.Varchar("College", 255, education.College);
            _command.Varchar("State", 100, education.State);
            _command.Varchar("Country", 100, education.Country);
            _command.Varchar("Year", 10, education.Year);
            _command.Varchar("User", 10, user);
            await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
            if (_reader.HasRows)
            {
                while (_reader.Read())
                {
                    _education.Add(new(_reader.GetInt32(0), _reader.GetString(1), _reader.GetString(2), _reader.GetString(3), _reader.GetString(4),
                                       _reader.GetString(5), _reader.GetString(6)));
                }
            }

            await _reader.CloseAsync();
        }
        catch
        {
            //
        }

        await _connection.CloseAsync();

        return new()
               {
                   {
                       "Education", _education
                   }
               };
    }

    /// <summary>
    /// </summary>
    /// <param name="experience"></param>
    /// <param name="candidateID"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<Dictionary<string, object>> SaveExperience(CandidateExperience experience, [FromQuery] int candidateID, [FromQuery] string user)
    {
        await Task.Delay(1);
        List<CandidateExperience> _experiences = new();
        if (experience == null)
        {
            return new()
                   {
                       {
                           "Experience", _experiences
                       }
                   };
        }

        await using SqlConnection _connection = new(_configuration.GetConnectionString("DBConnect"));
        await _connection.OpenAsync();
        try
        {
            await using SqlCommand _command = new("SaveExperience", _connection)
                                              {
                                                  CommandType = CommandType.StoredProcedure
                                              };
            _command.Int("Id", experience.ID);
            _command.Int("CandidateID", candidateID);
            _command.Varchar("Employer", 100, experience.Employer);
            _command.Varchar("Start", 10, experience.Start);
            _command.Varchar("End", 10, experience.End);
            _command.Varchar("Location", 100, experience.Location);
            _command.Varchar("Description", 1000, experience.Description);
            _command.Varchar("Title", 1000, experience.Title);
            _command.Varchar("User", 10, user);
            await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
            if (_reader.HasRows)
            {
                while (_reader.Read())
                {
                    _experiences.Add(new(_reader.GetInt32(0), _reader.GetString(1), _reader.GetString(2), _reader.GetString(3), _reader.GetString(4),
                                         _reader.GetString(5), _reader.GetString(6), _reader.GetString(7)));
                }
            }

            await _reader.CloseAsync();
        }
        catch
        {
            //
        }

        await _connection.CloseAsync();

        return new()
               {
                   {
                       "Experience", _experiences
                   }
               };
    }

    /// <summary>
    /// </summary>
    /// <param name="mpc"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<Dictionary<string, object>> SaveMPC(CandidateRatingMPC mpc, [FromQuery] string user)
    {
        string _mpcNotes = "";
        List<CandidateMPC> _mpc = new();
        if (mpc == null)
        {
            return new()
                   {
                       {
                           "MPCList", _mpc
                       },
                       {
                           "FirstMPC", null
                       }
                   };
        }

        await using SqlConnection _connection = new(_configuration.GetConnectionString("DBConnect"));
        await _connection.OpenAsync();
        try
        {
            await using SqlCommand _command = new("ChangeMPC", _connection)
                                              {
                                                  CommandType = CommandType.StoredProcedure
                                              };
            _command.Int("@CandidateId", mpc.ID);
            _command.Bit("@MPC", mpc.MPC);
            _command.Varchar("@Notes", -1, mpc.MPCComments);
            _command.Varchar("@From", 10, user);
            _mpcNotes = _command.ExecuteScalar().ToString();
        }
        catch
        {
            //
        }

        await _connection.CloseAsync();

        string[] _mpcArray = _mpcNotes?.Split('?');
        _mpc.AddRange((_mpcArray ?? Array.Empty<string>())
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

        return new()
               {
                   {
                       "MPCList", _mpc
                   },
                   {
                       "FirstMPC", mpc
                   }
               };
    }

    /// <summary>
    /// </summary>
    /// <param name="rating"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<Dictionary<string, object>> SaveRating(CandidateRatingMPC rating, [FromQuery] string user)
    {
        string _ratingNotes = "";
        List<CandidateRating> _rating = new();
        if (rating == null)
        {
            return new()
                   {
                       {
                           "RatingList", _rating
                       },
                       {
                           "FirstRating", null
                       }
                   };
        }

        await using SqlConnection _connection = new(_configuration.GetConnectionString("DBConnect"));
        await _connection.OpenAsync();
        try
        {
            await using SqlCommand _command = new("ChangeRating", _connection)
                                              {
                                                  CommandType = CommandType.StoredProcedure
                                              };
            _command.Int("@CandidateId", rating.ID);
            _command.TinyInt("@Rating", rating.Rating);
            _command.Varchar("@Notes", -1, rating.RatingComments);
            _command.Varchar("@From", 10, user);
            _ratingNotes = _command.ExecuteScalar().ToString();
        }
        catch
        {
            //
        }

        await _connection.CloseAsync();

        string[] _ratingArray = _ratingNotes?.Split('?');
        _rating.AddRange((_ratingArray ?? Array.Empty<string>())
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

        return new()
               {
                   {
                       "RatingList", _rating
                   },
                   {
                       "FirstRating", rating
                   }
               };
    }

    /// <summary>
    /// </summary>
    /// <param name="skill"></param>
    /// <param name="candidateID"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<Dictionary<string, object>> SaveSkill(CandidateSkills skill, [FromQuery] int candidateID, [FromQuery] string user)
    {
        await Task.Delay(1);
        List<CandidateSkills> _skills = new();
        if (skill == null)
        {
            return new()
                   {
                       {
                           "Skills", _skills
                       }
                   };
        }

        await using SqlConnection _connection = new(_configuration.GetConnectionString("DBConnect"));
        await _connection.OpenAsync();
        try
        {
            await using SqlCommand _command = new("SaveSkill", _connection)
                                              {
                                                  CommandType = CommandType.StoredProcedure
                                              };
            _command.Int("EntitySkillId", skill.ID);
            _command.Varchar("Skill", 100, skill.Skill);
            _command.Int("CandidateID", candidateID);
            _command.SmallInt("LastUsed", skill.LastUsed);
            _command.SmallInt("ExpMonth", skill.ExpMonth);
            _command.Varchar("User", 10, user);
            await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
            if (_reader.HasRows)
            {
                while (_reader.Read())
                {
                    _skills.Add(new(_reader.GetInt32(0), _reader.GetString(1), _reader.GetInt16(2), _reader.GetInt16(3), _reader.GetString(4)));
                }
            }

            await _reader.CloseAsync();
        }
        catch
        {
            //
        }

        await _connection.CloseAsync();

        return new()
               {
                   {
                       "Skills", _skills
                   }
               };
    }

    /// <summary>
    /// </summary>
    /// <param name="submissionID"></param>
    /// <param name="user"></param>
    /// <param name="roleID"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<Dictionary<string, object>> UndoCandidateActivity(int submissionID, [FromQuery] string user, [FromQuery] string roleID = "RS")
    {
        await Task.Delay(1);
        List<CandidateActivity> _activities = new();
        if (submissionID == 0)
        {
            return new()
                   {
                       {
                           "Activity", null
                       }
                   };
        }

        await using SqlConnection _connection = new(_configuration.GetConnectionString("DBConnect"));
        await _connection.OpenAsync();
        try
        {
            await using SqlCommand _command = new("UndoCandidateActivity", _connection)
                                              {
                                                  CommandType = CommandType.StoredProcedure
                                              };
            _command.Int("Id", submissionID);
            _command.Varchar("User", 10, user);
            _command.Bit("CandScreen", true);
            _command.Char("RoleID", 2, roleID);
            await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
            if (_reader.HasRows)
            {
                while (_reader.Read())
                {
                    _activities.Add(new(_reader.GetString(0), _reader.GetDateTime(1), _reader.GetString(2), _reader.GetInt32(3), _reader.GetInt32(4),
                                        _reader.GetString(5), _reader.GetString(6), _reader.GetInt32(7), _reader.GetBoolean(8), _reader.GetString(9),
                                        _reader.GetString(10), _reader.GetString(11), _reader.GetBoolean(12), _reader.GetString(13), _reader.GetInt32(14),
                                        _reader.GetString(15), _reader.GetInt32(16), _reader.GetString(17), _reader.GetBoolean(18),
                                        _reader.NDateTime(19), _reader.GetString(20), _reader.NString(21), _reader.NString(22),
                                        _reader.GetBoolean(23)));
                }
            }

            await _reader.CloseAsync();
        }
        catch
        {
            //
        }

        await _connection.CloseAsync();

        return new()
               {
                   {
                       "Activity", _activities
                   }
               };
    }

    /// <summary>
    /// </summary>
    [HttpPost]
    public async Task<Dictionary<string, object>> UploadDocument()
    {
        await Task.Delay(1);
        string _fileName = Request.Form.Files[0].FileName;
        string _candidateID = Request.Form["candidateID"].ToString();
        string _mime = Request.Form.Files[0].ContentDisposition;
        string _internalFileName = Guid.NewGuid().ToString("N");
        string _destinationFileName = Path.Combine(Request.Form["path"].ToString(), "Uploads", "Candidate", _candidateID, _internalFileName);

        await using MemoryStream _stream = new();
        await using FileStream _fs = System.IO.File.Open(_destinationFileName, FileMode.OpenOrCreate, FileAccess.Write);
        try
        {
            await Request.Form.Files[0].CopyToAsync(_fs);
            _fs.Flush();
            _fs.Close();
        }
        catch
        {
            _fs.Close();
        }

        await using SqlConnection _connection = new(_configuration.GetConnectionString("DBConnect"));
        await _connection.OpenAsync();
        List<CandidateDocument> _documents = new();
        try
        {
            await using SqlCommand _command = new("SaveCandidateDocuments", _connection)
                                              {
                                                  CommandType = CommandType.StoredProcedure
                                              };
            _command.Int("CandidateId", _candidateID);
            _command.Varchar("DocumentName", 255, Request.Form["name"].ToString());
            _command.Varchar("DocumentLocation", 255, _fileName);
            _command.Varchar("DocumentNotes", 2000, Request.Form["notes"].ToString());
            _command.Varchar("InternalFileName", 50, _internalFileName);
            _command.Int("DocumentType", Request.Form["type"].ToInt32());
            _command.Varchar("DocsUser", 10, Request.Form["user"].ToString());
            await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
            if (_reader.HasRows)
            {
                while (_reader.Read())
                {
                    _documents.Add(new(_reader.GetInt32(0), _reader.GetString(1), _reader.GetString(2), _reader.GetString(3), $"{_reader.NDateTime(4)} [{_reader.NString(5)}]",
                                       _reader.GetString(6), _reader.GetString(7), _reader.GetInt32(8)));
                }
            }

            await _reader.CloseAsync();
        }
        catch
        {
            //
        }

        return new()
               {
                   {
                       "Document", _documents
                   }
               };
    }
}