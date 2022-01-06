#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           JobOptionsCandidate.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:12
// *****************************************/

#endregion

namespace ProfSvc_WebAPI.Data;

public class JobOptionsCandidate
{
    #region Constructors

	/// <summary>
	///     Initializes the Job Options.
	/// </summary>
	/// <param name="code">Code for the Job Option.</param>
	/// <param name="jobOptions">Name of the Job Option.</param>
	/// <param name="description">Description for the Job Options.</param>
	/// <param name="updatedDate">Last Updated Date.</param>
	public JobOptionsCandidate(string code, string jobOptions, string description = "", string updatedDate = "")
    {
        Code = code;
        JobOptions = jobOptions;
        Description = description;
        UpdatedDate = updatedDate;
    }

    #endregion

    #region Properties

	/// <summary>
	///     Code for the Job Option.
	/// </summary>
	public string Code
    {
        get;
    }

	/// <summary>
	///     Description for the Job Options.
	/// </summary>
	public string Description
    {
        get;
        set;
    }

	/// <summary>
	///     Name of the Job Option.
	/// </summary>
	public string JobOptions
    {
        get;
    }

	/// <summary>
	///     Last Updated Date.
	/// </summary>
	public string UpdatedDate
    {
        get;
        set;
    }

    #endregion
}