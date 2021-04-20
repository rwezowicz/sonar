Feature: MetricsService
	Tests for interactions with the Contoso Metrics Service API endpoint

@unittest
Scenario: Confirm Successful Request for Metrics Service Data
	Given I have a list of metrics data
		| customer_id | id | name | expression |
		| 1           | 1  | name | expression |
	And I have a metrics service
	When I request a list of metrics data for customer_id 1
	Then the following metric is returned
		| customer_id | id | name | expression |
		| 1           | 1  | name | expression |

@unittest
Scenario: Retrieve an implementation of IMetricsService
	Given I have a service collection
	When I request an implementation of ICustomerIMetricsServiceService
	Then I receive a MetricsService object