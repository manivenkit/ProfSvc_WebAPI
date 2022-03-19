#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           AdminController.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-26-2022 19:30
// Last Updated On:     02-18-2022 15:49
// *****************************************/

#endregion

namespace ProfSvc_WebAPI.Controllers;

[ApiController, Route("api/[controller]")]
public class AdminController : ControllerBase
{
    /// <summary>
    ///     Initializes the AdminController class with the IConfiguration Service.
    /// </summary>
    /// <param name="configuration"></param>
    public AdminController(IConfiguration configuration) => _config = configuration;

    private readonly IConfiguration _config;

    /// <summary>
    ///     Checks whether the Code already exists.
    /// </summary>
    /// <param name="methodName">Name of the stored procedure to execute.</param>
    /// <param name="code">Code to check.</param>
    /// <param name="isString">Whether the Code supplied is string or integer?</param>
    /// <returns></returns>
    [HttpGet("CheckCode")]
    public async Task<bool> CheckCode(string methodName = "", string code = "", bool isString = false)
    {
        await using SqlConnection _connection = new(_config.GetConnectionString("DBConnect"));
        await using SqlCommand _command = new(methodName, _connection)
                                          {
                                              CommandType = CommandType.StoredProcedure
                                          };
        if (!isString)
        {
            _command.Int("@ID", code.ToInt32());
        }
        else
        {
            _command.Char("@Code", 1, code);
        }

        _connection.Open();

        return _command.ExecuteScalar().ToBoolean();
    }

    /// <summary>
    ///     Checks whether the Code already exists.
    /// </summary>
    /// <param name="id">ID to check.</param>
    /// <returns></returns>
    [HttpGet("CheckRole")]
    public async Task<bool> CheckRole(string id = "")
    {
        await using SqlConnection _connection = new(_config.GetConnectionString("DBConnect"));
        await using SqlCommand _command = new("Admin_CheckRoleID", _connection)
                                          {
                                              CommandType = CommandType.StoredProcedure
                                          };
        _command.Char("@ID", 2, id);

        _connection.Open();

        return _command.ExecuteScalar().ToBoolean();
    }

    /// <summary>
    ///     Checks whether the Code already exists.
    /// </summary>
    /// <param name="code">ID to check.</param>
    /// <returns></returns>
    [HttpGet("CheckState")]
    public async Task<bool> CheckState(string code = "")
    {
        await using SqlConnection _connection = new(_config.GetConnectionString("DBConnect"));
        await using SqlCommand _command = new("Admin_CheckStateCode", _connection)
                                          {
                                              CommandType = CommandType.StoredProcedure
                                          };
        _command.Char("@Code", 2, code);

        _connection.Open();

        return _command.ExecuteScalar().ToBoolean();
    }

    /// <summary>
    ///     Gets the AdminList object from the supplied method name.
    /// </summary>
    /// <param name="methodName">Name of the stored procedure to execute.</param>
    /// <param name="filter">Filter the result using the supplied string.</param>
    /// <param name="isString">Whether the Code is string or integer?</param>
    /// <returns>JSON containing AdminList object and Count.</returns>
    [HttpGet("GetAdminList")]
    public async Task<Dictionary<string, object>> GetAdminList(string methodName, string filter = "", bool isString = true)
    {
        await using SqlConnection _connection = new(_config.GetConnectionString("DBConnect"));
        List<AdminList> _generalItems = new();
        await using SqlCommand _command = new(methodName, _connection)
                                          {
                                              CommandType = CommandType.StoredProcedure
                                          };

        if (!filter.NullOrWhiteSpace())
        {
            _command.Varchar("@Filter", 100, filter);
        }

        _connection.Open();
        await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
        while (_reader.Read())
        {
            if (isString)
            {
                _generalItems.Add(new(_reader.GetString(0), _reader.GetString(1), _reader.GetString(2), _reader.GetString(3), _reader.GetString(4),
                                      _reader.GetBoolean(5)));
            }
            else
            {
                _generalItems.Add(new(_reader.GetInt32(0), _reader.GetString(1), _reader.GetString(2), _reader.GetString(3), _reader.GetString(4),
                                      _reader.GetBoolean(5)));
            }
        }

        _reader.NextResult();
        _reader.Read();
        int _count = _reader.GetInt32(0);

        return new()
               {
                   {
                       "GeneralItems", _generalItems
                   },
                   {
                       "Count", _count
                   }
               };
    }

