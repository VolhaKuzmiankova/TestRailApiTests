# TestRailApiTests

Framework for the following endpoints:
1.	POST index.php?/api/v2/add_project
2.	GET index.php?/api/v2/get_project/:project_id
3.	POST index.php?/api/v2/delete_project/:project_id
4.	POST index.php?/api/v2/add_suite/:project_id
5.	POST index.php?/api/v2/update_suite/:suite_id

**Prerequirements:**
1. dotnet 3.1
2. allure

**To see a result, follow the next steps:**
1. ```dotnet test```
2. ```allure serve TestRailApiTestFramework\bin\Debug\netcoreapp3.1\allure-results```
