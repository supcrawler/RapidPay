Usage:
------
* Clone or download the Repo.
* I've transfered all hardcoded stuff to a file named ***Constants.cs***. So, if you want to change any parameter or configuration go there and set for example the UFE schedule to 5 seconds so you can see it in action without any absurd delay on testing side.
* Build Solution
* Run Migrations:

     a)running **Update-Database**  from Visual Studio Package Manager Console or
     
     b)running **dotnet ef database update** command on a console prompt under your project's directory
     
         Note: it requires to have **dotnet** previously installed

* Run Solution
* Hit the following endpoints using postman or any other REST client

Endpoints:
----------
Create:  	POST:  https://localhost:7124/CreditCard/Create				// Sample Body Payload: { "CardNumber" : "123456789012345", "Balance": 1000 }

Pay:   	  	POST:  https://localhost:7124/CreditCard/Pay				// Sample Body Payload: { "CardNumber" : "123456789012345", "Amount": 10.24 }

GetBalance: GET:   https://localhost:7124/CreditCard/{cardNumber}


User: Jose

Pass: Enser

