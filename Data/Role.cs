#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           Role.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:12
// *****************************************/

#endregion

namespace ProfSvc_WebAPI.Data;

/// <summary>
///     Class to hold the List of roles.
/// </summary>
public class Role
{
    #region Constructors

	/// <summary>
	///     Initializes the Roles class.
	/// </summary>
	public Role()
    {
        ID = "";
        RoleName = "";
        ViewCandidate = false;
        ViewRequisition = false;
        EditCandidate = false;
        EditRequisition = false;
        ChangeCandidateStatus = false;
        ChangeRequisitionStatus = false;
        SendEmailCandidate = false;
        ForwardResume = false;
        DownloadResume = false;
        SubmitCandidate = false;
        ViewClients = false;
        EditClients = false;
        Description = "";
    }

	/// <summary>
	///     Initializes the Roles class.
	/// </summary>
	/// <param name="id">ID of the Role.</param>
	/// <param name="roleName">Name of the Role.</param>
	/// <param name="viewCandidate">Does Role have the rights to view the Candidate information?</param>
	/// <param name="viewRequisition">Does Role have the rights to view the Requisition information?</param>
	/// <param name="editCandidate">Does Role have the rights to edit the Candidate information?</param>
	/// <param name="editRequisition">Does Role have the rights to edit the Requisition information?</param>
	/// <param name="changeCandStatus">Does Role have the rights to change the Candidate Status?</param>
	/// <param name="changeReqStatus">Does Role have the rights to change the Requisition Status?</param>
	/// <param name="sendEmail">Does Role have the rights to send e-mail?</param>
	/// <param name="forwardResume">Does Role have the rights to forward resumes?</param>
	/// <param name="downloadResume">Does Role have the rights to download Candidate resumes?</param>
	/// <param name="submitCandidate">Does Role have the rights to submit a Candidate for Requisition?</param>
	/// <param name="viewClients">Does Role have the rights to view the Client information?</param>
	/// <param name="editClients">Does Role have the rights to edit the Client information?</param>
	/// <param name="description">Description of the Role.</param>
	public Role(string id, string roleName, bool viewCandidate, bool viewRequisition, bool editCandidate, bool editRequisition, bool changeCandStatus,
				bool changeReqStatus,
				bool sendEmail, bool forwardResume, bool downloadResume, bool submitCandidate, bool viewClients, bool editClients, string description = "")
    {
        ID = id;
        RoleName = roleName;
        ViewCandidate = viewCandidate;
        ViewRequisition = viewRequisition;
        EditCandidate = editCandidate;
        EditRequisition = editRequisition;
        ChangeCandidateStatus = changeCandStatus;
        ChangeRequisitionStatus = changeReqStatus;
        SendEmailCandidate = sendEmail;
        ForwardResume = forwardResume;
        DownloadResume = downloadResume;
        SubmitCandidate = submitCandidate;
        ViewClients = viewClients;
        EditClients = editClients;
        Description = description;
    }

    #endregion

    #region Properties

	/// <summary>
	///     Does Role have the rights to change the Candidate Status?
	/// </summary>
	public bool ChangeCandidateStatus
    {
        get;
        set;
    }

	/// <summary>
	///     Does Role have the rights to change the Requisition Status?
	/// </summary>
	public bool ChangeRequisitionStatus
    {
        get;
        set;
    }

	/// <summary>
	///     Does Role have the rights to download Candidate resumes?
	/// </summary>
	public bool DownloadResume
    {
        get;
        set;
    }

	/// <summary>
	///     Does Role have the rights to edit the Candidate information?
	/// </summary>
	public bool EditCandidate
    {
        get;
        set;
    }

	/// <summary>
	///     Does Role have the rights to edit the Client information?
	/// </summary>
	public bool EditClients
    {
        get;
        set;
    }

	/// <summary>
	///     Does Role have the rights to edit the Requisition information?
	/// </summary>
	public bool EditRequisition
    {
        get;
        set;
    }

	/// <summary>
	///     Does Role have the rights to forward resumes?
	/// </summary>
	public bool ForwardResume
    {
        get;
        set;
    }

	/// <summary>
	///     Does Role have the rights to send e-mail?
	/// </summary>
	public bool SendEmailCandidate
    {
        get;
        set;
    }

	/// <summary>
	///     Does Role have the rights to submit a Candidate for Requisition?
	/// </summary>
	public bool SubmitCandidate
    {
        get;
        set;
    }

	/// <summary>
	///     Does Role have the rights to view the Candidate information?
	/// </summary>
	public bool ViewCandidate
    {
        get;
        set;
    }

	/// <summary>
	///     Does Role have the rights to view the Client information?
	/// </summary>
	public bool ViewClients
    {
        get;
        set;
    }

	/// <summary>
	///     Does Role have the rights to view the Requisition information?
	/// </summary>
	public bool ViewRequisition
    {
        get;
        set;
    }

	/// <summary>
	///     Description of the Role.
	/// </summary>
	public string Description
    {
        get;
        set;
    }

	/// <summary>
	///     ID of the Role.
	/// </summary>
	public string ID
    {
        get;
        set;
    }

	/// <summary>
	///     Name of the Role.
	/// </summary>
	public string RoleName
    {
        get;
        set;
    }

    #endregion
}