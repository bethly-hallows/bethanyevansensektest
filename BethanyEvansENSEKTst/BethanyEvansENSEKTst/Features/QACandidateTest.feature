Feature: QACandidateTest
	Test scenarios for interview

Background:
	Given I call the Login endpoint
	And I call the Reset endpoint

#Fails on Gas and Nuclear cases due to Buy request not returning expected success message
Scenario Outline: Reset the test data
	Given I call the Buy endpoint for energy type <energyId> and quantity <quantity>
	And I call the Reset endpoint
	When I call the Orders endpoint
	Then no Order data is retrieved for today

	Examples:
		| energyId | quantity |
		| 1        | 1        |
		| 2        | 2        |
		| 3        | 3        |
		| 4        | 4        |

#Fails on Gas and Nuclear cases due to Buy request not returning expected success message
#Fails as the Electric type returns unexpected model  
# eg::
# EXPECTED:  {
#    "fuel": "electric",
#    "id": "080d9823-e874-4b5b-99ff-2021f2a59b25",
#    "quantity": 23,
#    "time": "Mon, 7 Feb 2022 00:01:24 GMT"
#  },
# ACTUAL: {
#    "Id": "167c64f3-3e69-4edd-aa79-e86bbe04c6f3",
#    "fuel": "Elec",
#    "quantity": 555,
#    "time": "Thu, 06 Apr 2023 09:51:52 GMT"
#  }

Scenario Outline: Buy a quantity of fuel
	Given I call the Buy endpoint for energy type <energyId> and quantity <quantity>
	When I call the Orders endpoint
	Then there is an order for each type of energy with quantity <quantity> and energy type <energyType>

	Examples:
		| energyId | energyType | quantity |
		| 1        | Gas        | 1        |
		| 2        | Nuclear    | 2        |
		| 3        | Electric   | 3        |
		| 4        | Oil        | 4        |

Scenario: Assert orders placed before today
	Given I call the Orders endpoint
	Then there are 5 orders placed before today

#Fails as calling the Delete endpoint with the order id results in a 500 Internal Server Error
Scenario: Deleting an order removes it from Orders
	Given I call the Buy endpoint for energy type 4 and quantity 3
	And I call the Orders endpoint
	And there is an order for each type of energy with quantity 3 and energy type Oil
	When I call the Delete Orders endpoint
	Then the order is deleted from the Orders

#Fails as calling the Get Order endpoint with the order id results in a 500 Internal Server Error
Scenario: Get an order by Order Id returns the correct order
	Given I call the Buy endpoint for energy type 4 and quantity 3
	And I call the Orders endpoint
	And there is an order for each type of energy with quantity 3 and energy type Oil
	When I call the Get Order endpoint
	Then the correct order is returned