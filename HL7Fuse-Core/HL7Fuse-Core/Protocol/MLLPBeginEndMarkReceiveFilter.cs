using System;
using System.Buffers;
using System.Text;
using SuperSocket.ProtoBase;
using NHapiTools.Base.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Binder;
using Microsoft.Extensions.Configuration.Json;


namespace HL7Fuse.Protocol
{
    public class MLLPBeginEndMarkReceiveFilter : BeginEndMarkPipelineFilter<HL7RequestInfo>
    {
        private readonly IConfiguration configuration;
        private readonly static byte[] BeginMark = new byte[] { 11 }; //HEX 0B
        private readonly static byte[] EndMark = new byte[] { 28, 13 }; //HEX 1C, 0D

        public MLLPBeginEndMarkReceiveFilter() : base(BeginMark, EndMark)
        {
            this.configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        protected override HL7RequestInfo DecodePackage(ref ReadOnlySequence<byte> buffer)
        {
            byte[] msg = new byte[buffer.Length];
            Array.Copy(buffer.ToArray<byte>(), msg, buffer.Length);
            string result = System.Text.UTF8Encoding.UTF8.GetString(msg);

            if (result.Length > 3)
            {
                StringBuilder sb = new StringBuilder(result);
                //MLLP.StripMLLPContainer(sb);

                result = sb.ToString();
            }
            // remove empty space at the end of the message
            result = result.TrimEnd(new char[]{' ', '\r', '\n'});
            HL7RequestInfoParser parser = new HL7RequestInfoParser(this.configuration);
            
            var response = parser.ParseRequestInfo(result, "MLLP");
            response.ResponseMessage = response.Message;

            return response;

            //return base.DecodePackage(ref buffer);
        }
    }
}