Feature: Create folder
 
Calculator for adding two numbers
 
@mytag
Scenario: Add two numbers
Add two numbers with the calculator
Given I have entered <First> into the calculator
And I also have entered <Second> into the calculator
When I press add
Then the result should be <Result> on the screen
Examples:
| First | Second | Result |
| 50    | 70     | 120    |
| 30    | 40     | 70     |
| 60    | 30     | 90     |