#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           RequisitionController.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          03-18-2022 20:58
// Last Updated On:     07-23-2022 15:51
// *****************************************/

#endregion

namespace ProfSvc_WebAPI.Controllers;

[ApiController, Route("api/[controller]/[action]")]
public class RequisitionController : ControllerBase
{
    /// <summary>
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="env"></param>
    public RequisitionController(IConfiguration configuration, IWebHostEnvironment env)
    {
        _configuration = configuration;
        _hostingEnvironment = env;
    }

    private readonly IConfiguration _configuration;

    private readonly IWebHostEnvironment _hostingEnvironment;

    /// <summary>
    /// </summary>
    /// <param name="reqSearch"></param>
    /// <param name="getCompanyInformation"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<Dictionary<string, object>> GetGridRequisitions([FromBody] RequisitionSearch reqSearch, [FromQuery] bool getCompanyInformation = false)
    {
        await using SqlConnection _connection = new(_configuration.GetConnectionString("DBConnect"));
        List<Requisitions> _requisitions = new();
        await using SqlCommand _command = new("GetGridRequisitions", _connection)
                                          {
                                              CommandType = CommandType.StoredProcedure
                                          };
        _command.Int("Count", reqSearch.ItemCount);
        _command.Int("Page", reqSearch.Page);
        _command.Int("SortRow", reqSearch.SortField);
        _command.TinyInt("SortOrder", reqSearch.SortDirection);
        _command.Varchar("Code", 15, reqSearch.Code);
        _command.Varchar("Title", 2000, reqSearch.Title);
        _command.Varchar("Company", 2000, reqSearch.Company);
        _command.Varchar("Option", 30, reqSearch.Option);
        _command.Varchar("Status", 1000, reqSearch.Status);
        _command.Varchar("CreatedBy", 10, reqSearch.CreatedBy);
        _command.DateTime("CreatedOn", reqSearch.CreatedOn);
        _command.DateTime("CreatedOnEnd", reqSearch.CreatedOnEnd);
        _command.DateTime("Due", reqSearch.Due);
        _command.DateTime("DueEnd", reqSearch.DueEnd);
        _command.Bit("Recruiter", reqSearch.Recruiter);
        _command.Bit("GetCompanyInformation", getCompanyInformation);
        _command.Varchar("User", 10, reqSearch.User);

        await _connection.OpenAsync();
        await using SqlDataReader _reader = await _command.ExecuteReaderAsync();

        _reader.Read();
        int _count = _reader.GetInt32(0);

        _reader.NextResult();

        while (_reader.Read())
        {
            _requisitions.Add(new(_reader.GetInt32(0), _reader.GetString(1), _reader.GetString(2), _reader.GetString(3), _reader.GetString(4), _reader.GetString(5),
                                  $"{_reader.GetDateTime(6).ToString("d", new CultureInfo("en-us"))} [{_reader.GetString(7)}]",
                                  _reader.GetDateTime(8).ToString("d", new CultureInfo("en-us")), _reader.GetString(9), GetPriority(_reader.GetByte(10)),
                                  _reader.GetBoolean(11), _reader.GetBoolean(12), _reader.GetBoolean(13), _reader.GetString(14), _reader.GetString(15)));
        }

        List<Company> _companies = new();
        List<CompanyContact> _companyContacts = new();
        List<IntValues> _skills = new();

        if (getCompanyInformation)
        {
            _reader.NextResult();
            while (_reader.Read())
            {
                _companies.Add(new(_reader.GetInt32(0), _reader.GetString(1)));
            }

            _reader.NextResult();
            while (_reader.Read())
            {
                _companyContacts.Add(new(_reader.GetInt32(0), _reader.GetInt32(2), _reader.GetString(1)));
            }

            _reader.NextResult();
            while (_reader.Read())
            {
                _skills.Add(new(_reader.GetInt32(0), _reader.GetString(1)));
            }
        }

        await _reader.CloseAsync();

        await _connection.CloseAsync();

        return new()
               {
                   {
                       "Requisitions", _requisitions
                   },
                   {
                       "Companies", _companies
                   },
                   {
                       "Contacts", _companyContacts
                   },
                   {
                       "Skills", _skills
                   },
                   {
                       "Count", _count
                   }
               };
    }

