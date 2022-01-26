#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           Designation.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:12
// *****************************************/

#endregion

namespace ProfSvc_WebAPI.Data;

/// <summary>
///     Class to store the Designation.
/// </summary>
public class Designation
{
    #region Constructors

    public Designation()
    {
    }

	/// <summary>
	///     Initializes the Designation Class.
	/// </summary>
	/// <param name="id">ID of the Designation.</param>
	/// <param name="designation">Name of the Designation.</param>
	public Designation(int id, string designation)
    {
        ID = id;
        DesignationValue = designation;
    }

    #endregion

    #region Properties

	/// <summary>
	///     ID of the Designation.
	/// </summary>
	public int ID
    {
        get;
    }

	/// <summary>
	///     Name of the Designation.
	/// </summary>
	public string DesignationValue
    {
        get;
    }

    #endregion
}