## User/Password

longtduc@gmail.com/abcd1234, administrator
diemtduc@gmail.com/abcd1234

## Ninject is used for Web API
## Castle.Windsor ASP.NET MVC bootstrapping package used for MVC
------------------------------------------------

This package simplifies bootstrapping of `Castle.Windsor` container in your ASP.NET MVC 4 application. 
To install this package, use following NuGet command:

    PM> Install-Package Castle.Windsor.Web.Mvc

Package contains `WindsorControllerFactory`, `ControllersInstaller` and `WindsorActivator` classes for your MVC application.

All controllers are now resolved with controller factory. To register your custom components 
just [create another Installer class](http://docs.castleproject.org/Windsor.Installers.ashx)
implementing `IWindsorInstaller` interface.

Fo more informations [how to use Windsor](http://docs.castleproject.org/Windsor.MainPage.ashx) see the
[documentation](http://docs.castleproject.org/Windsor.MainPage.ashx).

[![endorse](https://api.coderwall.com/rarous/endorsecount.png)](https://coderwall.com/rarous)

# How to use web.config customErrors in ASP.NET MVC?
- home/throwException
- https://www.tutorialsteacher.com/articles/web-config-customerrors-in-aspnet-mvc


# Filter types in MVC
- Authentication filters (MVC 5)
- Authorization filters
- Action filters
- Result filters
- Exception filters

## when to use filter

- Custom Authentication
- Custom Authorization(User based or Role based)
- Error handling or logging
- User Activity Logging
- Data Caching
- Data Compression

## Predefined filter
- Authorize
- ValidateInput
- HandleError
- RequireHttps
- OutputCache, etc

## Three level of filter
- Global level (FilterConfig.cs) 
- Controller level
- Action level

## How to create a custom filter in ASP.NET MVC?
- https://www.tutorialsteacher.com/articles/create-custom-filters
- Home/NullReferenceException and see D:\SourceCodes\ShareHolderMeeting\ShareHolderMeeting.Web\Log\Log.txt
- LogCustomExceptionFilter.cs => FilterConfig.cs (Log/Log.txt)

## How to use async, await in MVC

- Home/GetLocation
+ use Thread.Sleep(3000);          
+ ~ 3000ms 
- Home/GetLocationAsyn 
+ use await Task.Delay(1000);
+ took only ~ 2000ms

- SingleOrDefaultAsync(): Get a single object as a result
- FindAsync(): Asynchronously finds the entity with the given primary key value.
- SaveChangesAsync(): Asynchronously save all changes..
- ToListAsync()
## How to log unhandled exception to database 
- Added LogException table and modified LogCustomExceptionFilter.cs
- Migration:
+ Moved to Infrastructure project
+ EntityFramework version 4.4 can not migrate to the new model => Degraded to version 6.1.2
+ Removed folder Migrations from ShareHolderMeeting.Web project


