Feature: LoaderRunner
	Tests for interactions with the Loader Runner

@unittest
Scenario: Run the load runner
	Given I have these customers in my database
		| id | name | representative | representative_email | representative_phone |
		| 1  | name | representative | representative_email | representative_phone |
	And I have these metrics in my database
		| customer_id | id | name | expression |
		| 1           | 1  | name | expression |
	And I have a loader manager
	And I have a loader runner
	When I run the load runner
	Then all customers and metrics will be saved in the database