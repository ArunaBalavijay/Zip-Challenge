# Zip Coding Challenge

## Quick Summary

This project is developed with a basic distributed system using microservices architecture. It uses `CQRS` (_Command Query Responsibility Segregation_) pattern to separate reads and writes, using commands to update data, and queries to read data. Also it uses `RabbitMQ` as service bus, to send messages across the distributed services, and `MongoDB`, which is a NoSQL document database to persist the data. 

Api gateway acts as a single point of entry for a defined group of microservices (just one service for the sake here) which sits in front of an `application programming interface` (API). We have Identity service as micro service which is responsible for handling the incoming messages (or actually the commands that will be distributed through the services bus). This service will let the user register their information, retrieve their details back, list all the registered users, create account and list them.

Using Docker and Docker Compose, entire application can be packed into the container and can access the api gateway locally through the container or can deploy the same into a virtual machine that runs in the cloud.

## Instructions

### Dockerization

Run below `docker-compose` command in an “elevated” PowerShell (run it as administrator) in Windows or in Mac terminal.

```bash
docker-compose up -d
```

_Note_: For the first time, it may take some time to download all the required images and to bring all the containers up.

### Health Check

Once the containers are up, the below health check api end point can be accessed in the browser to make sure the setup goes well.

[http://localhost:8080/healthcheck](http://localhost:8080/healthcheck)

The health check api should return the payload as below if the dockerization goes well locally.

```json
{"status":"Healthy"}
```

### End Points

All the end points can be accessed through `Postman` and the sample `cURL` commands are given below.

### Create User

POST [http://localhost:8080/identity](http://localhost:8080/identity)

```bash
curl -X POST \
  http://localhost:8080/identity \
  -H 'Accept: */*' \
  -H 'Accept-Encoding: gzip, deflate' \
  -H 'Cache-Control: no-cache' \
  -H 'Connection: keep-alive' \
  -H 'Content-Type: application/json' \
  -H 'Host: localhost:8080' \
  -H 'cache-control: no-cache' \
  -d '{
		"firstName": "Hello",
		"lastName": "World",
		"email": "hello.world@example.com",
		"salary": 1500,
		"expense": 500
	}'
```

### List Users

GET [http://localhost:8080/identity](http://localhost:8080/identity)

```bash
curl -X GET \
  http://localhost:8080/identity \
  -H 'Accept: */*' \
  -H 'Accept-Encoding: gzip, deflate' \
  -H 'Cache-Control: no-cache' \
  -H 'Connection: keep-alive' \
  -H 'Host: localhost:8080' \
  -H 'cache-control: no-cache'
```

### Get User

GET [http://localhost:8080/identity/hello.world@example.com](http://localhost:8080/identity/hello.world@example.com)

```bash
curl -X GET \
  http://localhost:8080/identity/hello.world@example.com \
  -H 'Accept: */*' \
  -H 'Accept-Encoding: gzip, deflate' \
  -H 'Cache-Control: no-cache' \
  -H 'Connection: keep-alive' \
  -H 'Host: localhost:8080' \
  -H 'cache-control: no-cache'
```

### Create Account

POST [http://localhost:8080/account](http://localhost:8080/account)

```bash
curl -X POST \
  http://localhost:8080/account \
  -H 'Accept: */*' \
  -H 'Accept-Encoding: gzip, deflate' \
  -H 'Cache-Control: no-cache' \
  -H 'Connection: keep-alive' \
  -H 'Content-Type: application/json' \
  -H 'Host: localhost:8080' \
  -H 'cache-control: no-cache' \
  -d '{
		"userEmail": "hello.world@example.com",
		"creditLimit": 1000,
		"balance": 1000
	}'
```

### List Accounts

GET [http://localhost:8080/account](http://localhost:8080/account)

```bash
curl -X GET \
  http://localhost:8080/account/ \
  -H 'Accept: */*' \
  -H 'Accept-Encoding: gzip, deflate' \
  -H 'Cache-Control: no-cache' \
  -H 'Connection: keep-alive' \
  -H 'Host: localhost:8080' \
  -H 'cache-control: no-cache'
```