    [HttpGet("GetCache")]
    public async Task<Dictionary<string, object>> GetCache()
    {
        await using SqlConnection _connection = new(_config.GetConnectionString("DBConnect"));
        List<IntValues> _states = new();
        List<IntValues> _eligibility = new();
        List<KeyValues> _jobOptions = new();
        List<KeyValues> _taxTerms = new();
        List<IntValues> _skills = new();
        List<IntValues> _experience = new();
        List<Template> _templates = new();
        List<User> _users = new();
        List<StatusCode> _statusCodes = new();
        List<Zip> _zips = new();
        List<IntValues> _education = new();
        List<Company> _companies = new();
        List<CompanyContact> _companyContacts = new();
        List<Role> _roles = new();
        List<IntValues> _titles = new();
        List<IntValues> _leadSources = new();
        List<IntValues> _leadIndustries = new();
        List<IntValues> _leadStatus = new();
        List<CommissionConfigurator> _commissionConfigurators = new();
        List<VariableCommission> _variableCommissions = new();
        List<Workflow> _workflows = new();
        List<IntValues> _documentTypes = new();
        await using SqlCommand _command = new("SetCacheTables", _connection)
                                          {
                                              CommandType = CommandType.StoredProcedure
                                          };

        _connection.Open();
        await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
        if (_reader.HasRows) //States
        {
            while (_reader.Read())
            {
                _states.Add(new(_reader.GetInt32(0), _reader.GetString(1)));
            }
        }

        _reader.NextResult();
        if (_reader.HasRows) //Eligibility
        {
            while (_reader.Read())
            {
                _eligibility.Add(new(_reader.GetInt32(0), _reader.GetString(1)));
            }
        }

        _reader.NextResult();
        if (_reader.HasRows) //Job Options
        {
            while (_reader.Read())
            {
                _jobOptions.Add(new(_reader.GetString(0), _reader.GetString(1)));
            }
        }

        _reader.NextResult();
        if (_reader.HasRows) //Tax Terms
        {
            while (_reader.Read())
            {
                _taxTerms.Add(new(_reader.GetString(0), _reader.GetString(1)));
            }
        }

        _reader.NextResult();
        if (_reader.HasRows) //Skills
        {
            while (_reader.Read())
            {
                _skills.Add(new(_reader.GetInt32(1), _reader.GetString(0)));
            }
        }

        _reader.NextResult();
        if (_reader.HasRows) //Experience
        {
            while (_reader.Read())
            {
                _experience.Add(new(_reader.GetInt32(0), _reader.GetString(1)));
            }
        }

        _reader.NextResult();
        if (_reader.HasRows) //Templates
        {
            while (_reader.Read())
            {
                _templates.Add(new(_reader.GetInt32(0), _reader.GetString(1), _reader.GetString(2), _reader.GetString(3), _reader.GetString(4)));
            }
        }

        _reader.NextResult();
        if (_reader.HasRows) //Users
        {
            while (_reader.Read())
            {
                _users.Add(new(_reader.GetString(0), _reader.GetString(1), _reader.GetString(2), _reader.GetString(3)));
            }
        }

        _reader.NextResult();
        if (_reader.HasRows) //Status Codes
        {
            while (_reader.Read())
            {
                _statusCodes.Add(new(_reader.GetInt32(6), _reader.GetString(0), _reader.GetString(1), _reader.NString(2), _reader.GetString(3),
                                     _reader.GetBoolean(4), _reader.GetBoolean(5)));
            }
        }

        _reader.NextResult();
        if (_reader.HasRows) //Zips
        {
            while (_reader.Read())
            {
                _zips.Add(new(_reader.GetString(0), _reader.GetString(1)));
            }
        }

        _reader.NextResult();
        if (_reader.HasRows) //Education
        {
            while (_reader.Read())
            {
                _education.Add(new(_reader.GetInt32(0), _reader.GetString(1)));
            }
        }

        _reader.NextResult();
        if (_reader.HasRows) //Companies
        {
            while (_reader.Read())
            {
                _companies.Add(new(_reader.GetInt32(0), _reader.GetString(1), _reader.GetString(2), _reader.GetString(3), _reader.GetInt32(4), _reader.GetString(5)));
            }
        }

        _reader.NextResult();
        if (_reader.HasRows) //Company Contacts
        {
            while (_reader.Read())
            {
                _companyContacts.Add(new(_reader.GetInt32(0), _reader.GetInt32(1), _reader.GetString(2), _reader.GetString(3), _reader.GetInt32(4),
                                         _reader.GetString(5), _reader.GetString(6)));
            }
        }

        _reader.NextResult();
        if (_reader.HasRows) //Roles
        {
            while (_reader.Read())
            {
                _roles.Add(new(_reader.GetString(0), _reader.GetString(1), _reader.GetBoolean(2), _reader.GetBoolean(3), _reader.GetBoolean(4),
                               _reader.GetBoolean(5), _reader.GetBoolean(6), _reader.GetBoolean(7), _reader.GetBoolean(8), _reader.GetBoolean(9),
                               _reader.GetBoolean(10), _reader.GetBoolean(11), _reader.GetBoolean(12), _reader.GetBoolean(13), _reader.GetString(14)));
            }
        }

        _reader.NextResult();
        if (_reader.HasRows) //Titles
        {
            while (_reader.Read())
            {
                _titles.Add(new(_reader.GetInt32(0), _reader.GetString(1)));
            }
        }

        _reader.NextResult();
        if (_reader.HasRows) //Lead Sources
        {
            while (_reader.Read())
            {
                _leadSources.Add(new(_reader.GetInt32(0), _reader.GetString(1)));
            }
        }

        _reader.NextResult();
        if (_reader.HasRows) //Lead Industries
        {
            while (_reader.Read())
            {
                _leadIndustries.Add(new(_reader.GetInt32(0), _reader.GetString(1)));
            }
        }

        _reader.NextResult();
        if (_reader.HasRows) //Lead Status
        {
            while (_reader.Read())
            {
                _leadStatus.Add(new(_reader.GetInt32(0), _reader.GetString(1)));
            }
        }

        _reader.NextResult();
        if (_reader.HasRows) //Commission Configurators
        {
            while (_reader.Read())
            {
                _commissionConfigurators.Add(new(_reader.GetInt32(0), _reader.GetInt16(1), _reader.GetInt16(2), _reader.GetByte(3), _reader.GetByte(4)));
            }
        }

        _reader.NextResult();
        if (_reader.HasRows) //Variable Commission
        {
            while (_reader.Read())
            {
                _variableCommissions.Add(new(_reader.GetInt32(0), _reader.GetInt16(1), _reader.GetByte(2), _reader.GetByte(3), _reader.GetByte(4),
                                             _reader.GetByte(5)));
            }
        }

        _reader.NextResult();
        // ReSharper disable once InvertIf
        if (_reader.HasRows) //Workflow
        {
            while (_reader.Read())
            {
                _workflows.Add(new(_reader.GetInt32(0), _reader.GetString(1), _reader.NString(2), _reader.GetBoolean(3), _reader.GetString(4),
                                   _reader.GetBoolean(5), _reader.GetBoolean(6)));
            }
        }

        _reader.NextResult();
        if (_reader.HasRows) //Document Types
        {
            while (_reader.Read())
            {
                _documentTypes.Add(new(_reader.GetInt32(0), _reader.GetString(1)));
            }
        }

        return new()
               {
                   {
                       "States", _states
                   },
                   {
                       "Eligibility", _eligibility
                   },
                   {
                       "JobOptions", _jobOptions
                   },
                   {
                       "TaxTerms", _taxTerms
                   },
                   {
                       "Skills", _skills
                   },
                   {
                       "Experience", _experience
                   },
                   {
                       "Templates", _templates
                   },
                   {
                       "Users", _users
                   },
                   {
                       "StatusCodes", _statusCodes
                   },
                   {
                       "Zips", _zips
                   },
                   {
                       "Education", _education
                   },
                   {
                       "Companies", _companies
                   },
                   {
                       "CompanyContacts", _companyContacts
                   },
                   {
                       "Roles", _roles
                   },
                   {
                       "Titles", _titles
                   },
                   {
                       "LeadSources", _leadSources
                   },
                   {
                       "LeadIndustries", _leadIndustries
                   },
                   {
                       "LeadStatus", _leadStatus
                   },
                   {
                       "CommissionConfigurators", _commissionConfigurators
                   },
                   {
                       "VariableCommissions", _variableCommissions
                   },
                   {
                       "Workflow", _workflows
                   },
                   {
                       "DocumentTypes", _documentTypes
                   }
               };
    }

