# spendmanagement-receipts-api
This application has the purpose of being a gateway for the spendmanagement project.

# How does it works?
![Alt text](SpendManagementDiagramFlow.jpg?raw=true "Title")

# Projects
> [spendmanagement-identity-api](https://github.com/fmattioli/spendmanagement-identity) <br/>
> [receipts-commandhandler-api](https://github.com/fmattioli/spendmanagement-receipts-commandhandler-api)  <br/>
> [receipts-queryhandler-api](https://github.com/fmattioli/spendmanagement-receipts-queryhandler-api)  <br/>
> [receipts-domain-api](https://github.com/fmattioli/spendmanagement-receipts-domain-api)  <br/>

#  Packages
> [receipts-contracts](https://github.com/fmattioli/spendmanagement-receipts-contracts) <br/>
> [receipts-topics](https://github.com/fmattioli/spendmanagement-topics) <br/>


# How to make it works on your machine?
This project is just one piece of several projects that exist in the context of the SpendManagement ecosystem.
Therefore, to make it work, you will need to configure other projects, as described order below:

To start is necessary to run the docker-compose that will create the component dependencies that are used by projects inside the SpendManagament ecosystem (As for example: Kafka, Mongo and Postgresql).
> https://github.com/fmattioli/spendmanagement-common-containers/blob/main/docker-compose.yml

After that, is necessary to run the docker-composes related to the applications inside the SpendManagament ecosystem.

The first project that is necessary to run the docker-compose is the SpendManagament.Identity. This project has the purpose to generate JWT tokens and manage the users who access and use the SpendManagament project.
>https://github.com/fmattioli/spendmanagement-identity/blob/main/docker-compose.yml

The second project that is necessary to run the docker-compose is SpendManagament.CommandHandler.Api. This project has the purpose of receiving writting of spends and produce kafka commands.
> https://github.com/fmattioli/spendmanagement-receipts-commandhandler-api/blob/main/docker-compose.yml

The third project that is necessary to run the docker-compose is the SpendManagament.Receipts.QueryHandler.Api. This project has the purpose of being a ReadModel related to the receipts boundary context.
This project read data from a NoSQL database. 
> https://github.com/fmattioli/spendmanagement-receipts-queryhandler

The fourth project that is necessary to run the docker-compose is SpendManagament.Receipts.Domain. This project has the purpose of receiving commands on a Kafka topic and converting them into domain events. This project also stored these commands and events in a database, respecting the event sourcing pattern.
> https://github.com/fmattioli/spendmanagement-receipts-domain-api
