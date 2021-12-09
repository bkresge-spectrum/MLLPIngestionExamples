# Apache Camel HL7 / Mina Example Project
This project is an example Java project that leverages Maven, Spring, and MINA to build out an example of a TCP endpoint listener for HL7 messaging and routing.  This example responds with an acknowledgment message.  This is based off of Ignacio Suay's work in 2014 with a 2.x version of Apache Camel.

# Dependencies
* Defined in pom.xml, but recommend Java 1.8 or higher.
* Maven

# Run Instructions
* After cloning this repo, to test it:
```
$ mvn camel:run
```
* In a separate window, run the test unit.
```
$ mvn -Dtest=TestListener test
```
# Maintainer
[Brian Kresge](mailto:brian.kresge@sprectrummg.com)