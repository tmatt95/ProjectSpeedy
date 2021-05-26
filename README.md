# Project Speedy
Welcome to propject speedy. An application designed to support lean software development.

[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=tmatt95_ProjectSpeedy&metric=code_smells&token=6cc30f98f6262ce60b204958d0a292d80d694a28)](https://sonarcloud.io/dashboard?id=tmatt95_ProjectSpeedy)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=tmatt95_ProjectSpeedy&metric=coverage&token=6cc30f98f6262ce60b204958d0a292d80d694a28)](https://sonarcloud.io/dashboard?id=tmatt95_ProjectSpeedy)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=tmatt95_ProjectSpeedy&metric=sqale_rating&token=6cc30f98f6262ce60b204958d0a292d80d694a28)](https://sonarcloud.io/dashboard?id=tmatt95_ProjectSpeedy)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=tmatt95_ProjectSpeedy&metric=alert_status&token=6cc30f98f6262ce60b204958d0a292d80d694a28)](https://sonarcloud.io/dashboard?id=tmatt95_ProjectSpeedy)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=tmatt95_ProjectSpeedy&metric=reliability_rating&token=6cc30f98f6262ce60b204958d0a292d80d694a28)](https://sonarcloud.io/dashboard?id=tmatt95_ProjectSpeedy)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=tmatt95_ProjectSpeedy&metric=security_rating&token=6cc30f98f6262ce60b204958d0a292d80d694a28)](https://sonarcloud.io/dashboard?id=tmatt95_ProjectSpeedy)
[![Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=tmatt95_ProjectSpeedy&metric=sqale_index&token=6cc30f98f6262ce60b204958d0a292d80d694a28)](https://sonarcloud.io/dashboard?id=tmatt95_ProjectSpeedy)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=tmatt95_ProjectSpeedy&metric=vulnerabilities&token=6cc30f98f6262ce60b204958d0a292d80d694a28)](https://sonarcloud.io/dashboard?id=tmatt95_ProjectSpeedy)



## The Problem
There are a large number of project managament applications / websites which let you create stories / tasks. These tend to suit a waterfall / agile approach to software development. They tend to have a number of issues including:
* Not being simple to set up.
* Not quick to use.
* The applications are too flexible.

## The Idea
The idea is to create an application which will allow users to create projects. User can brain storm problems which may stop the project from succeeding. Bets can be made that will hopefully solve the problems (actioned by developers).

## Tech Stack
The following technologies have been used in the creation of this proof of concept:

### Asp.net Core 5
Asp.net core 5 has a react template which I used as a base and was a quick way of pulling together the front and back end for the proof of concept.

### CouchDb
This is used to store the application data. I have used relational database a lot recently and wanted to see how easy it was to call couchdb in a test application. 

### React
It was a choice of React or Angular. I have already used angular on a test project before and thought it would be interesting to try and gain some experience with react. The virtual dom is really interesting.

### Bootstrap 5
Not ready for use on anything other than a test app. I have used this primarily as it is not longer tied to Jquery. So far it has been very robust.

### Swagger
Used as a base for the API design.

### Balsamiq
Used to generate the first version of the application which was just a wireframe mock up I could click through before commiting to coding it.

