#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           Login.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:11
// *****************************************/

#endregion

namespace ProfSvc_WebAPI.Code;

public class Login
{
    #region Properties

    public bool Checked
    {
        get;
        set;
    } = true;

    [Required(ErrorMessage = "Password is required."),
     StringLength(16, MinimumLength = 3, ErrorMessage = "Password should be between 3 and 16 characters long.")]
    public string Password
    {
        get;
        set;
    }

    [Required(ErrorMessage = "User Name is required."),
     StringLength(10, MinimumLength = 3, ErrorMessage = "User Name should be between 3 and 10 characters long.")]
    public string UserName
    {
        get;
        set;
    }

    #endregion
}