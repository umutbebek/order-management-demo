# order-management-demo
small demo to test and connect some different projects
## What is this
This is a visual studio solution which includes different types of projects to do a small job. It has a .net core mvc project for web UI which talks to a .net core web api (via only jquery ajax calls) and that api talks to an in-memory (EF) db and rabbitmq.

Web UI lists a simple order list, user can insert an order; but instead writing directly to the db; API writes it to rabbitMQ; then our processor (worker process) process that message queue (simulates) and writes the order into the DB (again via API calls). Solution seperated into multiple projects for this small task.

## How to run
1. run a rabbitmq container (using windows 10 - Docker Desktop) with this command:

docker run -d --hostname myRabbitHost --name myRabbit -p 5672:5672 -p 15672:15672 rabbitmq:3-management

2. Run the solution with multiple start up projects setup and set "WebUI, API and Processor" as startups

## What to see
I did not change the default .net core mvc project, so when it starts you will see "Orders" menu on the navbar (the address should be https://localhost:7080/)

"Orders" menu calls the "order/list" page to display in-line memory (EF) records (3 by default)

"Detail" button next to each record goes to "order/{id}" detail page

"Add New" button goes to "order/new" page to add a new record (after adding it returns to list page again automatically). But you will not see the inserted order there yet. Because new order will be sent to rabbitmq for processing. Our processor app will handle the queue with 5 seconds delays (to simulate long job), and if you refresh the list after 5 seconds, the record should be visible then.



