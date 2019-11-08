# Financial Chat

Simple browser-based chatroom app that supports requesting financial data from the Stooq API by using the command `/stock=stock_code`

### Setup

In order to run this application, the following tools are required:

- PostgreSQL
- RabbitMQ
- Node.js
- .NET Core

#### Database

After you have installed PostgreSQL from its [official site](https://www.postgresql.org/download/) and configured the server, create a database called `chatroom`.
In a new terminal, access the `psql` CLI and execute\
`CREATE DATABASE chatroom;`\
No further steps are required as the tables and relationships should be automatically created by the Entity Framework code-first migrations. 

#### Node.js

Node.js is required in order to run the frontend that's been built in React/Redux.

#### RabbitMQ

The app uses RabbitMQ as a message broker. You can install RabbitMQ via Chocolatey by running\
`choco install rabbitmq`\
or by downloading the official installer from the [official page](https://www.rabbitmq.com/)

Before running the app, make sure that both `postgresql` and `rabbitmq` services are running. On a Windows terminal run\
`net start postgresql`\
`net start rabbitmq`\
also check that the `npm` and `dotnet` programs are added to the PATH.

#### Running the app

1. First, run the Bot service by accessing `\FinancialChat.Bot` and start this console project app with by running\
`dotnet run`\
This will start the server from this simple RPC communication system and listen to requests in the queue. 

2. Next, cd to `\FinancialChat.Web` and start the Web project by running\
`dotnet run`\
This should internally run an `npm install` command (to recover all packages required for the frontend) and also run the database migrations, which are going to be run only the first time the app is run pointing to that particular database. 
The app should run at the port specified in `launchSettings.json`, which in this case are 5000 and 5001. Therefore, the URL will be localhost:5001 or localhost:5001.

3. Since the API and the SignalR hub require authorization, a user has to be created in order to obtain the token. The UI only supports login, we'll have to create a user with the API. For that, make a request to\
`POST /api/account/register`\
with the following body\
	```
	{
		"username": <username>,
		"password": <password>
	}
	```
	After that, you should be able to log in via the UI or make other API calls with the obtained beared token.

4. Finally, go to either localhost:5001 or localhost:5001 to access the chatroom application. 