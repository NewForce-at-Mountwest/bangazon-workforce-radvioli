# bangazon-workforce-radvioli

```BANGAZON WORKFORCE MANAGEMENT SITE```

Bangazon Workforce Management Site is an internal application built for Human Resources and the IT Department. This application allows a user to create, list, and view Employees, Training Programs, Departments, and Computers.

``DATABASE SETUP``

If you have not already set up a database for this, you'll need to do so. To do this, open SSMS (Microsoft SQL Server Management Studio) and click the option to create a new database. Name your database Ravioli and begin the process of setting this up. You can find all of the information you'll need here:
https://gist.github.com/nikx1015/cb23e425bc71e489ef4017794537ea9a

You can copy the code beginning with CREATE Table and ending before the ALTER table.
Once you run this, your initial tables and columns should be set up and you can move on. Perform a new query with only the 3 lines following the ALTER command. Run this and then copy the code beneath that and post it into the query. Once you run that query, check to make sure your database is seeded properly before moving on.

``CLONING THE GITHUB PROJECT``

Next, you'll need to clone the files from Github and launch Visual Studio. Open the .sln file you just created.

```INSTRUCTIONS FOR USING SQL SERVER```

-EDIT THE APPSETTINGS FILE-

Go to your appsettings.json file and replace the code inside with this: { "Logging": { "LogLevel": { "Default": "Warning" } }, "AllowedHosts": "*", "ConnectionStrings": { "DefaultConnection": "Server=localhost\SQLEXPRESS;Database=Ravioli;Trusted_Connection=True;" } }

```DEPENDENCIES```

You'll want to install the SQL package to allow you to access the SQL server:

cd BangazonAPI dotnet add package System.Data.SqlClient dotnet restore

```FEATURES```

This app is a work in progress and is not yet completed. Currently, you may do the following things:

``EMPLOYEES``

The Employee feature of the app is functioning. You may view all of the employees, view a single employee with that employees details, create a new employee, and edit an employees first name, last name, and department. 

To use these features, use the ctrl+F5 command to launch the app. Once it's loaded, you may click on the Employee affordance to be taken to the Employee section of the app. You should see a list of current employees with an option on each to view their details. You should also see an affordance for creating a new employee. 

To create a new employee, click the affordance that says create new employee. You'll be redirected to a form where you may enter an employees information. Once you're finished, click submit and you should be redirected back to the employee list. Scroll down and your newly added employee should be there.

To view an employees details, choose an employee from the employee list and click the details link beside their name. You'll be redirected to a new page with that employees information.

``DEPARTMENTS``

The Department feature of the app is functioning as well. You may view all departments or view a single department and the details of that department.

To use the features, use the ctrl+F5 command in visual studio to launch the app. Once it's loaded, you may click on the department affordance to be taken to the department section of the app. You should see a list of departments with an option to view the details of each department. You should also see an affordance to create a new department.

To create a new department, click on the affordance that says create new department. You'll be redirected to a form where you may enter a departments information. Once you've finished, click submit and you should be redirected back to the list of departments. If you scroll to the bottom of this list, you should see the department you created.

To view a departments details, choose a department from the department list and click the details link beside the name of the department. You'll be redirected to a new page with that departments information.

```FEATURES IN DEVELOPMENT```

``COMPUTERS``

The Computer feature of the app is currently in development and will allow you to see all computers, create a computer, or delete a computer from the database, assuming it's never been used by an employee.

Once this feature is implemented, it will work similarly to the features listed above.

``TRAINING PROGRAM``

The Training Program feature of this app is in development. This will allow you to see all training programs, see a single training program with the details of the program, edit a program, or delete a program. 

Once this feature is implemented, it should work similarly to the features listed above.

```TECH USED```

C#, SQL

```COLLABORATORS```

This app was built in collaboration with Matt Rowe, Nikki Ash, Dale Saul, and Russell Miller.
bangazon-workforce-radvioli created by GitHub Classroom
