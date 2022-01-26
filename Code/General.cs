#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           General.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:11
// *****************************************/

#endregion

namespace ProfSvc_WebAPI.Code;

public static class General
{
    #region Properties

    public static byte[] Md5PasswordHash(string inputText) => MD5.Create().ComputeHash(new UTF8Encoding().GetBytes(inputText));

    public static Color FromHex(string hex)
    {
        if (hex.StartsWith("#"))
        {
            hex = hex[1..];
        }

        if (hex.Length != 6)
        {
            return Color.Empty;
        }

        Color _color = Color.Empty;

        try
        {
            int _red = int.Parse(hex[..2], NumberStyles.HexNumber);
            int _green = int.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
            int _blue = int.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);

            return Color.FromArgb(_red, _green, _blue);
        }
        catch
        {
            return _color;
        }
    }

    public static string Decrypt(string str, bool query = false)
    {
        if (str.NullOrWhiteSpace())
        {
            return "";
        }

        GenerateBytes(out byte[] _byteKey, out byte[] _vectorByte, query);
        using TripleDes _enc = new(_byteKey, _vectorByte);

        return _enc.Decrypt(str).Trim();
    }

    public static string Encrypt(object str, bool query = false) => str == null ? "" : Encrypt(str.ToString(), query);

    private static string Encrypt(string str, bool query = false)
    {
        if (str.NullOrWhiteSpace())
        {
            return "";
        }

        GenerateBytes(out byte[] _byteKey, out byte[] _vectorByte, query);
        using TripleDes _enc = new(_byteKey, _vectorByte);

        return _enc.Encrypt(str);
    }

    private static void GenerateBytes(out byte[] key, out byte[] vector, bool query = false)
    {
        if (!query)
        {
            key = new byte[]
                  {
                      240, 24, 133, 174, 0, 155, 238, 145, 244, 93, 112, 139, 139, 65, 57, 242, 167, 135, 16, 221, 254, 128, 190, 228
                  };

            vector = new byte[]
                     {
                         121, 42, 68, 241, 103, 89, 5, 192
                     };
        }
        else
        {
            key = new byte[]
                  {
                      184, 133, 201, 79, 6, 193, 193, 255, 0, 173, 62, 48, 30, 27, 16, 15
                  };

            vector = new byte[]
                     {
                         121, 111, 149, 157, 168, 222, 239, 241
                     };
        }
    }

    #endregion
}