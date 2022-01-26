#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           StatusDrop.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-30-2021 15:19
// Last Updated On:     01-04-2022 16:12
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Data;

public class StatusDrop
{
    #region Constructors

	/// <summary>
	///     Initializes the StatusDrop class with default values.
	/// </summary>
	public StatusDrop()
    {
        ClearData();
    }

	/// <summary>
	///     Initializes the StatusDrop class with supplied values.
	/// </summary>
	/// <param name="code">The Status Code.</param>
	/// <param name="status">Name of the Status Code.</param>
	public StatusDrop(string code, string status)
    {
        Code = code;
        Status = status;
    }

    #endregion

    #region Properties

	/// <summary>
	///     The Status Code.
	/// </summary>
	public string Code
    {
        get;
        set;
    }

	/// <summary>
	///     Name of the Status Code.
	/// </summary>
	public string Status
    {
        get;
        set;
    }

    #endregion

    #region Methods

	/// <summary>
	///     Creates a shallow copy of the current object.
	/// </summary>
	/// <returns></returns>
	public StatusDrop Copy() => (StatusDrop)MemberwiseClone();

	/// <summary>
	///     Clears this object data.
	/// </summary>
	public void ClearData()
    {
        Code = "";
        Status = "";
    }

    #endregion
}