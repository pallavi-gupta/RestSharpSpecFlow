Feature: BasicOperation
Covers basic API Test operations

        
  #  @smoke
  #  Scenario: GET products
  #      Given I perform a GET operation of "Product/GetProductById/{id}" 
  #        | ProductId |
   #       | 1         |
    #    And I should get the product name as "Keyboard"
        
    
    
    Scenario: Create Cart
        Given I hit the "CreateCart" endpoint
        And With the Query Parameter And JsonBody
        Then Verify User should get the 201 response code
        And CardID should not be Blank
    
   # Scenario: Add Item in Cart
    #    Given I hit the "AddItemInCart" endpoint
     #   Then Verify User should get the 201 response code
      #  And CardID should not be Blank
        
      
        

        
   