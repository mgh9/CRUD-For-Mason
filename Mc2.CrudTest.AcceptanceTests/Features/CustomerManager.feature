Feature: Customer Manager
    As an operator,
    I wish to be able to Create, Update, Delete customers and list all customers

@mytag
Scenario: Operator creates a customer
    Given a new customer with the following details:
        | FirstName | LastName      | DateOfBirth | Email                        | PhoneNumber       | BankAccountNumber        |
        | Kevin     | Mitnick       | 1990-01-01  | mahdi.ghardashpoor@gmail.com | +989364726673     | DE03601202003749456545   |
    When I add the new customer to the system
    Then the customer should be added successfully

Scenario: Operator lists customers
    Given I have added a customer to the system
    When I list all customers
    Then I should see the added customer in the list

Scenario: Operator updates a customer
    Given I have added a customer to the system
    When I update the customer's email to "mahdi_email_updated@gmail.com"
    Then the customer's email should be updated successfully

Scenario: Operator deletes a customer
    Given I have added a customer to the system
    When I delete the customer from the system
    Then the customer should be deleted successfully
