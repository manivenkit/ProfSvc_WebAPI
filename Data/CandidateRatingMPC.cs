#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           CandidateRatingMPC.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-03-2022 16:20
// Last Updated On:     01-04-2022 16:12
// *****************************************/

#endregion

namespace ProfSvc_WebAPI.Data;

public class CandidateRatingMPC
{
    #region Constructors

    public CandidateRatingMPC()
    {
        ClearData();
    }

    public CandidateRatingMPC(int id, int rating, string ratingComments, bool mpc, string mpcComments)
    {
        ID = id;
        Rating = rating;
        RatingComments = ratingComments;
        MPC = mpc;
        MPCComments = mpcComments;
    }

    #endregion

    #region Properties

    public bool MPC
    {
        get;
        set;
    }

    public int ID
    {
        get;
        set;
    }

    public int Rating
    {
        get;
        set;
    }

    public string RatingComments
    {
        get;
        set;
    }

    public string MPCComments
    {
        get;
        set;
    }

    #endregion

    #region Methods

    public void ClearData()
    {
        Rating = 0;
        RatingComments = "";
        MPC = false;
        MPCComments = "";
    }

    #endregion
}