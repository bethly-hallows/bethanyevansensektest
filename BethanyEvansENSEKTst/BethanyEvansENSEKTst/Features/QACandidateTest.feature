Feature: QACandidateTest
	Test scenarios for interview

Scenario: Reset the data
	Given I call the Reset endpoint
	When I call the Energy endpoint
	And I call the Orders endpoint
	Then no data is retrieved