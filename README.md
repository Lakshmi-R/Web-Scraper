# Web-Scraper
This tool gets the keyword and finds the position of the given url.

This application was built using .Net 8 ,used Dependency Injection, IhttpClientFactory to make a http call , EF core 9 version uses repository pattern, front end uses Angular 19.

# Project has the following modules.
Scraper.API
Scraper.Core
Scraper.Common
Scraper.Repository

# Setting up API:
Connection string used to create and connect database
"Server=(local)\\SQLEXPRESS;Database=Scraper;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
Database migration can be found scraper.Repository
use update-database command in package manage console to create and get access to database.

# Setting up SPA 
Scraper.Client
Make sure node , npm installed, use ng s -o to run the client application.

Used Angular19, Reactive form which gets the input keyword and searchurl and maxcount and interact with web api to add results and bind search results to the table.

# Output
when the user clicks on the submit button it will return the currently searched keywords and previous searches from DB.
![image](https://github.com/user-attachments/assets/d9ce5e43-07d3-4a4b-9ac4-ead845183c69)
