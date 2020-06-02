# MetaWeather

[![Codacy Badge](https://api.codacy.com/project/badge/Grade/44211e9b1ef34c658df571f370047276)](https://app.codacy.com/manual/mcquiggd/MetaWeather?utm_source=github.com&utm_medium=referral&utm_content=mcquiggd/MetaWeather&utm_campaign=Badge_Grade_Settings)
[![Build Status](https://dev.azure.com/mcquiggd/MetaWeather/_apis/build/status/mcquiggd.MetaWeather?branchName=master)](https://dev.azure.com/mcquiggd/MetaWeather/_build/latest?definitionId=1&branchName=master)
![Azure DevOps tests](https://img.shields.io/azure-devops/tests/mcquiggd/MetaWeather/1)

A solution to a coding assessmenent test, with the initial specification that the resulting application should feature an ASP.Net MVC Application with a React front end, with authentication and authorization.

#### Design Decisions

D1: As the external API that returns the desired data is:

a. Unsecured.
 
b. Returns a variety of fields which are not required to be displayed in the desired UI.

Implement a secured API proxy to the external API, offering control of authentication methods, authorization profiles, transformation of data to the desired model, etc. 

D2. As authentication and authorization is to be:

a. Shared between both the API proxy and React client.

Implement IdentityServer4 as a Secure Token Server (STS).

### Implementation

TDD / BDD style for establishing initial "structure", namely interfaces, method signatures, entities and their relationships.

Unit Testing for components where applicable (examples, rather than full coverage, is provided)

Use of Live Unit Testing (in this instance, via NCrunch).

Use of NSubstitute, AutoFixture, xUnit.

Use of Refit for building REST client based on interface definitions.

GitHub Flow in preference to Git Flow - in this scenario development branch / complex branching strategies are not required. 

Azure Devops for CI builds / test runs on master branch.

Test and Build history are available at this [link](https://dev.azure.com/mcquiggd/MetaWeather/_build?definitionId=1&_a=summary&view=ms.vss-pipelineanalytics-web.new-build-definition-pipeline-analytics-view-cardmetrics)

For full list of technologies utilised, please refer to: 

[Dependencies](https://github.com/mcquiggd/MetaWeather/network/dependencies)

Feature branches are not deleted after merging to master, to allow viewing of each defined stage of development.

For full list of commits, please refer to:

https://github.com/mcquiggd/MetaWeather/commits/master