    /// <summary>
    ///     Gets the JobOptions.
    /// </summary>
    /// <param name="filter">Filter the result using the supplied string.</param>
    /// <param name="setTaxTerm">Should TaxTerm be returned or not?</param>
    /// <returns>JSON containing JobOptions object and Count.</returns>
    [HttpGet("GetJobOptions")]
    public async Task<Dictionary<string, object>> GetJobOptions(string filter = "", bool setTaxTerm = true)
    {
        await using SqlConnection _connection = new(_config.GetConnectionString("DBConnect"));
        List<JobOption> _jobOptions = new();
        await using SqlCommand _command = new("Admin_GetJobOptions", _connection)
                                          {
                                              CommandType = CommandType.StoredProcedure
                                          };
        if (!filter.NullOrWhiteSpace())
        {
            _command.Varchar("@Filter", 100, filter);
        }

        _connection.Open();
        await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
        while (_reader.Read())
        {
            _jobOptions.Add(new(_reader.GetString(0), _reader.GetString(1), _reader.GetString(2), _reader.GetString(3),
                                _reader.GetBoolean(4), _reader.GetBoolean(5), _reader.GetBoolean(6), _reader.GetString(7),
                                _reader.GetBoolean(8), _reader.GetBoolean(9), _reader.GetBoolean(10), _reader.GetBoolean(11),
                                _reader.GetString(12), _reader.GetString(13), _reader.GetDecimal(14), _reader.GetBoolean(15)));
        }

        _reader.NextResult();
        _reader.Read();
        int _count = _reader.GetInt32(0);

        _reader.NextResult();
        if (!_reader.HasRows || !setTaxTerm)
        {
            return new()
                   {
                       {
                           "JobOptions", _jobOptions
                       },
                       {
                           "Count", _count
                       },
                       {
                           "TaxTerms", null
                       }
                   };
        }

        List<KeyValues> _taxTermKeyValues = new();
        while (_reader.Read())
        {
            _taxTermKeyValues.Add(new(_reader.GetString(0), _reader.GetString(1)));
        }

        return new()
               {
                   {
                       "JobOptions", _jobOptions
                   },
                   {
                       "Count", _count
                   },
                   {
                       "TaxTerms", _taxTermKeyValues
                   }
               };
    }

