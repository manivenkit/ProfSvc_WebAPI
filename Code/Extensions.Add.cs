#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           Extensions.Add.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:11
// *****************************************/

#endregion

namespace ProfSvc_WebAPI.Code;

public static partial class Extensions
{
    #region Properties

    /// <summary>
    ///     Generates a SQLParameter of type Int.
    /// </summary>
    /// <param name="t"> SqlParameterCollection </param>
    /// <param name="name"> The name of the parameter. </param>
    /// <param name="value"> The value to be assigned to the parameter. </param>
    /// <param name="output"> Should the Parameter Direction b InputOutput or Input. </param>
    /// <returns> SQLParameter of type Int </returns>
    public static SqlParameter Int(this SqlCommand t, string name, object value, bool output = false) => t.Parameters.Add(new(name, SqlDbType.Int)
                                                                                                                          {
                                                                                                                              Value = value,
                                                                                                                              Direction =
                                                                                                                                  output ? ParameterDirection
                                                                                                                                         .InputOutput
                                                                                                                                      : ParameterDirection.Input
                                                                                                                          });

    /// <summary>
    ///     Generates a SQLParameter of type Xml.
    /// </summary>
    /// <param name="t"> SqlParameterCollection </param>
    /// <param name="name"> The name of the parameter. </param>
    /// <param name="value"> The value to be assigned to the parameter. </param>
    /// <returns> SQLParameter of type Xml </returns>
    public static SqlParameter Xml(this SqlCommand t, string name, object value) => t.Parameters.Add(new(name, SqlDbType.Xml)
                                                                                                     {
                                                                                                         Value = value
                                                                                                     });

    /// <summary>
    ///     Generates a SQLParameter of type Binary.
    /// </summary>
    /// <param name="t"> SqlParameterCollection </param>
    /// <param name="name"> The name of the parameter. </param>
    /// <param name="size"> Size in bytes (length) of the parameter. </param>
    /// <param name="value"> The value to be assigned to the parameter. </param>
    /// <param name="output"> Should the Parameter Direction b InputOutput or Input. </param>
    /// <returns> SQLParameter of type Binary </returns>
    public static void Binary(this SqlCommand t, string name, int size, object value, bool output = false) => t.Parameters.Add(new(name, SqlDbType.Binary, size)
                                                                                                                               {
                                                                                                                                   Value = value,
                                                                                                                                   Direction =
                                                                                                                                       output
                                                                                                                                           ? ParameterDirection
                                                                                                                                              .InputOutput
                                                                                                                                           : ParameterDirection
                                                                                                                                              .Input
                                                                                                                               });

    /// <summary>
    ///     Generates a SQLParameter of type Bit.
    /// </summary>
    /// <param name="t"> SqlParameterCollection </param>
    /// <param name="name"> The name of the parameter. </param>
    /// <param name="value"> The value to be assigned to the parameter. </param>
    /// <param name="output"> Should the Parameter Direction b InputOutput or Input. </param>
    /// <returns> SQLParameter of type Bit </returns>
    public static void Bit(this SqlCommand t, string name, object value, bool output = false) => t.Parameters.Add(new(name, SqlDbType.Bit)
                                                                                                                  {
                                                                                                                      Value = value,
                                                                                                                      Direction =
                                                                                                                          output ? ParameterDirection
                                                                                                                             .InputOutput : ParameterDirection
                                                                                                                             .Input
                                                                                                                  });

    /// <summary>
    ///     Generates a SQLParameter of type Char.
    /// </summary>
    /// <param name="t"> SqlParameterCollection </param>
    /// <param name="name"> The name of the parameter. </param>
    /// <param name="size"> Size in bytes (length) of the parameter. </param>
    /// <param name="value"> The value to be assigned to the parameter. </param>
    /// <param name="isNType"> Whether the parameter should be nvarchar or varchar. </param>
    /// <param name="output"> Should the Parameter Direction b InputOutput or Input. </param>
    /// <returns> SQLParameter of type Char </returns>
    public static void Char(this SqlCommand t, string name, int size, object value, bool isNType = true,
                            bool output = false) => t.Parameters.Add(new(name, isNType ? SqlDbType.NChar : SqlDbType.Char, size)
                                                                     {
                                                                         Value = value,
                                                                         Direction = output ? ParameterDirection.InputOutput : ParameterDirection.Input
                                                                     });

