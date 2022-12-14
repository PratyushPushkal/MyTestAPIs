# User API Dev Guide
	List of IDE and tools to use this application
	* Dot Net Core 3.1
	* IDE VS Code/ VS 2019
	* Docker Desktop with Azure data studio or SQL Management studio with MSSQL Server
## Building
	Do Follow below steps to run and Build application
	* Provide Correct connection string. If you selecting docker for MSSQL Instance then follow below steps
		* Install Docker Desktop
		* Open Power Shell to Docker-Compose.yaml file location
		* Run command 'docker-compose up'
		* Once this command Executed then Image will be created on Docker
		* To Open SQL Database in Power Shell
			* docker exec -it solutions-mssql-1 "bash"
			* /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P YourStrong@Passw0rd
			* select name from sys.databases
		* To Check data on docker install Azure Data s and connect with providing Connetion string
		* Provide connection string as "Server=localhost,1433; Database=TestProjectDB; User= SA; Password=YourStrong@Passw0rd;"
	* If Selecting Local MSSQL Instance then provide local db connection string.
	* Add Docker Plugin as Solution Explorer right click, Add=>Docker Support
	* For EF Core Migration do execute below command
			* add-Migration Migation1
			* update-databases
## Testing
	* xUnit API Test Cases written under TestCases=>Controller, Open Test Explorer and do run
	* Service Test cases written under TestCases=>Service

## Deploying
	* To Deploy, Select Solution and use publish option, which will give multiple option to publish.
## API and Model
	Model:
		User: This model will hold information Like Name,Email,Salary and Expenses
			* Email will be unique for users
			* If Salary - Expenses will be more than 1000 then only it will be store in databases.
		Account: This Model will hold information like Email,Password,IsActive
			* Password will be store as hash value do hide information.
			* Email should be available unique and user should be available in User list
			* IsActive is decide either Account is active or not.
	API Created:
	 UserController: 
		ListUsers: This API will list all users available in system..
		CreateUser : This API will make able to create new user.
		GetUser : This API will fetch a Single User from system by its user id.
	 AccountController:
		* ListAccount: This API will list all user accounts.
		* GetAccountById: To Fetch Account information by AccountID, it will fetch Account information of single Account. 
		* CreateAccount: Once User exist this will enable user to create Account, where inform required like Email and Password
		* Login: This API will Login user once its A got create.
		* UpdatePassword: This API will allow user to change password
		* DeleteAccountById: This API will allow to delete Account
## Test Case Handled
	User:
		* API and Service for UserList done
		* API and Service for AccountList done
	Test Cases for what it handle for;
		* Users_ReturnCollection :  To Check User List
		* Users_Create : To Create new User
		* Users_CreateForExistingEmail : To Check,If Email already exist then it will throw error.
		* Users_CreateSalaryExpensesDiffLessThan1000 :  To Check Salary - Expensesmore than 1000$.
		* Users_ReturnsSingleUserFound :  To Check, Either User exist or not by its Id.
		* Users_ReturnsSingleUserNotFound : To Check, If User not present in list.
		
		* Accounts_ReturnCollection: To Check Account List
		* Accounts_Create : To Check Account Creation
		* Accounts_CreateUserNotAvailableForGivenEmail : To Check,Either User having with Email id in UserList.
		* Accounts_CreateEmailAlreadyExistWithOtherUser : To Check, If Email provided used by different User in Userlist.
		* Accounts_DeleteFound : To Check, Deletion of Account if its exist.
		* Accounts_DeleteNotFound : To Check, Deletion of Accountif its not exist;
		* Accounts_ReturnSingleAccountFound : To Get Account by its Id, if its found.
		* Accounts_ReturnSingleAccountNotFound :  To Get Account by its id, if its not found.
		* Accounts_UpdatePasswordFound: To Check, To Update password if email id found.
		* Accounts_UpdatePasswordNotFound : To Check, To Update password if email id not found.
		* Accounts_LoginSuccessful: To Check, If Email and Password match with existing list.
		* Accounts_LoginUnSuccessful : To Check, If password not matching with existing list for given Email login.
		* Accounts_LoginWrongEmail : To Check, If Email Id not present in Account List.
		
## Additional Information