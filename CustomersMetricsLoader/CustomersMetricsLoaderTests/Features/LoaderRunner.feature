Feature: LoaderRunner
	Tests for interactions with the Loader Runner

Scenario: Run the loader runner (Update)
	Given I have an api that will return the following list of customer data
		| id | name | representative | representative_email | representative_phone |
		| 1  | name | representative | representative_email | representative_phone |
	And I have these customers in my database
		| id | name | representative | representative_email | representative_phone |
		| 1  | name | representative | representative_email | representative_phone |
	And I have an api that will return the following list of metrics data
		| customer_id | id | name | expression |
		| 1           | 1  | name | expression |
	And I have these metrics in my database
		| customer_id | id | name | expression |
		| 1           | 1  | name | expression |
	And I have a loader manager
	And I have a loader runner
	When I run the load runner
	Then all customers and metrics will be saved in the database
	And the number of added customers will be 0
	And the number of updated customers will be 1
	And the number of added metrics will be 0
	And the number of updated metrics will be 1

Scenario: Run the loader runner (Add)
	Given I have an api that will return the following list of customer data
		| id | name | representative | representative_email | representative_phone |
		| 1  | name | representative | representative_email | representative_phone |
	And I have these customers in my database
		| id | name | representative | representative_email | representative_phone |
	And I have an api that will return the following list of metrics data
		| customer_id | id | name | expression |
		| 1           | 1  | name | expression |
	And I have these metrics in my database
		| customer_id | id | name | expression |
	And I have a loader manager
	And I have a loader runner
	When I run the load runner
	Then all customers and metrics will be saved in the database
	And the number of added customers will be 1
	And the number of updated customers will be 0
	And the number of added metrics will be 1
	And the number of updated metrics will be 0

Scenario: Run the loader runner (Add and Update)
	Given I have an api that will return the following list of customer data
		| id | name | representative | representative_email | representative_phone |
		| 1  | name | representative | representative_email | representative_phone |
		| 2  | name | representative | representative_email | representative_phone |
	And I have these customers in my database
		| id | name | representative | representative_email | representative_phone |
		| 1  | name | representative | representative_email | representative_phone |
	And I have an api that will return the following list of metrics data
		| customer_id | id | name | expression |
		| 1           | 1  | name | expression |
	And I have these metrics in my database
		| customer_id | id | name | expression |
		| 1           | 1  | name | expression |
	And I have a loader manager
	And I have a loader runner
	When I run the load runner
	Then all customers and metrics will be saved in the database
	And the number of added customers will be 1
	And the number of updated customers will be 1
	And the number of added metrics will be 0
	And the number of updated metrics will be 1