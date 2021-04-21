Feature: Common Features
	Tests for common and misc elements

@unittest
Scenario: Confirm CustomersMetricsDatabaseContext Constructor for Options
	When I create a CustomersMetricsDatabaseContext with options
	Then It is created without incident

@unittest
Scenario: Confirm CustomersMetricsDatabaseContext Constructor for No Options
	When I create a CustomersMetricsDatabaseContext without options
	Then It is created without incident
	
@unittest
Scenario: Configure the DbSets
	Given I have these customers in my database
		| id | name | representative | representative_email | representative_phone |
		| 1  | name | representative | representative_email | representative_phone |
	And I have these metrics in my database
		| customer_id | id | name | expression |
		| 1           | 1  | name | expression |
	When I create a CustomersMetricsDatabaseContext without options
	And I set the customer and metrics db set appropriately
	Then It is created without incident
	And customers and metrics are retrieved appropriately