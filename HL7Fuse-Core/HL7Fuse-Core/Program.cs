using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.ServiceProcess;
using System.Net.Sockets;
using System.Net;
using SuperSocket;
using SuperSocket.ProtoBase;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using HL7Fuse.Protocol;
using NHapi.Base.Parser;
using NHapiTools.Base.Util;

namespace HL7Fuse
{
    internal static class Program
    {

        static Program()
        { }

        public static async Task Main(string[] args)
        {
            PipeParser parser = new PipeParser();
        
            var host = SuperSocketHostBuilder.Create<HL7RequestInfo, MLLPBeginEndMarkReceiveFilter>()
                .UsePackageHandler(async (s, p) =>
                {
                    string responseMessage = "\v" + parser.Encode(new Ack("Test Server","Development").MakeACK(p.ResponseMessage)) + "\u001c\r\n";
                    await s.SendAsync(System.Text.Encoding.UTF8.GetBytes(responseMessage));
                }).ConfigureLogging((hostCtx, loggingBuilder) =>
                {
                    loggingBuilder.AddConsole();
                })
                .Build();

            await host.RunAsync();
        }
    }
}