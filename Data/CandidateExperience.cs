#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           CandidateExperience.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:12
// *****************************************/

#endregion

namespace ProfSvc_WebAPI.Data;

/// <summary>
/// </summary>
public class CandidateExperience
{
    #region Constructors

	/// <summary>
	/// </summary>
	public CandidateExperience()
    {
        ClearData();
    }

	/// <summary>
	/// </summary>
	/// <param name="id"></param>
	/// <param name="employer"></param>
	/// <param name="start"></param>
	/// <param name="end"></param>
	/// <param name="location"></param>
	/// <param name="description"></param>
	/// <param name="updatedBy"></param>
	/// <param name="title"></param>
	public CandidateExperience(int id, string employer, string start, string end, string location, string description, string updatedBy, string title)
    {
        ID = id;
        Employer = employer;
        Start = start;
        End = end;
        Location = location;
        Description = description;
        UpdatedBy = updatedBy;
        Title = title;
    }

    #endregion

    #region Properties

	/// <summary>
	/// </summary>
	public int ID
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public string Description
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public string Employer
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public string End
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public string Location
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public string Start
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public string Title
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public string UpdatedBy
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
        ID = 0;
        Employer = "";
        Start = "";
        End = "";
        Location = "";
        Description = "";
        UpdatedBy = "";
        Title = "";
    }

    #endregion
}