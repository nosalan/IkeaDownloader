### Description

This application, written in c#/dotnet core, is a simple price comparer for an Ikea product (crrently one is upported). It exposes REST API. Once the GET request is received, the app downloads IKEA webpage for given product, parses the HTML to extract the price and then compares the obtained price with the price from cache/database. The price in cache (REDIS) comes from previous hits for given product. 
If the price is lower than in the past, the information is returned that there is a discount. Otherwise, the information is returned that the price is the same, or higher.

This is mostly a demonstrational application to show the **component testing** approach / **hexagonal architecture.**
Component testing is possible by mocking the external interfaces.
In this application, there are three dependencies to external world:
1) IKEA Server (direction: -->)
2) Redis Db (direction: -->)
3) REST API (direction: <--)

In the hexagonal architecture terminology these are called ports. 

Direction arrow represents who initiates the communication. In case of IKEA and Redis the direction is outbound, it's the application that makes the request to external world.
In case of REST API the direction is inbound, it's the customer that makes the request to the application. 

Once the outbound dependencies are abstracted behind an interface, they can be mocked or faked, so the component test is testing the entire domain logic. Different implementation of the interface is provided in the production run and different in the test run.
For example, the IKEA Webpage is abstracted behind IPageDownloader interface.
- for production run, the interface is implemented by real http connection to IKEA webpage,
- for test run, the interface is mocked to provide different responses according to test scenarios.
The rest of the application (domain) remains the same for both runs because the domain dependes on the interface only. 


### Instruction for running the application locally

This application uses Redis database.
If you would like to run this application locally with local Redis then install Redis for Windows:
https://github.com/MicrosoftArchive/redis/releases
Choose msi to install Redis as Windows Service
