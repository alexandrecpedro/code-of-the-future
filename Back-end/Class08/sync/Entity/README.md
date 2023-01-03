# ENTITY FRAMEWORK COMMANDS

<table>
    <tr>
        <th>#</th>
        <th>Script</th>
    </tr>
    <tr>
        <td>Documentation</td>
        <td>https://learn.microsoft.com/pt-br/ef/core/cli/dotnet</td>
    </tr>
    <tr>
        <td>Add Package - EntityFramework Tools</td>
        <td>https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools</td>
    </tr>
    <tr>
        <td>Install</td>
        <td>dotnet tool install --global dotnet-ef</td>
    </tr>
    <tr>
        <td>New Migration</td>
        <td>dotnet ef migrations add <MyMigration_Name> </td>
    </tr>
    <tr>
        <td>Update Database</td>
        <td>dotnet ef database update <MyMigration_Name?> </td>
    </tr>
<table>

# SCAFFOLD
<table>
    <tr>
        <th>#</th>
        <th>Script</th>
    </tr>
    <tr>
        <td>Add Package - Web CodeGeneration Design</td>
        <td>https://www.nuget.org/packages/Microsoft.VisualStudio.Web.CodeGeneration.Design</td>
    </tr>
    <tr>
        <td>Add Package - EntityFramework SqlServer</td>
        <td>https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer</td>
    </tr>
    <tr>
        <td>Install</td>
        <td>dotnet tool install -g dotnet-aspnet-codegenerator</td>
    </tr>
    <tr>
        <td>New Controller</td>
        <td>dotnet aspnet-codegenerator controller -name ClientesController -m Cliente -dc DbContexto --relativeFolderPath Controllers --useDefaultLayout</td>
    </tr>
<table>
