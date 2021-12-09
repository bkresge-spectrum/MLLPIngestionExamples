FROM maven:latest
RUN mkdir -p /usr/local/src/camel
WORKDIR /usr/local/src/camel
ADD . /usr/local/src/camel
RUN mvn install -DskipTests
CMD ["mvn","camel:run","-DskipTests"]