    /// <summary>
    ///     Gets the Roles.
    /// </summary>
    /// <param name="filter">Filter the result using the supplied string.</param>
    /// <returns>JSON containing Roles object and Count.</returns>
    [HttpGet("GetRoles")]
    public async Task<Dictionary<string, object>> GetRoles(string filter = "")
    {
        await using SqlConnection _connection = new(_config.GetConnectionString("DBConnect"));
        _connection.Open();
        await using SqlCommand _command = new("Admin_GetRoles", _connection)
                                          {
                                              CommandType = CommandType.StoredProcedure
                                          };
        _command.Varchar("@Filter", 100, filter);
        List<Role> _roles = new();
        await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
        while (_reader.Read())
        {
            _roles.Add(new(_reader.GetString(0), _reader.GetString(1), _reader.GetBoolean(2), _reader.GetBoolean(3), _reader.GetBoolean(4),
                           _reader.GetBoolean(5), _reader.GetBoolean(6), _reader.GetBoolean(7), _reader.GetBoolean(8), _reader.GetBoolean(9),
                           _reader.GetBoolean(10), _reader.GetBoolean(11), _reader.GetBoolean(12), _reader.GetBoolean(13), _reader.GetString(14)));
        }

        _reader.NextResult();
        _reader.Read();
        int _count = _reader.GetInt32(0);

        return new()
               {
                   {
                       "Roles", _roles
                   },
                   {
                       "Count", _count
                   }
               };
    }

