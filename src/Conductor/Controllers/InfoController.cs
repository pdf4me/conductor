using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Conductor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Conductor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class InfoController : ControllerBase
    {
        private IConfiguration configuration;
        private IWebHostEnvironment env;

        public InfoController(IConfiguration configuration, IWebHostEnvironment env)
        {
            this.configuration = configuration;
            this.env = env;
        }
        [HttpGet]        
        public ActionResult<DiagnosticInfo> Get()
        {
            var process = Process.GetCurrentProcess();
            var entryAsm = Assembly.GetEntryAssembly();
            var version = FileVersionInfo.GetVersionInfo(entryAsm.Location);
            return new DiagnosticInfo()
            {
                MachineName = Environment.MachineName,
                StartTime = process.StartTime,
                WorkingSet = process.WorkingSet64,
                Version = version.ProductVersion,
                OSVersion = Environment.OSVersion.VersionString,
                Enviornment = $" EnvironmentName {env.EnvironmentName} ASPNETCORE_ENVIRONMENT {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}" , //Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                BuildId =  Environment.GetEnvironmentVariable("Build_Id")

            };
        }
    }
}
