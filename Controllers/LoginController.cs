#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           LoginController.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          02-12-2022 20:16
// Last Updated On:     02-12-2022 20:36
// *****************************************/

#endregion

namespace ProfSvc_WebAPI.Controllers;

[ApiController, Route("api/[controller]")]
public class LoginController : ControllerBase
{
    public LoginController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private readonly IConfiguration _configuration;

    /// <summary>
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <param name="ipAddress"></param>
    /// <returns></returns>
    [HttpPost("Login")]
    public async Task<LoginCooky> Login([FromQuery] string userName, [FromQuery] string password, [FromQuery] string ipAddress)
    {
        await Task.Yield();
        byte[] _password = Convert.FromBase64String(password);
        await using SqlConnection _connection = new(_configuration.GetConnectionString("DBConnect"));
        await using SqlCommand _command = new("ValidateCandidate", _connection)
                                          {
                                              CommandType = CommandType.StoredProcedure
                                          };
        _command.Varchar("@User", 10, userName);
        _command.Binary("@Password", 16, _password);
        _command.Varchar("@IP", 15, ipAddress);
        _connection.Open();
        await using SqlDataReader _reader = await _command.ExecuteReaderAsync();
        if (!_reader.HasRows)
        {
            return null;
        }

        _reader.Read();
        return new(userName, _reader.GetString(0), _reader.GetString(1), _reader.GetString(2), _reader.GetString(5), _reader.GetString(3),
                   _reader.IsDBNull(4) ? DateTime.MinValue : _reader.GetDateTime(4), _reader.NString(6));
    }
}