using SuperSocket.ProtoBase;
using NHapi.Base.Parser;
using NHapiTools.Base.Validation;
using NHapi.Base.Model;
using Microsoft.Extensions.Configuration;

namespace HL7Fuse.Protocol {

    public class HL7RequestInfoParser //: IRequestInfoParser<HL7RequestInfo>
    {
        private readonly IConfiguration configuration;

        #region Private Properties
        private bool HandleEachMessageAsEvent
        {
            get {
                bool result = false;
                if (!bool.TryParse(configuration["HandleEachMessageAsEvent"], out result))
                {
                    result = false;
                }
                return result;
            }
        }

        #endregion

        public HL7RequestInfoParser(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public HL7RequestInfo ParseRequestInfo(string message)
        {
            return ParseRequestInfo(message, string.Empty);
        }

        public HL7RequestInfo ParseRequestInfo(string message, string protocol)
        {
            HL7RequestInfo result = new HL7RequestInfo();
            PipeParser parser = new PipeParser();

            try
            {
                ConfigurableContext configContext = new ConfigurableContext(parser.ValidationContext);
                parser.ValidationContext = configContext;
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            IMessage hl7Message = parser.Parse(message);

            result = new HL7RequestInfo();
            if (HandleEachMessageAsEvent)
                result.Key = "V" + hl7Message.Version.Replace(".", "") + "." + hl7Message.GetStructureName();
            else
                result.Key = "V" + hl7Message.Version.Replace(".", "") + ".MessageFactory";

            if (!string.IsNullOrEmpty(protocol))
                result.Key += protocol;

            result.Message = hl7Message;

            // Parse the message
            return result;
        }
    }
}