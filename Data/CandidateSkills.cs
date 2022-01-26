#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           CandidateSkills.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:12
// *****************************************/

#endregion

namespace ProfSvc_WebAPI.Data;

/// <summary>
/// </summary>
public class CandidateSkills
{
    #region Constructors

	/// <summary>
	/// </summary>
	public CandidateSkills()
    {
        ClearData();
    }

	/// <summary>
	/// </summary>
	/// <param name="id"></param>
	/// <param name="skill"></param>
	/// <param name="lastUsed"></param>
	/// <param name="expMonth"></param>
	/// <param name="updatedBy"></param>
	public CandidateSkills(int id, string skill, short lastUsed, short expMonth, string updatedBy)
    {
        ID = id;
        Skill = skill;
        LastUsed = lastUsed;
        ExpMonth = expMonth;
        UpdatedBy = updatedBy;
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
	public short ExpMonth
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public short LastUsed
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public string Skill
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
        Skill = "";
        LastUsed = 0;
        ExpMonth = 0;
        UpdatedBy = "";
    }

    #endregion
}