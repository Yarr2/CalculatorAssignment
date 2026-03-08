What I want to do:
1. Calculator that calculates input, if it is correct
2. Ability to add your own functions using "=" for example f(x,y) = x^2 + y
3. Ability to save all command into special file, and retrieve them after restarting program("using NameOfRules")



How to implement it:
1. Create Stack and Queue data collections
2. Create Function and Operation classes, so we can add them later
3. Create Tokenizer, which will break down users input into tokens, like "1","2","max","("
4. Create Checker which checks correctness of given expression
5. Create PostFixConvertor which will output operations and operands in postfix form 
6. Create Calculator which calculates based on operations
