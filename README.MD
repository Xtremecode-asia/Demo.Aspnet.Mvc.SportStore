Demo.Aspnet.Mvc.Management.Tenants
===================================

A simple web application to demonstrate MVC web pattern using ASP .NET MVC 4, dependency injection using Castle Windsor & Behaviour Driven Development using Specflow.

Forged background story, behind the app:
----------------------------------------

A new startup company 'T1.PR' has launched their first SaaS app 'T1.PR.Saas' on the market and have gained 10 tenants since a week ago. However, they still don't have an application that can be used to manage their Tenants in their backoffice.
They needs such this application to do these following tasks:
	- Looking at a list of all Tenants records
	- Looking at a Tenant's detail record.
	- Search / Filtering a list of all Tenants records.
	- 


Things to do before running the sample:
---------------------------------------
Install these following softwares in your machine:
  - JDK 7 with latest updates version.
  - Git client
  - Intellij IDEA 11.x-12.x (Optional)

HOW TO - Pull the project from github to your local machine:
------------------------------------------------------------
1. Open the git bash console.
2. Change directory to a folder where you are going to put the source code in.
3. Type &amp; run this command: git clone git@github.com:Xtremecode-asia/Demo.SpringAop.git

HOW TO - Run the unit test in Intellij IDEA:
--------------------------------------------
1. Run your intellij IDEA.
2. Open the project.
3. Click "Run" -> "Edit Configuration" menu.
4. On the "Run/Debug Configurations", click greenish '+' small icon and click "Junit".
5. On the Junit section panel, enter the Name field with any string (e.g. demo.springaop.spec), Package: demo.springaop.spec, Search for tests: In whole project.
6. Click [Apply] and then [Ok].
7. Run the unit test through pressing [CTL]+[F5] or click "Run"->"Run '<your Junit run configuration name>'".

HOW TO - Run the console client in Intellij IDEA:
-------------------------------------------------
1. Run your intellij IDEA.
2. Open the project.
3. Click "Run" -> "Edit Configuration" menu.
4. On the "Run/Debug Configurations", click greenish '+' small icon and click "Application"
5. On the Application section panel, enter the Name field with any string (e.g. demo.springaop.console), Main class: demo.springaop.ConsoleClient.
6. Click [Apply] and then [Ok].
7. Run the unit test through pressing [CTL]+[F5] or click "Run"->"Run '<your Application run configuration name>'".

The anatomy of Demo.SpringAop project:
---------------------------------------------------------
1. lib folder - contains all required dependencies to run the samples.
2. resources folder - contains the Spring's configuration xml files and Jbehave's story files.
3. spec folder - contains java files which built the unit test demo.
4. src folder - contains java files which built the console demo & Digest service's class + interface.

HOW TO - Confirm that the dependency injection & Aspect are working:
-------------------------------------------------------------------
1. When running the Console demo, confirm that the output does not return "Digest service is unavailable.", instead it should return the output of invoking the Digest Service.
2. Confirm that a log message "INFO: Start calling 'demo.springaop.service.IDigestService.digest' method at ..." &amp; 
   "INFO: Finished calling 'demo.springaop.service.IDigestService.digest' method at ..." are displayed around the output.

Future TO-DOs:
--------------
1. Replace the console demo to JavaFX or Play 2.0 clients.

Changes History:
----------------
1. 18/12/2012:
    - Renamed the Aspect class to 'ServiceCallsLoggerAspect'.
    - Replaced @Around advice with @Before & @AfterReturning.
    - Added @AfterThrowing advice.
    - Added a new JBehave scenario to demonstrate the @AfterThrowing advice.
    - Enabled Console support in the console client so that when the client is ran under command line box, 
      it would ask the user to enter a plain string to digest.
    - Refactored Pointcut expressions by moved them into 'InfrastructureAspect' class, in a method annotated with @Pointcut.
    
References:
-----------
1. "Chapter 6. Aspect Oriented Programming with Spring" -> http://static.springsource.org/spring/docs/2.5.5/reference/aop.html
