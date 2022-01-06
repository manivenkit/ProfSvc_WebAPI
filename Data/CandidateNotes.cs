#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           CandidateNotes.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:12
// *****************************************/

#endregion

namespace ProfSvc_WebAPI.Data;

/// <summary>
/// </summary>
public class CandidateNotes
{
    #region Constructors

	/// <summary>
	/// </summary>
	public CandidateNotes()
    {
        ClearData();
    }

	/// <summary>
	/// </summary>
	/// <param name="id"></param>
	/// <param name="updatedDate"></param>
	/// <param name="updatedBy"></param>
	/// <param name="notes"></param>
	public CandidateNotes(int id, DateTime updatedDate, string updatedBy, string notes)
    {
        ID = id;
        UpdatedDate = updatedDate;
        UpdatedBy = updatedBy;
        Notes = notes;
    }

    #endregion

    #region Properties

	/// <summary>
	/// </summary>
	public DateTime UpdatedDate
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
	public string Notes
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
        UpdatedDate = DateTime.MinValue;
        UpdatedBy = "";
        Notes = "";
    }

    #endregion
}