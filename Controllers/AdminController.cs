#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           AdminController.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 21:39
// Last Updated On:     01-04-2022 16:11
// *****************************************/

#endregion

namespace ProfSvc_WebAPI.Controllers;

[ApiController, Route("api/[controller]")]
public class AdminController : ControllerBase
{
    #region Constructors

    /// <summary>
    ///     Initializes the AdminController class with the IConfiguration Service.
    /// </summary>
    /// <param name="configuration"></param>
    public AdminController(IConfiguration configuration) => _config = configuration;

    #endregion

    #region Fields

    private readonly IConfiguration _config;

    #endregion

    #region Methods

    /// <summary>
    ///     Checks whether the Code already exists.
    /// </summary>
    /// <param name="methodName">Name of the stored procedure to execute.</param>
    /// <param name="code">Code to check.</param>
    /// <param name="isString">Whether the Code supplied is string or integer?</param>
    /// <returns></returns>
    [HttpGet("CheckCode")]
    public ActionResult<bool> CheckCode(string methodName = "", string code = "", bool isString = false)
    {
        SqlConnection _connection = new(_config.GetConnectionString("DBConnect"));
        using SqlCommand _command = new(methodName, _connection)
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
    public ActionResult<bool> CheckRole(string id = "")
    {
        SqlConnection _connection = new(_config.GetConnectionString("DBConnect"));
        using SqlCommand _command = new("Admin_CheckRoleID", _connection)
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
    public ActionResult<bool> CheckState(string code = "")
    {
        SqlConnection _connection = new(_config.GetConnectionString("DBConnect"));
        using SqlCommand _command = new("Admin_CheckStateCode", _connection)
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
    public ActionResult<Dictionary<string, object>> GetAdminList(string methodName, string filter = "", bool isString = true)
    {
        SqlConnection _connection = new(_config.GetConnectionString("DBConnect"));
        List<AdminList> _generalItems = new();
        using SqlCommand _command = new(methodName, _connection)
                                    {
                                        CommandType = CommandType.StoredProcedure
                                    };

        if (!filter.NullOrWhiteSpace())
        {
            _command.Varchar("@Filter", 100, filter);
        }

        _connection.Open();
        using SqlDataReader _reader = _command.ExecuteReader();
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

        return new Dictionary<string, object>
               {
                   {
                       "GeneralItems", _generalItems
                   },
                   {
                       "Count", _count
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
    public ActionResult<Dictionary<string, object>> GetJobOptions(string filter = "", bool setTaxTerm = true)
    {
        SqlConnection _connection = new(_config.GetConnectionString("DBConnect"));
        List<JobOption> _jobOptions = new();
        using SqlCommand _command = new("Admin_GetJobOptions", _connection)
                                    {
                                        CommandType = CommandType.StoredProcedure
                                    };
        if (!filter.NullOrWhiteSpace())
        {
            _command.Varchar("@Filter", 100, filter);
        }

        _connection.Open();
        using SqlDataReader _reader = _command.ExecuteReader();
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
            return new Dictionary<string, object>
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

        return new Dictionary<string, object>
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
    public ActionResult<Dictionary<string, object>> GetRoles(string filter = "")
    {
        using SqlConnection _connection = new(_config.GetConnectionString("DBConnect"));
        _connection.Open();
        using SqlCommand _command = new("Admin_GetRoles", _connection)
                                    {
                                        CommandType = CommandType.StoredProcedure
                                    };
        _command.Varchar("@Filter", 100, filter);
        List<Role> _roles = new();
        using SqlDataReader _reader = _command.ExecuteReader();
        while (_reader.Read())
        {
            _roles.Add(new(_reader.GetString(0), _reader.GetString(1), _reader.GetBoolean(2), _reader.GetBoolean(3), _reader.GetBoolean(4),
                           _reader.GetBoolean(5), _reader.GetBoolean(6), _reader.GetBoolean(7), _reader.GetBoolean(8), _reader.GetBoolean(9),
                           _reader.GetBoolean(10), _reader.GetBoolean(11), _reader.GetBoolean(12), _reader.GetBoolean(13), _reader.GetString(14)));
        }

        _reader.NextResult();
        _reader.Read();
        int _count = _reader.GetInt32(0);

        return new Dictionary<string, object>
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
    ///     Gets the States.
    /// </summary>
    /// <param name="filter">Filter the result using the supplied string.</param>
    /// <returns>JSON containing States object and Count.</returns>
    [HttpGet("GetStates")]
    public ActionResult<Dictionary<string, object>> GetStates(string filter = "")
    {
        using SqlConnection _connection = new(_config.GetConnectionString("DBConnect"));
        _connection.Open();
        using SqlCommand _command = new("Admin_GetStates", _connection)
                                    {
                                        CommandType = CommandType.StoredProcedure
                                    };
        _command.Varchar("@Filter", 100, filter);
        List<State> _state = new();
        using SqlDataReader _reader = _command.ExecuteReader();
        while (_reader.Read())
        {
            _state.Add(new(_reader.GetInt32(0), _reader.GetString(1), _reader.GetString(2)));
        }

        _reader.NextResult();
        _reader.Read();
        int _count = _reader.GetInt32(0);

        return new Dictionary<string, object>
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
    public ActionResult<Dictionary<string, object>> GetStatusCodes(string filter = "")
    {
        using SqlConnection _connection = new(_config.GetConnectionString("DBConnect"));
        _connection.Open();
        using SqlCommand _command = new("Admin_GetStatusCodes", _connection)
                                    {
                                        CommandType = CommandType.StoredProcedure
                                    };
        _command.Varchar("@Filter", 100, filter);
        List<StatusCode> _statusCodes = new();
        using SqlDataReader _reader = _command.ExecuteReader();
        while (_reader.Read())
        {
            _statusCodes.Add(new(_reader.GetInt32(0), _reader.GetString(1), _reader.GetString(2), _reader.GetString(3), _reader.GetString(4),
                                 _reader.GetString(5), _reader.GetString(6), _reader.GetBoolean(7), _reader.GetBoolean(8), _reader.GetString(9),
                                 _reader.GetString(10), _reader.GetString(11)));
        }

        _reader.NextResult();
        _reader.Read();
        int _count = _reader.GetInt32(0);

        return new Dictionary<string, object>
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
    ///     Save a State.
    /// </summary>
    /// <param name="state">Object of type State.</param>
    /// <returns>Integer containing the id of the State.</returns>
    [HttpPost("SaveState")]
    public ActionResult<int> SaveState(State state)
    {
        using SqlConnection _con = new(_config.GetConnectionString("DBConnect"));
        _con.Open();
        int _id = 0;
        try
        {
            using SqlCommand _command = new("Admin_SaveState", _con)
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
    public ActionResult<int> SaveStatusCode(StatusCode statusCode)
    {
        using SqlConnection _con = new(_config.GetConnectionString("DBConnect"));
        _con.Open();
        int _id = 0;

        try
        {
            using SqlCommand _command = new("Admin_SaveStatusCode", _con)
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
    ///     Gets the Search Box AutoComplete list.
    /// </summary>
    /// <param name="methodName">Name of the stored procedure to execute.</param>
    /// <param name="paramName">Name of the parameter to pass to the stored procedure.</param>
    /// <param name="filter">Filter the result using the supplied string.</param>
    /// <returns>JSON containing a list of string.</returns>
    [HttpGet("SearchDropDown")]
    public ActionResult<List<string>> GetSearchDropDown(string methodName = "", string paramName = "", string filter = "")
    {
        SqlConnection _connection = new(_config.GetConnectionString("DBConnect"));
        using SqlCommand _command = new(methodName, _connection)
                                    {
                                        CommandType = CommandType.StoredProcedure
                                    };
        _command.Varchar(paramName, 100, filter);

        _connection.Open();
        using SqlDataReader _reader = _command.ExecuteReader();
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
    public ActionResult<List<string>> GetSearchJobOptions(string filter = "")
    {
        SqlConnection _connection = new(_config.GetConnectionString("DBConnect"));
        using SqlCommand _command = new("[Admin_SearchJobOption]", _connection)
                                    {
                                        CommandType = CommandType.StoredProcedure
                                    };
        _command.Varchar("@JobOption", 100, filter);

        _connection.Open();
        using SqlDataReader _reader = _command.ExecuteReader();
        List<string> _listOptions = new();
        while (_reader.Read())
        {
            _listOptions.Add(_reader.GetString(0));
        }

        return _listOptions;
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
    public ActionResult<string> SaveAdminList([FromQuery]string methodName, [FromQuery]string parameterName, [FromQuery]bool containDescription, [FromQuery]bool isString, [FromBody]AdminList adminList)
    {
        using SqlConnection _con = new(_config.GetConnectionString("DBConnect"));
        _con.Open();
        string _returnCode = "";
        try
        {
            using SqlCommand _command = new(methodName, _con)
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

            using SqlDataReader _reader = _command.ExecuteReader();
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
    public ActionResult<string> SaveJobOptions(JobOption jobOption)
    {
        using SqlConnection _con = new(_config.GetConnectionString("DBConnect"));
        _con.Open();

        try
        {
            using SqlCommand _command = new("Admin_SaveJobOptions", _con)
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
    public ActionResult<string> SaveRole(Role role)
    {
        using SqlConnection _con = new(_config.GetConnectionString("DBConnect"));
        _con.Open();

        try
        {
            using SqlCommand _command = new("Admin_SaveRole", _con)
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
    ///     Toggles the status of the Admin list Object.
    /// </summary>
    /// <param name="methodName">Name of the stored procedure to execute.</param>
    /// <param name="id">ID of the object.</param>
    /// <param name="userName">User Name of the user executing the method.</param>
    /// <param name="idIsString">Is ID a string or integer?</param>
    /// <returns></returns>
    [HttpPost("ToggleAdminList")]
    public ActionResult<string>
        ToggleAdminList(string methodName, string id, string userName, bool idIsString) //, AdminList adminList)
    {
        using SqlConnection _con = new(_config.GetConnectionString("DBConnect"));
        _con.Open();
        try
        {
            using SqlCommand _command = new(methodName, _con)
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

    #endregion
}