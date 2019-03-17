# sitekitBmi
As a first step to run this test please restore the db bak file so that you have the desired db schema.

Unfortunately I was not able to get the Azure Ad integration to work as desired.  Using the AzureAd and OpenIdConnect middleware I kept getting prompted that the user gooduser has had either password set incorrectly and therefore has been blocked.  I think this is either due to the tenant being provided incorrectly via the middleware or another missing configuration as I could log in and get a token when running from the browser.  If I had managed to log in correctly accessing the user information is a fairly simple task of accessing the logged in user object via system.Web.HttpContext.Current.User and mapping the claim information from the token.

For larger scale files I would look at implementing a caching layer such as redis or DynamoDb for the storage and DAX for the caching element.  However this would depend on how often the files are going to get written, and how concurrent.  I would also potentially move the aggregation/reporting logic out to another system to take the load off the import process - again this would depend on how often the files are getting uploaded.

For error handling I implemented a global exception handler, in addition to that I would plug in a logging middleware to write to a db of any exceptions - initially I just wrote to the console, however .net core has made swapping out loggers very easy through the ILogger interfaces.
