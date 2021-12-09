package hl7Integration.camel.routes.in;

import org.apache.camel.builder.RouteBuilder;
import org.apache.camel.CamelContext;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.apache.camel.impl.DefaultCamelContext;
import org.apache.camel.Processor;
import org.apache.camel.Exchange;
import ca.uhn.hl7v2.model.Message;
import ca.uhn.hl7v2.parser.PipeParser;

import org.springframework.stereotype.Component;
import org.springframework.boot.autoconfigure.EnableAutoConfiguration;
import org.springframework.context.annotation.Configuration;

@Configuration
@Component
public class InboundRouteBuilder extends RouteBuilder {

	private static final Logger log = LoggerFactory.getLogger(InboundRouteBuilder.class);
	
	
	@Override
	public void configure() throws Exception{
		
		from("hl7listener")
			.routeId("route_hl7listener")
			.startupOrder(997)
			.unmarshal()
			.hl7(false)
			.to("bean:respondACK?method=process");
			//.end();
		
		restConfiguration()
			.host("localhost")
			.port(8081);
		
		rest("/rest/v1")
			.get("/showports")
			.to("direct:hello");
		
		from("direct:hello")
			.transform().constant("HL7 Ports: 8889");
	}
}
