#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           AdminListDefault.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-30-2021 15:18
// Last Updated On:     01-04-2022 16:12
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Data;

public class AdminListDefault
{
    #region Constructors

	/// <summary>
	///     Initializes the AdminListDefault class.
	/// </summary>
	/// <param name="type">String to be displayed in case of error.</param>
	/// <param name="methodName">Name of the Stored Procedure to execute.</param>
	/// <param name="isAdd">Is the Record Add or Edit?</param>
	/// <param name="isString">Is the Primary key String or Integer.</param>
	/// <param name="clientFactory">Client Factory Service to access the Web API.</param>
	public AdminListDefault(string type, string methodName, bool isAdd, bool isString, IHttpClientFactory clientFactory)
    {
        Type = type;
        MethodName = methodName;
        IsAdd = isAdd;
        IsString = isString;
        ClientFactory = clientFactory;
    }

    #endregion

    #region Properties

	/// <summary>
	///     Is the Record Add or Edit?
	/// </summary>
	public static bool IsAdd
    {
        get;
        set;
    }

	/// <summary>
	///     Is the Primary key String or Integer.
	/// </summary>
	public static bool IsString
    {
        get;
        set;
    }

	/// <summary>
	///     Client Factory Service to access the Web API.
	/// </summary>
	public static IHttpClientFactory ClientFactory
    {
        get;
        set;
    }

	/// <summary>
	///     Name of the Stored Procedure to execute.
	/// </summary>
	public static string MethodName
    {
        get;
        set;
    }

	/// <summary>
	///     String to be displayed in case of error.
	/// </summary>
	public static string Type
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
	public AdminListDefault Copy() => (AdminListDefault)MemberwiseClone();

	/// <summary>
	///     Clears this object data.
	/// </summary>
	public void ClearData()
    {
        Type = "";
        MethodName = "";
        IsAdd = false;
        IsString = false;
        ClientFactory = null;
    }

    #endregion
}