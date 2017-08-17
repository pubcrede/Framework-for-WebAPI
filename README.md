# Genesys Source Framework for ASP.NET Web API
Genesys Source Framework for ASP.NET Web API is a pre-setup full-stack .NET Solution to house your reusable business objects, such as CustomerInfo.
 Works with your existing SQL tables, and allows you to incrementally upgrade your custom software app...one page at a time.

Bringing reusability to your software stack without the cost and uncertainty.

Projects:
* Framework.WebServices: Web API 5.2.3, C#, .NET 4.6 
* Framework.Models: Portable Class Library (Win, iOS, Android), C#, .NET 4.6, Profile 151 
* Framework.Interfaces: Portable Class Library (Win, iOS, Android), C#, .NET 4.6, Profile 151 
* Framework.DataAccess: Entity Framework 6.1.3, C#, .NET 4.6
* Framework.Database: SQL Server Data Tools (SSDT), T-SQL, SQL Server 2016, .NET 4.6

Database:
* SQL Express database included: App_Data\FrameworkData.mdf
* SSDT publish to SQL Server dev environment: Framework.Database\Publish\PublishToDev.publish.xml
* SSDT publish to SQL Express local file: Framework.Database\Publish\PublishToLocal.publish.xml

Genesys Source Namespaces:
* Genesys.Framework: Structure and functionality Framework to support your reusable entities. Classes such as CrudEntity, EntityReader and EntityWriter.
* Genesys.Extensions: .NET Framework extension methods for null-safe, strongly-typed operations. Cross-platform, open-source common library for .NET.Standard and .NET Core (Universal, Portable).
* Genesys.Extras: .NET Framework-level classes for common tasks such as Http request/response, serialization, string manipulation, error logging, etc. Cross-platform, open-source common library.

### Reference Site and Documentation
Genesys Source Framework downloads and docs available at [GenesysFramework.com](http://www.GenesysFramework.com):
Genesys Source Framework...
* [Genesys Framework Overview](http://www.getframework.com/)
* [Genesys Framework Quick-Guide](http://docs.genesyssource.com/library/Genesys-Framework/Genesys-Framework-Quick-Guide.pdf)
* [Genesys Framework Requirements](http://docs.genesyssource.com/library/Genesys-Framework/Genesys-Framework-Requirements.pdf)
* [Genesys Framework Reference](http://docs.genesyssource.com/reference/Genesys-Framework)
Genesys Soruce Extensions...
* [Genesys Extensions Reference ](http://docs.genesyssource.com/reference/Genesys-Extensions)

### Dev Environment and Compiling
Please use the latest Visual Studio and build using the IDE or MSBuild.exe. Our CICD processes default to the latest Visual Studio and MSBuild versions.

### Database Environment and Publishing
Please use the latest SQL Server and/or SQL Expresss and publish using the SSDT project Framework.Database.
- SSDT publish to SQL Server dev environment: Framework.Database\Publish\PublishToDev.publish.xml
- SSDT publish to SQL Express local file: Framework.Database\Publish\PublishToLocal.publish.xml

### Hosting
- Cloud: Azure Web Server, Database Server and/or Virtual Machines.
- On-Prem: Latest Windows Server, IIS, .NET 4.5, SQL Server.

### Build and Release
- VisualStudio.com repos set to TFVC. On-prem TFS server and build agent for local infrastructure powershell deployments.
- Local NuGet feed for development cycles.
- Sprints pushed to GitHub on or about the 7th of each month.

### Distribution
Please use ClickOnce Deployments to distribute your app with a Url
* [ClickOnce Deployment](http://bit.ly/2tqrDIk)

### Git Repo
- [https://github.com/GenesysSource/Framework-for-WebAPI.git](https://github.com/GenesysSource/Framework-for-WebAPI.git)
- `git clone git@github.com:GenesysSource/Framework-for-WebAPI.git`