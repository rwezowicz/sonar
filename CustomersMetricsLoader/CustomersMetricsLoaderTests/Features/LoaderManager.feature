Feature: LoaderManager
	Tests for interactions with the Loader Manager

Scenario: Get All Customers
	Given I have an api that will return the following list of customer data
		| id | name | representative | representative_email | representative_phone |
		| 1  | name | representative | representative_email | representative_phone |
	And I have a loader manager
	When I request a list of all customers
	Then all customers will be returned

Scenario: Get A List of Metrics for a Customer
	Given I have an api that will return the following list of metrics data
		| customer_id | id | name | expression |
		| 1           | 1  | name | expression |
	And I have a loader manager
	When I request a list of all metrics for customer id 1
	Then all metrics will be returned for that customer

Scenario: Save a list of customers (Update)
	Given I have these customers in my database
		| id | name | representative | representative_email | representative_phone |
		| 1  | name | representative | representative_email | representative_phone |
	And I have a loader manager
	And I want to update these customers
		| id | name | representative | representative_email | representative_phone |
		| 1  | name | representative | representative_email | representative_phone |
	When I save all the customer data
	Then the data will successfully save

Scenario: Save a list of customers (Add)
	Given I have these customers in my database
		| id | name | representative | representative_email | representative_phone |
	And I have a loader manager
	And I want to update these customers
		| id | name | representative | representative_email | representative_phone |
		| 1  | name | representative | representative_email | representative_phone |
	When I save all the customer data
	Then the data will successfully save

Scenario: Unable to Save Customers
	Given I have these customers in my database
		| id | name | representative | representative_email | representative_phone |
		| 1  | name | representative | representative_email | representative_phone |
	And I have a loader manager
	And I want to update these customers
		| id | name | representative | representative_email | representative_phone |
		| 1  | name | representative | representative_email | representative_phone |
	And the database isn't working correctly
	When I save all the customer data
	Then the log will show "error occurred when saving customers"

Scenario: Save a list of metrics (Update)
	Given I have these metrics in my database
		| customer_id | id | name | expression |
		| 1           | 1  | name | expression |
	And I have a loader manager
	And I want to update these metrics
		| customer_id | id | name | expression |
		| 1           | 1  | name | expression |
	When I save all the metrics data
	Then the data will successfully save

Scenario: Save a list of metrics (Add)
	Given I have these metrics in my database
		| customer_id | id | name | expression |
	And I have a loader manager
	And I want to update these metrics
		| customer_id | id | name | expression |
		| 1           | 1  | name | expression |
	When I save all the metrics data
	Then the data will successfully save

Scenario: Unable to Save Metrics
	Given I have these metrics in my database
		| customer_id | id | name | expression |
		| 1           | 1  | name | expression |
	And I have a loader manager
	And I want to update these metrics
		| customer_id | id | name | expression |
		| 1           | 1  | name | expression |
	And the database isn't working correctly
	When I save all the metrics data
	Then the log will show "error occurred when saving metrics"