    /// <summary>
    ///     Gets the Search Box AutoComplete list.
    /// </summary>
    /// <param name="methodName">Name of the stored procedure to execute.</param>
    /// <param name="paramName">Name of the parameter to pass to the stored procedure.</param>
    /// <param name="filter">Filter the result using the supplied string.</param>
    /// <returns>JSON containing a list of string.</returns>
    [HttpGet("SearchDropDown")]
    public async Task<List<string>> GetSearchDropDown(string methodName = "", string paramName = "", string filter = "")
    {
        SqlConnection _connection = new(_config.GetConnectionString("DBConnect"));
        await using SqlCommand _command = new(methodName, _connection)
                                          {
                                              CommandType = CommandType.StoredProcedure
                                          };
        _command.Varchar(paramName, 100, filter);

        _connection.Open();
        await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
        List<string> _listOptions = new();
        while (_reader.Read())
        {
            _listOptions.Add(_reader.GetString(0));
        }

        return _listOptions;
    }

    /// <summary>
    ///     Gets the Search Box AutoComplete list for JobOptions.
    /// </summary>
    /// <param name="filter">Filter the result using the supplied string.</param>
    /// <returns></returns>
    [HttpGet("SearchJobOptions")]
    public async Task<List<string>> GetSearchJobOptions(string filter = "")
    {
        SqlConnection _connection = new(_config.GetConnectionString("DBConnect"));
        await using SqlCommand _command = new("[Admin_SearchJobOption]", _connection)
                                          {
                                              CommandType = CommandType.StoredProcedure
                                          };
        _command.Varchar("@JobOption", 100, filter);

        _connection.Open();
        await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
        List<string> _listOptions = new();
        while (_reader.Read())
        {
            _listOptions.Add(_reader.GetString(0));
        }

        return _listOptions;
    }

    /// <summary>
    ///     Gets the States.
    /// </summary>
    /// <param name="filter">Filter the result using the supplied string.</param>
    /// <returns>JSON containing States object and Count.</returns>
    [HttpGet("GetStates")]
    public async Task<Dictionary<string, object>> GetStates(string filter = "")
    {
        await using SqlConnection _connection = new(_config.GetConnectionString("DBConnect"));
        _connection.Open();
        await using SqlCommand _command = new("Admin_GetStates", _connection)
                                          {
                                              CommandType = CommandType.StoredProcedure
                                          };
        _command.Varchar("@Filter", 100, filter);
        List<State> _state = new();
        await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
        while (_reader.Read())
        {
            _state.Add(new(_reader.GetInt32(0), _reader.GetString(1), _reader.GetString(2)));
        }

        _reader.NextResult();
        _reader.Read();
        int _count = _reader.GetInt32(0);

        return new()
               {
                   {
                       "States", _state
                   },
                   {
                       "Count", _count
                   }
               };
    }

    /// <summary>
    ///     Gets the Status Codes.
    /// </summary>
    /// <param name="filter">Filter the result using the supplied string.</param>
    /// <returns>JSON containing Status Codes object and Count.</returns>
    [HttpGet("GetStatusCodes")]
    public async Task<Dictionary<string, object>> GetStatusCodes(string filter = "")
    {
        await using SqlConnection _connection = new(_config.GetConnectionString("DBConnect"));
        _connection.Open();
        await using SqlCommand _command = new("Admin_GetStatusCodes", _connection)
                                          {
                                              CommandType = CommandType.StoredProcedure
                                          };
        _command.Varchar("@Filter", 100, filter);
        List<StatusCode> _statusCodes = new();
        await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
        while (_reader.Read())
        {
            _statusCodes.Add(new(_reader.GetInt32(0), _reader.GetString(1), _reader.GetString(2), _reader.GetString(3), _reader.GetString(4),
                                 _reader.GetString(5), _reader.GetString(6), _reader.GetBoolean(7), _reader.GetBoolean(8), _reader.GetString(9),
                                 _reader.GetString(10), _reader.GetString(11)));
        }

        _reader.NextResult();
        _reader.Read();
        int _count = _reader.GetInt32(0);

        return new()
               {
                   {
                       "StatusCodes", _statusCodes
                   },
                   {
                       "Count", _count
                   }
               };
    }

