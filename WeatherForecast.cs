#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           WeatherForecast.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 21:38
// Last Updated On:     01-04-2022 16:12
// *****************************************/

#endregion

namespace ProfSvc_WebAPI;

public class WeatherForecast
{
    #region Properties

    public DateTime Date
    {
        get;
        set;
    }

    public int TemperatureC
    {
        get;
        set;
    }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string Summary
    {
        get;
        set;
    }

    #endregion
}