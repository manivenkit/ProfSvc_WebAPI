#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           CandidateRating.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-30-2021 15:07
// Last Updated On:     01-04-2022 16:12
// *****************************************/

#endregion

namespace ProfSvc_WebAPI.Data;

/// <summary>
/// </summary>
public class CandidateRating
{
    #region Constructors

    /// <summary>
    /// </summary>
    public CandidateRating()
    {
        ClearData();
    }

    /// <summary>
    /// </summary>
    /// <param name="date"></param>
    /// <param name="user"></param>
    /// <param name="rating"></param>
    /// <param name="comments"></param>
    public CandidateRating(DateTime date, string user, byte rating, string comments)
    {
        Date = date;
        User = user;
        Rating = rating;
        Comments = comments;
    }

    #endregion

    #region Properties

    /// <summary>
    /// </summary>
    public DateTime Date
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public int Rating
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string User
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string Comments
    {
        get;
        set;
    }

    #endregion

    #region Methods

    /// <summary>
    /// </summary>
    public void ClearData()
    {
        Date = DateTime.Today;
        User = "";
        Rating = 0;
        Comments = "";
    }

    #endregion
}