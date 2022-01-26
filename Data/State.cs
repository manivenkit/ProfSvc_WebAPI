#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           State.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:12
// *****************************************/

#endregion

namespace ProfSvc_WebAPI.Data;

/// <summary>
///     Summary description for States
/// </summary>
public class State
{
    #region Constructors

	/// <summary>
	///     Initialize the States Class with default data.
	/// </summary>
	public State()
    {
        ID = 0;
        StateName = "";
        Code = "";
    }

	/// <summary>
	///     Initialize the States Class with supplied data.
	/// </summary>
	/// <param name="id">ID of the State.</param>
	/// <param name="state">Name of the State.</param>
	/// <param name="code">Code for the State.</param>
	public State(int id, string state, string code)
    {
        ID = id;
        StateName = state;
        Code = code;
    }

    #endregion

    #region Properties

	/// <summary>
	///     ID of the State.
	/// </summary>
	public int ID
    {
        get;
        set;
    }

	/// <summary>
	///     Code for the State.
	/// </summary>
	public string Code
    {
        get;
        set;
    }

	/// <summary>
	///     Name of the State.
	/// </summary>
	public string StateName
    {
        get;
        set;
    }

    #endregion
}