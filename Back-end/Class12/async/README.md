#### Class 12 | Message Brokers and Traceability ID
    -   Presenting RabbitMQ
        -   Presenting Microsservices Project
<div align = 'center' justify-content = 'space-around'>
  <img width="1604" alt="Microsservices Project" src="./img/Microsservices_Project.png">
</div>
<br>
        -   Microservice architecture
            -   Reference
                -   https://www.alura.com.br/artigos/microservicos-com-dotnetcore-comunicacao-entre-servicos
<table style="display: flex; justify-content: center; align-items: center; width: 50%;">
    <thead>
        <tr>
            <th>Problem</th>
            <th>Solution</th>
            <th>Explanation</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>In this architecture, each component/service is autonomous and does not know the other</td>
            <td>
                Use a message broker (communication between each service)<br>
                In our case: RabbitMQ + Rebus (Event Bus)
            </td>
            <td>
                Event Bus is a software artifact that allows the microservices to post notifications (events) that indicate the occurrence of something relevant to other microservices
                <br>The RabbitMQ + Rebus also allows them to subscribe to (and receive messages from) events
            </td>
        </tr>
    </tbody>
</table>

    -   Message Broker with Rebus and RabbitMQ
    -   Azure Service Bus: Sending Messages to the Queue
        -   Azure Service Bus is a RabbitMQ component
        -   Documentation
            -   https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-dotnet-get-started-with-queues?tabs=passwordless
        -   Install packages
            -   dotnet add package Azure.Messaging.ServiceBus
            -   dotnet add package Azure.Identity
    -   Azure Service Bus: Receiving Queue Messages
        -   Install packages
            -   dotnet add package Azure.Messaging.ServiceBus
            -   dotnet add package Azure.Identity
    -   Virtualization: ASP.NET Core Microservices Project
<div align = 'center' justify-content = 'space-around'>
  <img width="1604" alt="Virtualization - part 1" src="./img/Virtualization1.png">
  <img width="1604" alt="Virtualization - part 2" src="./img/Virtualization2.png">
  <img width="1604" alt="Virtualization - Virtual Machines Options" src="./img/Virtualization3.png">
  <img width="1604" alt="Virtualization - Virtual Machines concept" src="./img/Virtualization4.png">
  <img width="1604" alt="Virtualization - Virtual Machines vs Docker" src="./img/Virtualization5.png">
</div>
<br>
    -   Traceability ID with Jaeger UI
        -   Documentation
            -   https://www.jaegertracing.io/