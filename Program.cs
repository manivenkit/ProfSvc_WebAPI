#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_WebAPI
// File Name:           Program.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 21:38
// Last Updated On:     01-04-2022 16:12
// *****************************************/

#endregion

#region Using

using Syncfusion.Blazor;
using Syncfusion.Licensing;

#endregion

WebApplicationBuilder _builder = WebApplication.CreateBuilder(args);
_builder.Services.AddSyncfusionBlazor();

// Add services to the container.
//_builder.Services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

_builder.Services.AddControllers();

WebApplication _app = _builder.Build();
SyncfusionLicenseProvider.RegisterLicense("NjY5MjIyQDMyMzAyZTMyMmUzMGQwUmxjaWZkYmlZS05HL2QreE9ub1pGU1VJYjN6a0ZRekt4WUdEMkFFcFU9");

// Configure the HTTP request pipeline.

_app.UseHttpsRedirection();
//_app.UseMvcWithDefaultRoute();
_app.UseAuthorization();

_app.MapControllers();

_app.Run();