#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           Candidates.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:12
// *****************************************/

#endregion

namespace ProfSvc_WebAPI.Data;

public class Candidates
{
    #region Constructors

    public Candidates()
    {
        ID = 0;
        Name = "";
        Phone = "";
        Email = "";
        Location = "";
        Updated = "";
        Status = "";
        Mpc = false;
        Rating = 3;
        OriginalResume = false;
        FormattedResume = false;
    }

	/// <summary>
	/// </summary>
	/// <param name="id"></param>
	/// <param name="name"></param>
	/// <param name="phone"></param>
	/// <param name="email"></param>
	/// <param name="location"></param>
	/// <param name="updatedDate"></param>
	/// <param name="updatedBy"></param>
	/// <param name="status"></param>
	/// <param name="mpc"></param>
	/// <param name="rating"></param>
	/// <param name="originalResume"></param>
	/// <param name="formattedResume"></param>
	public Candidates(int id, string name, string phone, string email, string location, string updated, string status, bool mpc,
					  byte rating,
					  bool originalResume, bool formattedResume) //, List<Note> notes)
    {
        ID = id;
        Name = name;
        Phone = phone;
        Email = email;
        Location = location;
        Updated = updated;
        Status = status;
        Mpc = mpc;
        Rating = rating;
        OriginalResume = originalResume;
        FormattedResume = formattedResume;
        //Notes = notes;
    }

    #endregion

    #region Properties

	/// <summary>
	/// </summary>
	public bool FormattedResume
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public bool Mpc
    {
        get;
        set;
    }

    /*/// <summary>
    /// </summary>
    //public List<Note> Notes
    //{
    //	get;
    //	set;
    //}*/

	/// <summary>
	/// </summary>
	public bool OriginalResume
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public byte Rating
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public int ID
    {
        set;
        get;
    }

	/// <summary>
	/// </summary>
	public string Email
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
	public string Name
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public string Phone
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public string State
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
	public string Updated
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public string ZipCode
    {
        get;
        set;
    }

    #endregion
}