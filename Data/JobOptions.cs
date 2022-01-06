#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           JobOptions.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:12
// *****************************************/

#endregion

namespace ProfSvc_WebAPI.Data;

/// <summary>
///     Class for storing the Candidate / Requisition Job Option.
/// </summary>
public class JobOption
{
    #region Constructors

	/// <summary>
	///     Initializes the Job Options.
	/// </summary>
	public JobOption()
    {
        Code = "";
        Option = "";
        Description = "";
        UpdatedDate = "";
        Duration = false;
        Rate = false;
        Sal = false;
        Tax = "";
        Exp = false;
        PlaceFee = false;
        Benefits = false;
        ShowHours = false;
        RateText = "";
        PercentText = "";
        CostPercent = 0M;
        ShowPercent = false;
        ConnectionString = "";
        IsAdd = false;
    }

	/// <summary>
	///     Initializes the Job Options.
	/// </summary>
	/// <param name="code">Code for the Job Option.</param>
	/// <param name="option">Name of the Job Option.</param>
	/// <param name="description">Description for the Job Options.</param>
	/// <param name="updatedDate">Date when this Option was Last Updated.</param>
	/// <param name="rate">Is Rate required?</param>
	/// <param name="sal">Is Salary Range required?</param>
	/// <param name="tax">Tax Terms for the Job Option.</param>
	/// <param name="duration">Is Duration for the Job Option required?</param>
	/// <param name="exp">Is Experience required?</param>
	/// <param name="placeFee">Is Placement Fee required?</param>
	/// <param name="benefits">Is Benefits required?</param>
	/// <param name="rateText">Rate Description for Rate.</param>
	/// <param name="percentText">Percent Description for the Tax Terms.</param>
	/// <param name="showHours">Should the hours be shown?</param>
	/// <param name="showPercent">Should the percent be shown?</param>
	/// <param name="costPercent">Cost in Percent.</param>
	public JobOption(string code, string option, string description = "", string updatedDate = "", bool duration = false, bool rate = false,
					 bool sal = false, string tax = "", bool exp = false, bool placeFee = false, bool benefits = false, bool showHours = false,
					 string rateText = "", string percentText = "", decimal costPercent = 0, bool showPercent = false)
    {
        Code = code;
        Option = option;
        Description = description;
        UpdatedDate = updatedDate;
        Duration = duration;
        Rate = rate;
        Sal = sal;
        Tax = tax;
        Exp = exp;
        PlaceFee = placeFee;
        Benefits = benefits;
        ShowHours = showHours;
        RateText = rateText;
        PercentText = percentText;
        CostPercent = costPercent;
        ShowPercent = showPercent;
    }

    #endregion

    #region Properties

	/// <summary>
	///     Is Benefits required?
	/// </summary>
	public bool Benefits
    {
        get;
        set;
    }

	/// <summary>
	///     Is Duration for the Job Option required?
	/// </summary>
	public bool Duration
    {
        get;
        set;
    }

	/// <summary>
	///     Is Experience required?
	/// </summary>
	public bool Exp
    {
        get;
        set;
    }

    public bool IsAdd
    {
        get;
        set;
    }

	/// <summary>
	///     Is Placement Fee required?
	/// </summary>
	public bool PlaceFee
    {
        get;
        set;
    }

	/// <summary>
	///     Is Rate required?
	/// </summary>
	public bool Rate
    {
        get;
        set;
    }

	/// <summary>
	///     Is Salary Range required?
	/// </summary>
	public bool Sal
    {
        get;
        set;
    }

	/// <summary>
	///     Should the hours be shown?
	/// </summary>
	public bool ShowHours
    {
        get;
        set;
    }

	/// <summary>
	///     Should the percent be shown?
	/// </summary>
	public bool ShowPercent
    {
        get;
        set;
    }

	/// <summary>
	///     Cost in Percent.
	/// </summary>
	public decimal CostPercent
    {
        get;
        set;
    }

	/// <summary>
	///     Code for the Job Option.
	/// </summary>
	[Required(ErrorMessage = "Job Options Code is required.")]
    public string Code
    {
        get;
        set;
    }

    public string ConnectionString
    {
        get;
        set;
    }

	/// <summary>
	///     Description for Rate.
	/// </summary>
	[StringLength(500, MinimumLength = 0, ErrorMessage = "Job Options Description should not be more than 500 characters long.")]
    public string Description
    {
        get;
        set;
    }

	/// <summary>
	///     Name of the Job Option.
	/// </summary>
	[Required(ErrorMessage = "Job Option is required."),
	 StringLength(50, MinimumLength = 2, ErrorMessage = "Job Options must be between 2 and 50 characters.")]
    public string Option
    {
        get;
        set;
    }

	/// <summary>
	///     Percent Description for the Tax Terms.
	/// </summary>
	[Required(ErrorMessage = "Percent Text is required."),
	 StringLength(255, MinimumLength = 2, ErrorMessage = "Percent Text must be between 2 and 255 characters.")]
    public string PercentText
    {
        get;
        set;
    }

	/// <summary>
	///     Rate Description for the Job Options.
	/// </summary>
	[Required(ErrorMessage = "Rate Text is required."),
	 StringLength(255, MinimumLength = 2, ErrorMessage = "Rate Text must be between 2 and 255 characters.")]
    public string RateText
    {
        get;
        set;
    }

	/// <summary>
	///     Tax Terms for the Job Option.
	/// </summary>
	[StringLength(20, ErrorMessage = "Tax Terms should be not more than 20 characters long.")]
    public string Tax
    {
        get;
        set;
    }

    public string UpdatedDate
    {
        get;
        set;
    }

    #endregion
}