    /// <summary>
    ///     Saves the AdminList object.
    /// </summary>
    /// <param name="methodName">Name of the stored procedure to execute.</param>
    /// <param name="parameterName">Name of the parameter to pass to the stored procedure.</param>
    /// <param name="containDescription">Does the supplied object contain description?</param>
    /// <param name="isString">Whether the Code is string or integer?</param>
    /// <param name="adminList">Object of type AdminList.</param>
    /// <returns></returns>
    [HttpPost("SaveAdminList")]
    public async Task<string> SaveAdminList([FromQuery] string methodName, [FromQuery] string parameterName, [FromQuery] bool containDescription, [FromQuery] bool isString,
                                            [FromBody] AdminList adminList)
    {
        await using SqlConnection _con = new(_config.GetConnectionString("DBConnect"));
        _con.Open();
        string _returnCode = "";
        try
        {
            await using SqlCommand _command = new(methodName, _con)
                                              {
                                                  CommandType = CommandType.StoredProcedure
                                              };
            if (isString)
            {
                _command.Char("@Code", 1, adminList.Code.DbNull());
            }
            else
            {
                _command.Int("@ID", adminList.ID.DbNull());
            }

            _command.Varchar("@" + parameterName, 50, adminList.Text);
            if (containDescription)
            {
                _command.Varchar("@Desc", 500, adminList.Text);
            }

            _command.Varchar("@User", 10, "ADMIN");
            _command.Bit("@Enabled", adminList.IsEnabled);

            await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
            if (!_reader.HasRows)
            {
                _returnCode = "";
            }
            else
            {
                _reader.Read();
                _returnCode = _reader.GetValue(0).ToString();
            }
        }
        catch
        {
            // ignored
        }

        return _returnCode;
    }

    /// <summary>
    ///     Save a Job Option.
    /// </summary>
    /// <param name="jobOption">Object of type JobOption.</param>
    /// <returns>String containing the Job Options code.</returns>
    [HttpPost("SaveJobOptions")]
    public async Task<string> SaveJobOptions(JobOption jobOption)
    {
        await using SqlConnection _con = new(_config.GetConnectionString("DBConnect"));
        _con.Open();

        try
        {
            await using SqlCommand _command = new("Admin_SaveJobOptions", _con)
                                              {
                                                  CommandType = CommandType.StoredProcedure
                                              };
            _command.Char("@Code", 1, jobOption.Code);
            _command.Varchar("@JobOptions", 50, jobOption.Option);
            _command.Varchar("@Desc", 500, jobOption.Description);
            _command.Bit("@Duration", jobOption.Duration);
            _command.Bit("@Rate", jobOption.Rate);
            _command.Bit("@Sal", jobOption.Sal);
            _command.Varchar("@TaxTerms", 20, jobOption.Tax);
            _command.Bit("@Expenses", jobOption.Exp);
            _command.Bit("@PlaceFee", jobOption.PlaceFee);
            _command.Bit("@Benefits", jobOption.Benefits);
            _command.Bit("@ShowHours", jobOption.ShowHours);
            _command.Varchar("@RateText", 255, jobOption.RateText);
            _command.Varchar("@PercentText", 255, jobOption.PercentText);
            _command.Decimal("@CostPercent", 5, 2, jobOption.CostPercent);
            _command.Bit("@ShowPercent", jobOption.ShowPercent);
            _command.Varchar("@User", 10, "ADMIN");

            _command.ExecuteNonQuery();
        }
        catch
        {
            // ignored
        }

        return jobOption.Code;
    }

    /// <summary>
    ///     Save a Role.
    /// </summary>
    /// <param name="role">Object of type Role.</param>
    /// <returns>String containing the code of the Role.</returns>
    [HttpPost("SaveRole")]
    public async Task<string> SaveRole(Role role)
    {
        await using SqlConnection _con = new(_config.GetConnectionString("DBConnect"));
        _con.Open();

        try
        {
            await using SqlCommand _command = new("Admin_SaveRole", _con)
                                              {
                                                  CommandType = CommandType.StoredProcedure
                                              };
            _command.Char("@Code", 2, role.ID);
            _command.Varchar("@Role", 50, role.RoleName);
            _command.Varchar("@Desc", 200, role.Description);
            _command.Bit("@ViewCandidate", role.ViewCandidate);
            _command.Bit("@ViewRequisition", role.ViewRequisition);
            _command.Bit("@EditCandidate", role.EditCandidate);
            _command.Bit("@EditRequisition", role.EditRequisition);
            _command.Bit("@ChangeCandidateStatus", role.ChangeCandidateStatus);
            _command.Bit("@ChangeRequisitionStatus", role.ChangeRequisitionStatus);
            _command.Bit("@SendEmail", role.SendEmailCandidate);
            _command.Bit("@ForwardResume", role.ForwardResume);
            _command.Bit("@DownloadResume", role.DownloadResume);
            _command.Bit("@SubmitCandidate", role.SubmitCandidate);
            _command.Bit("@ViewClients", role.ViewClients);
            _command.Bit("@EditClients", role.EditClients);
            _command.Varchar("@User", 10, "ADMIN");

            _command.ExecuteNonQuery();
        }
        catch
        {
            // ignored
        }

        return role.ID;
    }

