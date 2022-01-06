#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           StorageCompression.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:11
// *****************************************/

#endregion

namespace ProfSvc_WebAPI.Code;

public class StorageCompression
{
    #region Constructors

    public StorageCompression(ProtectedSessionStorage storage) => LocalStorage = storage;

    #endregion

    #region Properties

    private ProtectedSessionStorage LocalStorage
    {
        get;
    }

    #endregion

    #region Methods

    public bool Delete(string cookieName)
    {
        try
        {
            LocalStorage?.DeleteAsync(cookieName);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<Cooky> Get(string cookieName)
    {
        try
        {
            ProtectedBrowserStorageResult<Cooky> _resultValue = await LocalStorage.GetAsync<Cooky>(cookieName);
            Cooky _cooky = _resultValue.Value ?? new Cooky();

            return _cooky;
        }
        catch
        {
            return new();
        }
    }

    public async Task<int> Set(string cookieName, Cooky cookieValue)
    {
        try
        {
            await LocalStorage.SetAsync(cookieName, cookieValue);
        }
        catch (Exception)
        {
            return 0;
        }

        return 0;
    }

    #endregion
}