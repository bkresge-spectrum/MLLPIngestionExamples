using System;
using System.Collections.Generic;
using System.Linq;
using NHapi.Base.Model;

namespace HL7Fuse.Protocol
{
    public class HL7RequestInfo
    {
        public string Key { get; set; }

        public IMessage Message { get; set; }

        public string ErrorMessage { get; set; }

        public bool WasUnknownRequest { get; set; }

        public bool HasError { get { return (ErrorMessage != null); }}

        public IMessage ResponseMessage { get; set; }
    }
}
