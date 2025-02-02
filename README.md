### Project Description
A simple web api project created with .net web api template

### Create a new dotnet web api project
```
dotnet new webapi -n to-do-list-api
```

### Adding to source code
```
git init
git add .
git commit -m "first commit"
git branch -M main
git remote add origin https://github.com/Parbin29/to-do-list-api.git
git push -u origin main
```

### Install package for swagger
```
dotnet add package Swashbuckle.AspNetCore
```

### Configure SQLServer with ef core 
Add the following packages from nuget:
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.EntityFrameworkCore.Design

### Add the following packages from Newtonsoft.Json to get the correct nested entities in response json
```
dotnet add package Newtonsoft.Json
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
```

- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.EntityFrameworkCore.Design

### install ef core
```
dotnet tool install --global dotnet-ef
```

### Run migrations
```
dotnet ef migrations add init
dotnet ef database update
```

### Update migrations
```
dotnet ef migrations add add_relations
dotnet ef database update
```

### To Remove migrations 
```
ef migrations remove
```

### Run in watch mode
```
dotnet watch run
```

### To test the endpoints navigate to 

[Swagger](http://localhost:5293/swagger/index.html)

