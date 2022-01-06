#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           AdminList.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:12
// *****************************************/

#endregion

namespace ProfSvc_WebAPI.Data;

/// <summary>
/// </summary>
public class AdminList
{
    #region Constructors

	/// <summary>
	///     Initializes the Admin List Class.
	/// </summary>
	public AdminList()
    {
        ID = 0;
        Text = "";
        CreatedDate = "";
        UpdatedDate = "";
        Enabled = "Active";
        IsEnabled = true;
    }

	/// <summary>
	///     Initializes the Admin List Class.
	/// </summary>
	/// <param name="id">ID of the Entity.</param>
	/// <param name="text">Text of the Entity.</param>
	/// <param name="created">Created Date of the Entity.</param>
	/// <param name="updated">Last Updated Date of the Entity.</param>
	/// <param name="enabled">Text Status for the Entity.</param>
	/// <param name="isEnabled">Status of the Entity.</param>
	public AdminList(int id, string text, string created, string updated, string enabled = "Active", bool isEnabled = true)
    {
        ID = id;
        Text = text;
        CreatedDate = created;
        UpdatedDate = updated;
        Enabled = enabled;
        IsEnabled = isEnabled;
    }

	/// <summary>
	///     Initializes the Admin List Class.
	/// </summary>
	/// <param name="code">Code of the Entity.</param>
	/// <param name="text">Text of the Entity.</param>
	/// <param name="created">Created Date of the Entity.</param>
	/// <param name="updated">Last Updated Date of the Entity.</param>
	/// <param name="enabled">Text Status for the Entity.</param>
	/// <param name="isEnabled">Status of the Entity.</param>
	public AdminList(string code, string text, string created, string updated, string enabled = "Active", bool isEnabled = true)
    {
        Code = code;
        Text = text;
        CreatedDate = created;
        UpdatedDate = updated;
        Enabled = enabled;
        IsEnabled = isEnabled;
    }

    #endregion

    #region Properties

	/// <summary>
	///     Status of the Entity.
	/// </summary>
	public bool IsEnabled
    {
        get;
        set;
    }

	/// <summary>
	///     ID of the Entity.
	/// </summary>
	public int ID
    {
        get;
        set;
    }

	/// <summary>
	///     Code of the Entity.
	/// </summary>
	public string Code
    {
        get;
        set;
    }

	/// <summary>
	///     Created Date of the Entity.
	/// </summary>
	public string CreatedDate
    {
        get;
        set;
    }

	/// <summary>
	///     Text Status for the Entity.
	/// </summary>
	public string Enabled
    {
        get;
        set;
    }

	/// <summary>
	///     Text of the Entity.
	/// </summary>
	[Required(ErrorMessage = "This field is required."),
	 StringLength(100, MinimumLength = 2, ErrorMessage = "Length should be between 2 and 100 characters.")]
    public string Text
    {
        get;
        set;
    }

	/// <summary>
	///     Last Updated Date of the Entity.
	/// </summary>
	public string UpdatedDate
    {
        get;
        set;
    }

    #endregion
}