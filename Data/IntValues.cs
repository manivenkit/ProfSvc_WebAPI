#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           IntValues.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:12
// *****************************************/

#endregion

namespace ProfSvc_WebAPI.Data;

public class IntValues
{
    #region Constructors

    public IntValues()
    {
    }

    public IntValues(int key, string value)
    {
        Key = key;
        Value = value;
    }

    #endregion

    #region Properties

    public int Key
    {
        get;
        set;
    }

    public string Value
    {
        get;
        set;
    }

    #endregion
}