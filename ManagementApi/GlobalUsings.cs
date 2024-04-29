global using Microsoft.OpenApi.Models;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Mvc.Authorization;
global using Microsoft.IdentityModel.Tokens;

global using OneOf;
global using Serilog;
global using FluentValidation;

global using Shared;
global using Auth.Helpers;
global using Shared.Filters;
global using Infrastructure;
global using Infrastructure.Entities;

global using ManagementApi.Repo.Users;
global using ManagementApi.Repo.Roles;
global using ManagementApi.Repo.Roles.Models;
global using ManagementApi.Repo.Users.Models;

global using System.Text;
global using Auth.Models;
global using Auth.Authorization;
global using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
