using Newtonsoft.Json;
using Pdf4me.Common.AiLogging;
using Pdf4me.DataContract;
using Pdf4me.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCore.Interface;

namespace Conductor.Pdf4me
{
    public class Pdf4meMessageHandler
    {

        private static IWorkflowHost _wfHost = null;

        public Pdf4meMessageHandler(IWorkflowHost wfHost)
        {
            _wfHost = wfHost;
        }


        public async Task Pdf4meActionHandlerAsync(ProcessingMessage message, CancellationToken token)
        {
            var logTrace = new LogTrace();

            try
            {                
                AiLogger.LogInfo(logTrace, "WfMessage to execute: " + JsonConvert.SerializeObject(message));

                var msgId = message.MessageId;

                var docPluginRes = JsonConvert.DeserializeObject<DocPluginRes>(message.MessageBody);

                await _wfHost.PublishEvent(docPluginRes.WfEventName, docPluginRes.WfEventKey, message.MessageBody);

                AiLogger.LogInfo(logTrace, "WfMessage executed!");
            }catch(Exception e)
            {
                AiLogger.LogException(logTrace, e);
            }

        }


    }
}
