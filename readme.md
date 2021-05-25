# Blog Solution
This is a experiment solution created in .Net 5 framework. 
The purpose of the solution is to setup a solution from scratch using .Net 5 framework, EntityFrameworkCore 5 for backend and NuxtJS for the frontend.


## How to run the project
1. Install all the project dependencies by running below command at the solution root path. 
```
    dotnet restore
```

2. Update the `appsettings.json`, `appsettings.Development.json`, and `appsettings.Test.json` files in corresponding projects for the database configuration. Also create the corresponding schema in the database. Then navigate into DatabaseMigration project and execute below command to migrate the database.
```
    dotnet ef database udpate
```

3. Navigate to the `Web` project, `client_app` directory, then run below command for installing Fronted dependency.
```
    npm install 
```

4. Now, you are ready to run the Web project. 

Note:  
* In development mode, by running the Web project, it should also monitor changes in the `client_app` directory and perform hot reload.


