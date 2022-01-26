#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           WeatherForecastController.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 21:38
// Last Updated On:     01-04-2022 16:12
// *****************************************/

#endregion

namespace ProfSvc_WebAPI.Controllers;

[ApiController, Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    #region Constructors

    public WeatherForecastController(ILogger<WeatherForecastController> logger) => _logger = logger;

    #endregion

    #region Fields

    private readonly ILogger<WeatherForecastController> _logger;

    #endregion

    #region Properties

    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    #endregion

    #region Methods

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                                                      {
                                                          Date = DateTime.Now.AddDays(index),
                                                          TemperatureC = Random.Shared.Next(-20, 55),
                                                          Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                                                      })
                         .ToArray();
    }

    #endregion
}