    /// <summary>
    ///     Save a State.
    /// </summary>
    /// <param name="state">Object of type State.</param>
    /// <returns>Integer containing the id of the State.</returns>
    [HttpPost("SaveState")]
    public async Task<int> SaveState(State state)
    {
        await using SqlConnection _con = new(_config.GetConnectionString("DBConnect"));
        _con.Open();
        int _id = 0;
        try
        {
            await using SqlCommand _command = new("Admin_SaveState", _con)
                                              {
                                                  CommandType = CommandType.StoredProcedure
                                              };
            _command.Int("@ID", state.ID.DbNull());
            _command.Char("@Code", 2, state.Code);
            _command.Varchar("@State", 50, state.StateName);
            _command.Varchar("@Country", 50, "USA");
            _command.Varchar("@User", 10, "ADMIN");

            _id = _command.ExecuteScalar().ToInt32();
        }
        catch
        {
            // ignored
        }

        return _id;
    }

    /// <summary>
    ///     Save a Status Code.
    /// </summary>
    /// <param name="statusCode">Object of type StatusCode.</param>
    /// <returns>String containing the code of the Status.</returns>
    [HttpPost("SaveStatusCode")]
    public async Task<int> SaveStatusCode(StatusCode statusCode)
    {
        await using SqlConnection _con = new(_config.GetConnectionString("DBConnect"));
        _con.Open();
        int _id = 0;

        try
        {
            await using SqlCommand _command = new("Admin_SaveStatusCode", _con)
                                              {
                                                  CommandType = CommandType.StoredProcedure
                                              };
            _command.Int("@ID", statusCode.ID.DbNull());
            _command.Char("@Code", 3, statusCode.Code);
            _command.Varchar("@Status", 50, statusCode.Status);
            _command.Varchar("@Desc", 100, statusCode.Description);
            _command.Char("@AppliesTo", 3, statusCode.AppliesToCode);
            _command.Varchar("@Icon", 255, statusCode.Icon);
            _command.Varchar("@Color", 10, statusCode.Color);
            _command.Bit("@SubmitCandidate", statusCode.SubmitCandidate);
            _command.Bit("@ShowCommission", statusCode.ShowCommission);
            _command.Varchar("@User", 10, "ADMIN");

            _id = _command.ExecuteScalar().ToInt32();
        }
        catch
        {
            // ignored
        }

        return _id;
    }

    /// <summary>
    ///     Toggles the status of the Admin list Object.
    /// </summary>
    /// <param name="methodName">Name of the stored procedure to execute.</param>
    /// <param name="id">ID of the object.</param>
    /// <param name="userName">User Name of the user executing the method.</param>
    /// <param name="idIsString">Is ID a string or integer?</param>
    /// <returns></returns>
    [HttpPost("ToggleAdminList")]
    public async Task<string> ToggleAdminList(string methodName, string id, string userName, bool idIsString)
    {
        await using SqlConnection _con = new(_config.GetConnectionString("DBConnect"));
        _con.Open();
        try
        {
            await using SqlCommand _command = new(methodName, _con)
                                              {
                                                  CommandType = CommandType.StoredProcedure
                                              };
            if (!idIsString)
            {
                _command.Int("@ID", id.ToInt32());
            }
            else
            {
                _command.Char("@Code", 1, id);
            }

            _command.Varchar("@User", 10, userName);
            _command.ExecuteNonQuery();
        }
        catch
        {
            // ignored
        }

        return id;
    }
}
