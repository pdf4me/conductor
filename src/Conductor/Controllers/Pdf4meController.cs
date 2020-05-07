using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Conductor.Auth;
using Conductor.Domain.Interfaces;
using Conductor.Domain.Models;
using Conductor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using MongoDB.Bson.IO;
using Newtonsoft.Json.Linq;
using Pdf4meWf;
using WorkflowCore.Interface;
using NewtonJsonConvert = Newtonsoft.Json.JsonConvert;

namespace Conductor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Pdf4meController : ControllerBase
    {
        private readonly IWorkflowController _workflowController;
        private readonly IPersistenceProvider _persistenceProvider;
        private readonly IMapper _mapper;

        public Pdf4meController(IWorkflowController workflowController, IPersistenceProvider persistenceProvider, IMapper mapper)
        {
            _workflowController = workflowController;
            _persistenceProvider = persistenceProvider;
            _mapper = mapper;
        }


        [HttpPost("{id}")]
        //[Authorize(Policy = Policies.Controller)]
        public async Task<ActionResult<WorkflowInstance>> StartWorkflow(string id)
        {
            string contentReq = null;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                contentReq = await reader.ReadToEndAsync();
            }

            object workflowData = contentReq;

            if (id.Equals(CompressJobWorkflow.WorkflowId))
            {
                workflowData = NewtonJsonConvert.DeserializeObject<CompressJobData>(contentReq);
            }

            var instanceId = await _workflowController.StartWorkflow(id, workflowData);
            var result = await _persistenceProvider.GetWorkflowInstance(instanceId);

            return Created(instanceId, _mapper.Map<WorkflowInstance>(result));
        }


    }
}