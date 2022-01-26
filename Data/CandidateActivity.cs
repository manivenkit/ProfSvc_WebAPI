#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           CandidateActivity.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:12
// *****************************************/

#endregion

namespace ProfSvc_WebAPI.Data;

/// <summary>
/// </summary>
public class CandidateActivity
{
    #region Constructors

	/// <summary>
	/// </summary>
	public CandidateActivity()
    {
        ClearData();
    }

	/// <summary>
	/// </summary>
	/// <param name="requisition"></param>
	/// <param name="updatedDate"></param>
	/// <param name="updatedBy"></param>
	/// <param name="positions"></param>
	/// <param name="positionFilled"></param>
	/// <param name="status"></param>
	/// <param name="notes"></param>
	/// <param name="id"></param>
	/// <param name="schedule"></param>
	/// <param name="appliesTo"></param>
	/// <param name="color"></param>
	/// <param name="icon"></param>
	/// <param name="doRoleHaveRight"></param>
	/// <param name="lastActionBy"></param>
	/// <param name="requisitionID"></param>
	/// <param name="candidateUpdatedBy"></param>
	/// <param name="countSubmitted"></param>
	public CandidateActivity(string requisition, DateTime updatedDate, string updatedBy, int positions, int positionFilled, string status, string notes, int id,
							 bool schedule,
							 string appliesTo, string color, string icon, bool doRoleHaveRight, string lastActionBy, int requisitionID,
							 string candidateUpdatedBy,
							 int countSubmitted)
    {
        Requisition = requisition;
        UpdatedDate = updatedDate;
        UpdatedBy = updatedBy;
        Positions = positions;
        PositionFilled = positionFilled;
        Status = status;
        Notes = notes;
        ID = id;
        Schedule = schedule;
        AppliesTo = appliesTo;
        Color = color;
        Icon = icon;
        DoRoleHaveRight = doRoleHaveRight;
        LastActionBy = lastActionBy;
        RequisitionID = requisitionID;
        CandidateUpdatedBy = candidateUpdatedBy;
        CountSubmitted = countSubmitted;
    }

    #endregion

    #region Properties

	/// <summary>
	/// </summary>
	public bool DoRoleHaveRight
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public bool Schedule
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public DateTime UpdatedDate
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public int CountSubmitted
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public int ID
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public int PositionFilled
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public int Positions
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public int RequisitionID
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public string AppliesTo
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public string CandidateUpdatedBy
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public string Color
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public string Icon
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public string LastActionBy
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public string Notes
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public string Requisition
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public string Status
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
        Requisition = "";
        UpdatedDate = DateTime.MinValue;
        UpdatedBy = "";
        Positions = 0;
        PositionFilled = 0;
        Status = "";
        Notes = "";
        ID = 0;
        Schedule = false;
        AppliesTo = "";
        Color = "#000000";
        Icon = "";
        DoRoleHaveRight = false;
        LastActionBy = "";
        RequisitionID = 0;
        CandidateUpdatedBy = "";
        CountSubmitted = 0;
    }

    #endregion
}