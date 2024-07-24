Feature: Grocery Features

        
  #  @smoke
  #  Scenario: GET products
  #      Given I perform a GET operation of "Product/GetProductById/{id}" 
  #        | ProductId |
   #       | 1         |
    #    And I should get the product name as "Keyboard"
        
    
    Background: Create Cart
        Given I send the "POST" request with the "CreateCart" endpoint
        When Execute the Create Cart request
        Then Verify Created should be true in CreateCart
        And Verify CartID should not be Blank New
    
    Scenario: Add Item in Cart
        Given I send the "POST" request with the "AddItemInCart" endpoint 
        And With the Cart Query Parameter
        And With the Cart valid JsonBody
        When Execute the Add Item request with all the parameters
        Then Verify the "201" Status Code of Add Item
        Then Verify Created should be true in AddItemInCart
        And Verify ItemID should not be Blank
        
    Scenario: Add Item in Cart API for invalid Json Data
        Given I send the "POST" request with the "AddItemInCart" endpoint 
        And With the Cart Query Parameter
        And With the Cart Invalid JsonBody
        When Execute the Add Item request with all the parameters
        Then Verify the "400" Status Code of Add Item
        
    Scenario: Get Item in Cart API 
        Given I send the "POST" request with the "AddItemInCart" endpoint 
        And With the Cart Query Parameter
        And With the Cart valid JsonBody
        When Execute the Add Item request with all the parameters
        Then Verify the "201" Status Code of Add Item
        Given I send the "GET" request with the "AddItemInCart" endpoint 
        And With the Cart Query Parameter
        When Execute the Get Item request with all the parameters
        Then Verify the "200" Status Code for Get Item
        And Verify the Productid should match with the POST request productid
        
    Scenario: Update Item in Cart API 
        Given I send the "POST" request with the "AddItemInCart" endpoint 
        And With the Cart Query Parameter
        And With the Cart valid JsonBody
        And Execute the Add Item request with all the parameters
        When I send the "PUT" request with the "UpdateCart" endpoint 
        And With the Cart Query Parameter
        And With the Item Query Parameter
        And With the Cart valid JsonBody
        And Execute the Put Item request with all the parameters
        Then Verify the "204" Status Code for Update Item
        Given I send the "GET" request with the "AddItemInCart" endpoint 
        And With the Cart Query Parameter
        When Execute the Get Item request with all the parameters
        Then Verify the "200" Status Code for Get Item
        And Verify the Productid should match with the POST request productid 