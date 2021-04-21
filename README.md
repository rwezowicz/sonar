# Customer and Metrics Loader

## Usage
- Publish the Customer Metrics Database project to the SQL Server of choice
- Build and run the Customer Metrics Loader Console Application

*** 
## Configuration
* appsettings.json
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  },
  "SonarApi": {
    "BaseUrl": "Base Api Url",
    "CustomerEndpoint": "Customer Endpoint",
    "MetricsEndpoint": "Metrics Endpoint"
  },
  "ConnectionStrings": {
    "Default": "Connection String"
  }
}
```
***
## C# Solution Projects
### CustomerMetricsLoader
- Contains the main loader runner for the system:
This is a pure console application that configures dependancy injection and then runs the loader which loads the data from the api endpoints and stores them in the database configured in the appsettings.json file
### ContosoCore
- Contains the main functionality for the system
- **Context** - the EF interface to the database
- **Helpers** - a static class of helpers
- **Interfaces** - interfaces for objects 
- **Managers** - the main interaction class that allows for retrieval and saving of data from the console loader
- **Models** - models for objects used
- **Services** - services used to call the api endpoints
### CustomersMetricsDatabase
* Contains the database schema and code used to create the database
### CustomerMetricsLoaderTests
* Unit and Behavior Driven Testing
- This utilize the BDD testing system called [SpecFlow](https://specflow.org/) which allows for easily readable test constructs written in English using the Gherkin (Given/When/Then) language
