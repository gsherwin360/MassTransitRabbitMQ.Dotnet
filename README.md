# MassTransitRabbitMQ.Dotnet

This demo API demonstrates inter-service communication across different modules within a single codebase, emphasizing a loosely coupled architecture. 
It utilizes MassTransit with RabbitMQ to enable asynchronous and scalable messaging between services.

## Overview
- **RabbitMQ**: Acts as the underlying message broker responsible for transporting and routing messages between services, ensuring reliable delivery
- **MassTransit**: A .NET library that simplifies working with message brokers like RabbitMQ. It provides tools for message publishing, subscription, and handling, enabling asynchronous and decoupled service communication.

## Key Features
- **Asynchronous Messaging**: Utilize MassTransit and RabbitMQ for asynchronous, reliable, and scalable communication between services.
- **Clean Architecture**: Adheres to clean architecture principles for maintainable and testable code.
- **Docker Integration**: Uses Docker for easy setup and management of dependencies like MongoDB and RabbitMQ.
  
## Development Prerequisites
Before diving into development with this project, ensure you have the following prerequisites:
- **Development Environment**: Either Visual Studio 2022 (IDE) or Visual Studio Code (Source-code editor)
- **.NET 8**: Required framework version for building and running the project
- **Docker Desktop**: Required for running MongoDB and RabbitMQ containers

## Getting Started
To run the API locally, follow these steps:
1. Clone this repository to your local machine.
2. Ensure you have Docker installed and running.
3. Open a shell and navigate to the `tools` folder within the cloned repository.
4. Run the following command to start MongoDB and RabbitMQ containers in detached mode: 
   ```bash
   docker compose up -d
5. Access Mongo Express:
    - Visit http://localhost:8081 in your browser. The default credentials are "admin:pass".
6. Access RabbitMQ Management:
    - Visit http://localhost:15672/ in your browser. The credentials are "guest:guest" (configured in appsettings.json).
7. Build and Run the API:
    - Open the solution in Visual Studio or Visual Studio Code.
    - Run the `ServiceName.Api` projects. Multiple services may need to be run simultaneously for full functionality.
      
### Working with RabbitMQ and MassTransit

#### Publishing a Message to RabbitMQ:
1. **Message Creation**: The application uses MassTransit to create and structure messages, providing a standardized approach for defining message content and logic.
2. **Message Translation**: MassTransit translates the constructed message into a format understandable by RabbitMQ and forwards it for delivery.
3. **Message Queueing**: RabbitMQ receives the message and places it into the appropriate queue. The queuing mechanism, typically FIFO (First In, First Out), ensures messages are processed in the order they are received, unless otherwise configured.

#### Consuming a Message from RabbitMQ:
1. **Consumer Definition**: Services define message consumers through MassTransit, specifying which messages they are interested in receiving and processing.
2. **Subscription Setup**: MassTransit sets up the necessary infrastructure to listen to RabbitMQ queues, enabling consumers to await incoming messages.
3. **Message Delivery**: When RabbitMQ identifies a message matching a consumer's criteria, it delivers the message to MassTransit.
4. **Message Processing**: MassTransit processes the received message and delegates it to the application’s consumer logic, where business rules dictate how the message is handled and processed.

#### Integration.Contracts
- A sub-project within the service layer that focuses on defining interfaces that establish standardized communication protocols, enabling services to communicate without being tightly coupled to each other, promoting consistency and reliability in inter-service communication.