    /// <summary>
    /// </summary>
    /// <param name="requisitionID"></param>
    /// <param name="roleID"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<Dictionary<string, object>>> GetRequisitionDetails([FromQuery] int requisitionID, [FromQuery] string roleID = "RC")
    {
        await using SqlConnection _connection = new(_configuration.GetConnectionString("DBConnect"));
        RequisitionDetails _requisitionDetail = null;

        await using SqlCommand _command = new("GetGridRequisitionDetailsView", _connection)
                                          {
                                              CommandType = CommandType.StoredProcedure
                                          };
        _command.Int("RequisitionID", requisitionID);
        _command.Varchar("RoleID", 2, roleID);
        await _connection.OpenAsync();
        await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
        if (_reader.HasRows) //Candidate Details
        {
            _reader.Read();
            try
            {
                _requisitionDetail = new(requisitionID, _reader.GetString(0), _reader.NString(1), _reader.GetString(2), _reader.GetString(3), _reader.GetInt32(4),
                                         _reader.GetString(5), _reader.GetString(6), _reader.GetString(38), _reader.GetString(8), _reader.GetDecimal(9),
                                         _reader.GetDecimal(10), _reader.GetDecimal(11), _reader.GetDecimal(12), _reader.GetDecimal(13), _reader.GetBoolean(14),
                                         _reader.GetString(15), _reader.NString(16), _reader.GetDecimal(17), _reader.GetDecimal(18), _reader.GetBoolean(19),
                                         _reader.GetDateTime(20), _reader.GetString(21), _reader.GetString(22), _reader.GetString(23), _reader.GetDateTime(24),
                                         _reader.GetString(25), _reader.GetDateTime(26), _reader.NString(27), _reader.NString(28), _reader.NString(29),
                                         _reader.GetBoolean(30), _reader.GetBoolean(31), _reader.NString(32), _reader.GetBoolean(33), _reader.GetDateTime(34),
                                         _reader.GetBoolean(35), _reader.GetString(39), _reader.GetInt32(7), _reader.GetString(40), _reader.NString(41),
                                         _reader.NString(42), _reader.NString(43), _reader.GetByte(44), _reader.NInt32(45), _reader.NInt32(46),
                                         _reader.NInt32(47), _reader.NString(48), _reader.GetInt32(36), _reader.GetInt32(37), _reader.NString(49));
            }
            catch (Exception)
            {
                //
            }
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

        _reader.NextResult();
        List<RequisitionDocuments> _documents = new();
        if (_reader.HasRows)
        {
            while (_reader.Read())
            {
                try
                {
                    _documents.Add(new(_reader.GetInt32(0), _reader.GetInt32(1), _reader.NString(2), _reader.NString(3), _reader.NString(6),
                                       $"{_reader.NDateTime(5)} [{_reader.NString(4)}]", _reader.NString(7)));
                }
                catch (Exception ex)
                {
                    //
                }
            }
        }


        await _reader.CloseAsync();

        await _connection.CloseAsync();

        return new Dictionary<string, object>
               {
                   {
                       "Requisition", _requisitionDetail
                   },
                   {
                       "Activity", _activity
                   },
                   {
                       "Documents", _documents
                   }
               };
    }

    /// <summary>
    /// </summary>
    /// <param name="documentID"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<Dictionary<string, object>> DeleteRequisitionDocument([FromQuery] int documentID, [FromQuery] string user)
    {
        await Task.Delay(1);
        await using SqlConnection _connection = new(_configuration.GetConnectionString("DBConnect"));
        await _connection.OpenAsync();
        List<RequisitionDocuments> _documents = new();
        try
        {
            await using SqlCommand _command = new("DeleteRequisitionDocuments", _connection)
                                              {
                                                  CommandType = CommandType.StoredProcedure
                                              };
            _command.Int("RequisitionDocId", documentID);
            _command.Varchar("User", 10, user);
            await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
            _reader.NextResult();
            if (_reader.HasRows)
            {
                while (_reader.Read())
                {
                    _documents.Add(new(_reader.GetInt32(0), _reader.GetInt32(1), _reader.NString(2), _reader.NString(3), _reader.NString(6),
                                       $"{_reader.NDateTime(5)} [{_reader.NString(4)}]", _reader.NString(7)));
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
    /// <param name="requisitionDetails"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> SaveRequisition(RequisitionDetails requisitionDetails, string fileName = "", string mimeType = "")
    {
        if (requisitionDetails == null)
        {
            return -1;
        }

        if (!fileName.NullOrWhiteSpace())
        {
        }

        await using SqlConnection _connection = new(_configuration.GetConnectionString("DBConnect"));
        await _connection.OpenAsync();
        int _returnCode = 0;
        try
        {
            await using SqlCommand _command = new("SaveRequisition", _connection)
                                              {
                                                  CommandType = CommandType.StoredProcedure
                                              };
            _command.Int("@RequisitionId", requisitionDetails.RequisitionID, true);
            _command.Int("@Company", requisitionDetails.CompanyID);
            _command.Int("@HiringMgr", requisitionDetails.ContactID);
            _command.Varchar("@City", 50, requisitionDetails.City);
            _command.Int("@StateId", requisitionDetails.StateID);
            _command.Varchar("@Zip", 10, requisitionDetails.ZipCode);
            _command.TinyInt("@IsHot", requisitionDetails.PriorityID);
            _command.Varchar("@Title", 200, requisitionDetails.PositionTitle);
            _command.Varchar("@Description", -1, requisitionDetails.Description);
            _command.Int("@Positions", requisitionDetails.Positions);
            _command.DateTime("@ExpStart", requisitionDetails.ExpectedStart);
            _command.DateTime("@Due", requisitionDetails.DueDate);
            _command.Int("@Education", requisitionDetails.EducationID);
            _command.Varchar("@Skills", 2000, requisitionDetails.SkillsRequired);
            _command.Varchar("@OptionalRequirement", 8000, requisitionDetails.Optional);
            _command.Char("@JobOption", 1, requisitionDetails.JobOptionID);
            _command.Int("@ExperienceID", requisitionDetails.ExperienceID);
            _command.Int("@Eligibility", requisitionDetails.EligibilityID);
            _command.Varchar("@Duration", 50, requisitionDetails.Duration);
            _command.Char("@DurationCode", 1, requisitionDetails.DurationCode);
            _command.Decimal("@ExpRateLow", 9, 2, requisitionDetails.ExpRateLow);
            _command.Decimal("@ExpRateHigh", 9, 2, requisitionDetails.ExpRateHigh);
            _command.Decimal("@ExpLoadLow", 9, 2, requisitionDetails.ExpLoadLow);
            _command.Decimal("@ExpLoadHigh", 9, 2, requisitionDetails.ExpLoadHigh);
            _command.Decimal("@SalLow", 9, 2, requisitionDetails.SalaryLow);
            _command.Decimal("@SalHigh", 9, 2, requisitionDetails.SalaryHigh);
            _command.Bit("@ExpPaid", requisitionDetails.ExpensesPaid);
            _command.Char("@Status", 3, requisitionDetails.StatusCode);
            _command.Bit("@Security", requisitionDetails.SecurityClearance);
            _command.Decimal("@PlacementFee", 8, 2, requisitionDetails.PlacementFee);
            _command.Varchar("@BenefitsNotes", -1, requisitionDetails.BenefitNotes);
            _command.Bit("@OFCCP", requisitionDetails.OFCCP);
            _command.Varchar("@User", 10, "JOLLY");
            _command.Varchar("@Assign", 550, requisitionDetails.AssignedTo);

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
    /// <returns></returns>
    [HttpPost]
    public async Task<string> UploadBenefitsDocument()
    {
        string _fileData = Request.Form["fileData"].ToString();
        string _name = _fileData.Split('^')[0];

        new DirectoryInfo(_hostingEnvironment.ContentRootPath + "Upload\\Benefits").Create();
        string _filename = _hostingEnvironment.ContentRootPath + $"Upload\\Benefits\\{_name}";

        await using FileStream _fs = System.IO.File.Open(_filename, FileMode.Create, FileAccess.Write);
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

        return _filename;
    }

    /// <summary>
    /// </summary>
    [HttpPost]
    public async Task<Dictionary<string, object>> UploadDocument()
    {
        await Task.Delay(1);
        string _fileName = Request.Form.Files[0].FileName;
        string _requisitionID = Request.Form["requisitionID"].ToString();
        string _mime = Request.Form.Files[0].ContentDisposition;
        string _internalFileName = Guid.NewGuid().ToString("N");
        Directory.CreateDirectory(Path.Combine(Request.Form["path"].ToString(), "Uploads", "Requisition", _requisitionID));
        string _destinationFileName = Path.Combine(Request.Form["path"].ToString(), "Uploads", "Requisition", _requisitionID, _internalFileName);

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
        List<RequisitionDocuments> _documents = new();
        try
        {
            await using SqlCommand _command = new("SaveRequisitionDocuments", _connection)
                                              {
                                                  CommandType = CommandType.StoredProcedure
                                              };
            _command.Int("RequisitionId", _requisitionID);
            _command.Varchar("DocumentName", 255, Request.Form["name"].ToString());
            _command.Varchar("DocumentLocation", 255, _fileName);
            _command.Varchar("DocumentNotes", 2000, Request.Form["notes"].ToString());
            _command.Varchar("InternalFileName", 50, _internalFileName);
            _command.Varchar("DocsUser", 10, Request.Form["user"].ToString());
            await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
            if (_reader.HasRows)
            {
                while (_reader.Read())
                {
                    _documents.Add(new(_reader.GetInt32(0), _reader.GetInt32(1), _reader.NString(2), _reader.NString(3), _reader.NString(6),
                                       $"{_reader.NDateTime(5)} [{_reader.NString(4)}]", _reader.NString(7)));
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

    private static string GetPriority(byte priority)
    {
        return priority switch
               {
                   0 => "Low",
                   2 => "High",
                   _ => "Medium"
               };
    }
}