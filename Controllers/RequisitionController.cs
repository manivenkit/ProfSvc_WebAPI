#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           RequisitionController.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          03-18-2022 20:58
// Last Updated On:     03-19-2022 16:11
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
    /// <param name="searchModel"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<Dictionary<string, object>> GetGridRequisitions([FromBody] RequisitionSearch searchModel)
    {
        await using SqlConnection _connection = new(_configuration.GetConnectionString("DBConnect"));
        List<Requisitions> _requisitions = new();
        await using SqlCommand _command = new("GetGridRequisitions", _connection)
                                          {
                                              CommandType = CommandType.StoredProcedure
                                          };
        _command.Int("Count", searchModel.ItemCount);
        _command.Int("Page", searchModel.Page);
        _command.Int("SortRow", searchModel.SortField);
        _command.TinyInt("SortOrder", searchModel.SortDirection);
        _command.Varchar("Code", 15, searchModel.Code);
        _command.Varchar("Title", 2000, searchModel.Title);
        _command.Varchar("Company", 2000, searchModel.Company);
        _command.Varchar("Option", 30, searchModel.Option);
        _command.Varchar("Status", 1000, searchModel.Status);
        _command.Varchar("CreatedBy", 10, searchModel.CreatedBy);
        _command.DateTime("CreatedOn", searchModel.CreatedOn);
        _command.DateTime("CreatedOnEnd", searchModel.CreatedOnEnd);
        _command.DateTime("Due", searchModel.Due);
        _command.DateTime("DueEnd", searchModel.DueEnd);
        _command.Bit("Recruiter", searchModel.Recruiter);
        _command.Varchar("User", 10, searchModel.User);

        await _connection.OpenAsync();
        await using SqlDataReader _reader = await _command.ExecuteReaderAsync();

        _reader.Read();
        int _count = _reader.GetInt32(0);

        _reader.NextResult();

        while (_reader.Read())
        {
            _requisitions.Add(new(_reader.GetInt32(0), _reader.GetString(1), _reader.GetString(2), _reader.GetString(3), _reader.GetString(4), _reader.GetString(5),
                                  _reader.GetString(6) + " [" + _reader.GetString(7) + "]", _reader.GetString(8), _reader.GetString(9), GetPriority(_reader.GetByte(10)),
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