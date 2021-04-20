Feature: CustomerService
	Tests for interactions with the Contoso Customer Service API endpoint

@unittest
Scenario: Confirm Successful Request for Customer Service Data
	Given I have a list of customer data
		| id | name | representative | representative_email | representative_phone |
		| 1  | name | representative | representative_email | representative_phone |
	And I have a customer service
	When I request a list of all customer data
	Then my entire customer data list is returns

@unittest
Scenario: Retrieve an implementation of ICustomerService
	Given I have a service collection
	When I request an implementation of ICustomerService
	Then I receive a CustomerService object