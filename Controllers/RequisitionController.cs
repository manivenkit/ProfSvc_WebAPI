﻿#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           RequisitionController.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          03-18-2022 20:58
// Last Updated On:     04-12-2022 15:44
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
    /// <returns></returns>
    [HttpGet]
    public async Task<Dictionary<string, object>> GetGridRequisitions([FromBody] RequisitionSearch reqSearch)
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

        await _reader.CloseAsync();

        await _connection.CloseAsync();

        return new()
               {
                   {
                       "Requisitions", _requisitions
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
        _command.Int("CandidateID", requisitionID);
        _command.Varchar("RoleID", 2, roleID);
        await _connection.OpenAsync();
        await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
        if (_reader.HasRows) //Candidate Details
        {
            _reader.Read();
            _requisitionDetail = new(_reader.GetString(0), _reader.GetString(1), _reader.GetString(2), _reader.GetString(3), _reader.GetInt32(4),
                                     _reader.GetString(5), _reader.GetString(6), _reader.GetString(7), _reader.GetString(8), _reader.GetDecimal(9),
                                     _reader.GetDecimal(10), _reader.GetDecimal(11), _reader.GetDecimal(12), _reader.GetDecimal(13), _reader.GetBoolean(14),
                                     _reader.GetString(15), _reader.NString(16), _reader.GetDecimal(17), _reader.GetDecimal(18), _reader.GetBoolean(19),
                                     _reader.GetDateTime(20), _reader.GetString(21), _reader.GetString(22), _reader.GetString(23), _reader.GetDateTime(24),
                                     _reader.GetString(25), _reader.GetDateTime(26), _reader.GetString(27), _reader.GetString(28), _reader.GetString(29),
                                     _reader.GetBoolean(30), _reader.GetBoolean(31), _reader.GetString(32), _reader.GetBoolean(33), _reader.GetDateTime(34),
                                     _reader.GetBoolean(35));
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

        await _reader.CloseAsync();

        await _connection.CloseAsync();

        return new Dictionary<string, object>
               {
                   {
                       "Candidate", _requisitionDetail
                   },
                   {
                       "Document", _activity
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