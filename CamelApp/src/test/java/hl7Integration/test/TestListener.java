package hl7Integration.test;

import ca.uhn.hl7v2.HL7Exception;
import ca.uhn.hl7v2.model.Message;
import ca.uhn.hl7v2.parser.PipeParser;
import ca.uhn.hl7v2.util.Terser;
import org.apache.camel.component.hl7.HL7MLLPCodec;
import org.junit.Test; 
import org.junit.Assert;
import org.apache.camel.test.junit4.CamelTestSupport;
import org.apache.camel.spring.Main;

public class TestListener extends CamelTestSupport {
	 
	@Test
    public void testHl7Codec() throws Exception {
		
		context.getRegistry().bind("hl7codec", new HL7MLLPCodec());
        String inMessage = "MSH|^~\\&|hl7Integration|hl7Integration|||||ADT^A01|||2.5|\r" +
                "EVN|A01|20130617154644\r" +
                "PID|1|465 306 5961||407623|Wood^Patrick^^^MR||19700101|1|||High Street^^Oxford^^Ox1 4DP~George St^^Oxford^^Ox1 5AP|||||||";

        String out = (String) template.requestBody("mina:tcp://127.0.0.1:8889?sync=true&codec=#hl7codec", inMessage);

        assertNotNull(out);
        //Check that the output is an ACK message
        assertEquals("ACK", getMessageType(out));


    }
	 
	 private String getMessageType(String message) throws HL7Exception{
	        PipeParser pipeParser = new PipeParser();
	        Message hl7Message = pipeParser.parse(message);
	        Terser terser = new Terser(hl7Message);

	        return terser.get("/MSH-9");

	    }

}
