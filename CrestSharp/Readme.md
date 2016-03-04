# CrestSharp - a .NET CREST library
This library provides a user friendly wrapper around CCPs CREST API for the EVE Online space MMO game.
It was created during the CREST API challenge in February 2016. Due to the short timeframe of that challenge it does not support all of CREST yet.
I consider the existing interface already more or less stable, so you can start to use it, if you want to. I will probably change some of the implementation for better performance and maintainability after the freeze of the challenge is over.

### API design
The API of CrestSharp was designed with the following intentations
* Ease of use and natural representation of CREST in a .NET / object oriented world
* Testability of client applications (through using an interface based design in the library)
* Support for asynchronicity through TPL

### Usage of public CREST
Using CrestSharp is really simple. You just reference the dll in your project (NuGet package will be created, once it is finished) and create a Crest object like

```cs
//set the user agent to your application (warning: this will move to Crest.Settings after the freeze), e.g. 
Crest.UserAgent = $"EVE Fast Fitting Assessment (v{Assembly.GetExecutingAssembly() .GetName() .Version})";

//intantiate a Crest object
ICrest crest = new Crest();

//use crest object
var publishedSkillCount = crest.Inventory.GetCategories()
                .First(category => category.Name == "Skill")
                .Groups.Where(@group => @group.Published)
                .SelectMany(@group => @group.Types)
                .Count(type => type.Published);
```

As you can see, the actual CREST calls (or cache hits) are not visible to the user, be he just traverses an object graph.
One problem with this is, when you access large amounts of objects like in the example above, it will take a long time.
You can speed up the process, by parallelizing the HTTP requests where possible:

```cs
//tell .NET to allow more than 2 parallel open web requests
System.Net.ServicePointManager.DefaultConnectionLimit = 10;

//parallelize the queries
var publishedSkillCount = crest.Inventory.GetCategories()
                .First(category => category.Name == "Skill")
                .Groups.Where(@group => @group.Published).AsParallel() // optionally increase parallelism through .WithDegreeOfParallelism(10)
                .SelectMany(@group => @group.Types)
                .Count(type => type.Published);
```


### Usage of authenticated CREST
You can also access authenticated CREST methods, once your user authenticates itself.
You only have to register your application online first under https://developers.eveonline.com/applications
The registration process creates a client id and secret for you
```
ICrest crest = new Crest();
var authedCrest =  await
                    crest.Authenticate(
                                       CLIENT_ID, // client id of your application
                                       CLIENT_SECRET, // client secret of your application
                                       new Uri(URL), // local callback URL of your application (selected on application registration), e.g. http://localhost:54321
                                       AuthenticatedCrestScope.CharacterFittingsRead | AuthenticatedCrestScope.CharacterFittingsWrite //scopes your application needs
                                       | AuthenticatedCrestScope.CharacterLocationRead | AuthenticatedCrestScope.CharacterNavigationWrite);
									   
//use authed crest
await authedCrest.SetWaypointAsync(system);

//authenticated crest contains a refresh token which you can store and use later, so the user doesn't need to authorize your application again
var refreshToken = authedCrest.CrestAuthorization.RefreshToken;

//login with refresh token
var authedCrest = await crest.Authenticate(CLIENT_ID, CLIENT_SECRET, refreshToken);
```

### Known limitations
As said, CrestSharp still isn't fully ready for prime time, because it was created as a tool in the CREST API challenge.
There are a some endpoints not yet supported and also the implementation isn't what I would like it to be in some cases.
It's also blatantly missing API documentation and larger test coverage.

If you have questions, please feel free to contact me at jonas.jacobi@web.de