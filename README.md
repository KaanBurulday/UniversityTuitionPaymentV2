# UniversityTuitionPaymentV2
This is the 2nd version of the midterm project

You can also use the app via Azure: <a>universitytuitionpaymentv2.azurewebsites.net</a>


<h1> 0. Introduction </h1>
Hello dear all, 
<br></br>
Due to being new to web api development with visual studio basic, I have made many mistakes in my first iteration, 
which led me to miss many requirements on the specifications. By starting over, I think I covered some the previous topics of 
Use Cases, Paging, Versionable.
<br></br>
There are going to be 3 main definitions for my tasks, the latter 2 are not important.

<h2>1. SE 4458 Midtterm Controllers</h2> 
Created to cover the previous use cases with the addition of sending and receiving messages
from the azure service bus queue.

<h2>2. Generator Controller</h2> 
This for the convenience of testing the application, it will first clean (delete all data that exists in the tables) the db then
generate model objects, such as terms, universities, students, etc., and insert them to the db.

<h2>3. Generic Controllers</h2> 
Includes almost all of the necessary rest operations in order to get, insert, update and delete model objects.

<br></br>
To achieve the second assignment, I have created 2 services (MessageSenderService, MessageReceiverService) that are able to 
send/receives messages to/from an Azure Service Bus Queue. You can test sending and receiving messages with MessageController controller. 
In the PayTuition operation of BankingApp controller, a message (PaymentInfo object) will be enqueued. To consume, you can either 
consume it from the api or run the MessageQueueSender project.
<br>
I hope that you wouldn't have any problems, if you do feel free to get in touch with me!

<h3>Endnote</h3>
Hello, soryy for the late upload to github I have noticed that the messagequeue sender and receiver were
not included, fixed them so that by starting the project via solution, you may be able to run them both.