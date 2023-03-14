# PlatformService
Service for platform management in the microservices architecture.

## Table of contents
* [Introduction](#introduction)
* [Used technologies](#used-technologies)
* [Features](#features)
* [Project status](#project-status)

## Introduction
This project is a part of a bigger microservices architecture designed to help people remember console commands for different platforms. It focues on creating, editing, 
etc. of platforms that will later be used by the commands service (https://github.com/Kprzyby/CommandService) to link commands to them. The project was deployed locally in
Kubernetes (deployment files here - https://github.com/Kprzyby/PlatformAppK8S).

## Used technologies
* .NET 6.0
* C#
* Swagger
* Azure Service Bus
* Entity Framework Core 7.0.2
* Docker

## Features
* Creating platforms
* Editing existing platforms
* Removing existing platforms
* Publishing messages to the service bus concerning the events listed above
* Loading a singuar platform
* Loading all existing platforms (pagination, filtering and sorting included)
* Automated migrations

## Project status
The project is still in development. In the future I plan on adding authentication, authorization and better error handling.
