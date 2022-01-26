#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           WeatherForecastService.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:12
// *****************************************/

#endregion

#region Using

#endregion

namespace ProfSvc_AppTrack.Data;

public class WeatherForecastService
{
    #region Properties

    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    #endregion

    #region Methods

    public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
    {
        Random rng = new Random();

        return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
                                                                      {
                                                                          Date = startDate.AddDays(index), TemperatureC = rng.Next(-20, 55),
                                                                          Summary = Summaries[rng.Next(Summaries.Length)]
                                                                      }).ToArray());
    }

    #endregion
}