#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           Zips.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-30-2021 15:19
// Last Updated On:     01-04-2022 16:12
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Data;

/// <summary>
///     Class to store the Zip Codes.
/// </summary>
public class Zips
{
    #region Constructors

    /// <summary>
    ///     Initializes the Zips Class.
    /// </summary>
    /// <param name="zip">Zip Code of the city.</param>
    /// <param name="city">Name of the City.</param>
    public Zips(string zip, string city)
    {
        ZipCode = zip;
        City = city;
    }

    #endregion

    #region Properties

    /// <summary>
    ///     Name of the City.
    /// </summary>
    public string City
    {
        get;
    }

    /// <summary>
    ///     Zip Code of the city.
    /// </summary>
    public string ZipCode
    {
        get;
    }

    #endregion
}