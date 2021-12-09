package hl7Integration.camel;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Component;

import ca.uhn.hl7v2.model.Message;

@Component
public class ShowPorts {

	private static final Logger log = LoggerFactory.getLogger(RespondACK.class);

    public Message process(Message in) throws Exception {
        log.info(in.toString());
        Message out =  in.generateACK();
        log.info(out.toString());
        return out;

    }
	
}
