Feature: QACandidateTest
	Test scenarios for interview

Background:

#Given I call the Reset endpoint
Scenario: Reset the data
	When I call the Energy endpoint
	And I call the Orders endpoint
	Then no data is retrieved

Scenario Outline: Order a quantity of fuel
	Given I call the Buy endpoint for each <energyType> and <quantity>
	When I call the Orders endpoint
	Then there is an order for each type of energy with quantity <quantity>

	Examples:
		| energyType | quantity |
		| 1          | 1        |
		| 2          | 2        |
		| 3          | 3        |
		| 4          | 3        |
#Scenario: Assert orders placed before today
#Given I call the