    /// <summary>
    ///     Generates a SQLParameter of type Date.
    /// </summary>
    /// <param name="t"> SqlParameterCollection </param>
    /// <param name="name"> The name of the parameter. </param>
    /// <param name="value"> The value to be assigned to the parameter. </param>
    /// <param name="output"> Should the Parameter Direction be InputOutput or Input. </param>
    /// <returns> SQLParameter of type Date </returns>
    public static void Date(this SqlCommand t, string name, object value, bool output = false) => t.Parameters.Add(new(name, SqlDbType.Date)
                                                                                                                   {
                                                                                                                       Value = value,
                                                                                                                       Direction =
                                                                                                                           output ? ParameterDirection
                                                                                                                              .InputOutput : ParameterDirection
                                                                                                                              .Input
                                                                                                                   });

    /// <summary>
    ///     Generates a SQLParameter of type DateTime.
    /// </summary>
    /// <param name="t"> SqlParameterCollection </param>
    /// <param name="name"> The name of the parameter. </param>
    /// <param name="value"> The value to be assigned to the parameter. </param>
    /// <param name="output"> Should the Parameter Direction b InputOutput or Input. </param>
    /// <returns> SQLParameter of type DateTime </returns>
    public static void DateTime(this SqlCommand t, string name, object value, bool output = false) => t.Parameters.Add(new(name, SqlDbType.DateTime)
                                                                                                                       {
                                                                                                                           Value = value,
                                                                                                                           Direction =
                                                                                                                               output ? ParameterDirection
                                                                                                                                      .InputOutput
                                                                                                                                   : ParameterDirection.Input
                                                                                                                       });

    /// <summary>
    ///     Generates a SQLParameter of type Decimal.
    /// </summary>
    /// <param name="t"> SqlParameterCollection </param>
    /// <param name="name"> The name of the parameter. </param>
    /// <param name="precision"> Gets or sets the number of digits used to represent the Value property. </param>
    /// <param name="scale"> Gets or sets the number of decimal places to Value property is resolved. </param>
    /// <param name="value"> The value to be assigned to the parameter. </param>
    /// <param name="output"> Should the Parameter Direction b InputOutput or Input. </param>
    /// <returns> SQLParameter of type DateTime </returns>
    public static void Decimal(this SqlCommand t, string name, byte precision, byte scale, object value, bool output = false) =>
        t.Parameters.Add(new(name, SqlDbType.Decimal)
                         {
                             Value = value, Direction = output ? ParameterDirection.InputOutput : ParameterDirection.Input, Precision = precision,
                             Scale = scale
                         });

    /// <summary>
    ///     Generates a SQLParameter of type SmallDateTime.
    /// </summary>
    /// <param name="t"> SqlParameterCollection </param>
    /// <param name="name"> The name of the parameter. </param>
    /// <param name="value"> The value to be assigned to the parameter. </param>
    /// <param name="output"> Should the Parameter Direction b InputOutput or Input. </param>
    /// <returns> SQLParameter of type SmallDateTime </returns>
    public static void SmallDateTime(this SqlCommand t, string name, object value, bool output = false) => t.Parameters.Add(new(name, SqlDbType.SmallDateTime)
                                                                                                                            {
                                                                                                                                Value = value,
                                                                                                                                Direction =
                                                                                                                                    output ? ParameterDirection
                                                                                                                                           .InputOutput
                                                                                                                                        : ParameterDirection
                                                                                                                                           .Input
                                                                                                                            });

    /// <summary>
    ///     Generates a SQLParameter of type SmallInt.
    /// </summary>
    /// <param name="t"> SqlParameterCollection </param>
    /// <param name="name"> The name of the parameter. </param>
    /// <param name="value"> The value to be assigned to the parameter. </param>
    /// <param name="output"> Should the Parameter Direction b InputOutput or Input. </param>
    /// <returns> SQLParameter of type SmallInt </returns>
    public static void SmallInt(this SqlCommand t, string name, object value, bool output = false) => t.Parameters.Add(new(name, SqlDbType.SmallInt)
                                                                                                                       {
                                                                                                                           Value = value,
                                                                                                                           Direction =
                                                                                                                               output ? ParameterDirection
                                                                                                                                      .InputOutput
                                                                                                                                   : ParameterDirection.Input
                                                                                                                       });

    /// <summary>
    ///     Generates a SQLParameter of type Tinyint.
    /// </summary>
    /// <param name="t"> SqlParameterCollection </param>
    /// <param name="name"> The name of the parameter. </param>
    /// <param name="value"> The value to be assigned to the parameter. </param>
    /// <param name="output"> Should the Parameter Direction b InputOutput or Input. </param>
    /// <returns> SQLParameter of type Tinyint </returns>
    public static void TinyInt(this SqlCommand t, string name, object value, bool output = false) => t.Parameters.Add(new(name, SqlDbType.TinyInt)
                                                                                                                      {
                                                                                                                          Value = value,
                                                                                                                          Direction =
                                                                                                                              output ? ParameterDirection
                                                                                                                                     .InputOutput
                                                                                                                                  : ParameterDirection.Input
                                                                                                                      });

    /// <summary>
    ///     Generates a SQLParameter of type UniqueIdentifier.
    /// </summary>
    /// <param name="t"> SqlParameterCollection </param>
    /// <param name="name"> The name of the parameter. </param>
    /// <param name="value"> The value to be assigned to the parameter. </param>
    /// <param name="output"> Should the Parameter Direction b InputOutput or Input. </param>
    /// <returns> SQLParameter of type UniqueIdentifier </returns>
    public static void UniqueIdentifier(this SqlCommand t, string name, object value, bool output = false) =>
        t.Parameters.Add(new(name, SqlDbType.UniqueIdentifier)
                         {
                             Value = value, Direction = output ? ParameterDirection.InputOutput : ParameterDirection.Input
                         });

    /// <summary>
    ///     Generates a SQLParameter of type Varchar.
    /// </summary>
    /// <param name="t"> SqlParameterCollection </param>
    /// <param name="name"> The name of the parameter. </param>
    /// <param name="size"> Size in bytes (length) of the parameter. Enter -1 for MAX parameter </param>
    /// <param name="value"> The value to be assigned to the parameter. </param>
    /// <param name="isNType"> Whether the parameter should be nvarchar or varchar. </param>
    /// <param name="output"> Should the Parameter Direction b InputOutput or Input. </param>
    /// <returns> SQLParameter of type Varchar </returns>
    public static void Varchar(this SqlCommand t, string name, int size, object value, bool isNType = true, bool output = false) =>
        t.Parameters.Add(new(name, isNType ? SqlDbType.NVarChar : SqlDbType.VarChar, size)
                         {
                             Value = value, Direction = output ? ParameterDirection.InputOutput : ParameterDirection.Input
                         });

    /// <summary>
    ///     Generates a SQLParameter of type Varchar.
    /// </summary>
    /// <param name="t"> SqlParameterCollection </param>
    /// <param name="name"> The name of the parameter. </param>
    /// <param name="size"> Size in bytes (length) of the parameter. Enter -1 for MAX parameter </param>
    /// <param name="isNType"> Whether the parameter should be nvarchar or varchar. </param>
    /// <param name="output"> Should the Parameter Direction b InputOutput or Input. </param>
    /// <returns> SQLParameter of type Varchar </returns>
    public static void VarcharD(this SqlCommand t, string name, int size, bool isNType = true, bool output = false) =>
        t.Parameters.Add(new(name, isNType ? SqlDbType.NVarChar : SqlDbType.VarChar, size)
                         {
                             Value = DBNull.Value, Direction = output ? ParameterDirection.InputOutput : ParameterDirection.Input
                         });

    #endregion
}