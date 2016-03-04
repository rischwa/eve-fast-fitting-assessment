# EVE Fast Fitting Assessment
EVE Fast Fitting Assessment (FFA) is a Windows desktop appliction to quickly assess the stats of fittings found on killmails.
It was created as my entry to the CREST API challenge of CCP in February 2016 on which I spent way too much time ;).
EVE Fast Fitting Assessment was designed to be useful for EVE players and at the same time also provide insight into a multitude of CREST endpoints for (future) EVE 3rd party developers.

This readme is divided into three sections.

* Download/installations instructions
* A user manual, describing what the application does and how to use it
* A technical description for the code behind it, targeted at developers, who want to create CREST applications themselves

## Download
To run FFA, you can either
* Download a click once [installer](http://eve-plh.com/FFA/setup/setup.exe), 
* or if that doesn't work (in my experience ClickOnce installers tend to have problems for a small number of users), you can just download the a [zip file containing the application](http://eve-plh.com/FFA/eveffa.zip) and start the .exe from there.

## User Manual

The user manual first describes a small use case, so you have an idea, when/if you want to use this program and then describes its functions a bit more in depth.

### Use Case

Imagine you are solo hunting in faction warfare with a scram kiter and see a Merlin on DScan in a small plex. You check the local (e.g. with Pirate's Little Helper ;)) and are pretty sure which character is in there.

You go check his killboard and find his last losses of Merlins. Now you'd like to know if you are able to scram kite him, if he uses the same fit, or if he is either too fast or his weapons have too good range/tracking for you.
So you copy the crest killmail link into your clipboard and paste it into EVE Fast Fitting Assessment. You can immediately see he could hit out to 6km with Null ammo loaded into his Light Neutron Blaster IIs while being able to run 1179m/s with his overheated afterburner.

He is not fast enough  for your, so you preheat your AB, set your orbit distance to 7km and enter the plex...
The Griffin decloaking right as you enter is not part of this story ;).

### Functional description

Here is a more in depth look into the capabilities of EVE Fast Fitting Assessment.
When you startup FFA you will be greated by this splashscreen:

![FFA Splashscreen](http://eve-plh.com/FFA/splashscreen.gif "FFA Splashcreen")

The first startup will take quite a while (probably over a minute), because a lot of requests to CCPs CREST API have to be done to initialize the fitting engine.
But fortunately the results get cached and the next startups will be much faster.

Once the application runs, you get a very tidy interface providing you with a text box for killmail links and a button to log into EVE. You can paste any crest killmail link into the textbox and the application will recognize it, load the killmail and calculate some of the most important PVP stats of the fit and also its costs in the major tradehubs.

![FFA in action](http://eve-plh.com/FFA/fittinganalysis.gif "FFA in action")

The fitting analysis on the bottom shows you multiple useful information:
* The EHP for omni damage and for the single damage types, to immediately see resist holes
* The (overheated) active tank (combining hull/armor and shield reps and also passive shield recharge)
* The maximal alpha damage and DPS of the fit
* The (overheated) weapon stats with the most commonly used ammo types and Gallente drones
* The base speed, speed with propulsion module and with overheated propulsion module

![FFA fitting stats](http://eve-plh.com/FFA/fitting.png "FFA fitting stats")

#### Log in with EVE Online
If you like the fit, you can log into EVE on CCPs site and give FFA the rights to

* write fits to your in game fittings
* set your autopilot destination

If you do that, you can save any fit in FFA to your character, if you want to and also set your autopilot to a trade hub of your choice, if you want to buy the fit immediately to try it out.

Once you click onte the login button, your browser will open the Single Sign On (SSO) page of CCP (this is in your browser and FFA can't see anything of it, your credentials remain completely secure. When you login in, you are asked to grant FFA the rights to read/write your fits and also read your location and write your autopilot target.

Here you can see how CCPs authorization page will look like:
![CCP Auhtorization Page](http://eve-plh.com/FFA/authorize.png "Authorize FFA in CCPs SSO system")

And this is how FFA looks, after you log in:

![Login with EVE Online](http://eve-plh.com/FFA/login.gif "Login with EVE Online")

On the right side you'll see your character portrait and if you are logged in your current location (updated every 10s).
Also two new buttons appear on the right, one to save the current fit (only shown, if you currently have a fit) and a logout button on the bottom. You also see white location markers in the prices box. Through pressing them you can instantly set your autopilot destination to the respective trade hup, if you want to try a fit immediately and need to buy it.

FFA will keep the rights you granted even after restarts, so you only have to login once, until you either
* press the logout button
* or remove its rights in CCPs third party application management (might be buggy on CCPs part, at least when I tested it not all applications showed up) https://community.eveonline.com/support/third-party-applications/ .

#### Known Limitations
The current state of the application makes it useful in most circumstances where you want to quickly assess the abilities of a fit out of killmail. But, due to the short timeframe of 17 days for the challenge and the tremendous task of developing a completely new fitting engine based on crest type and dogma info, there are some things not supported yet. They will be implemented once the freeze for the challenge is over. Mainly:
* Doomsdays, bombs, torpedos and cruise missiles are not supported in the damage analysis
* Mode switching is not getting considered in T3D analysis
* Speed analysis of dual prop fits is not working

And there are, of course a lot of smaller/larger extensions which would make the application even more useful, e.g. loading of own fits and showing some more in depth details about the fit which might also be interesting. Those will also have to wait until the freeze is over, unfortunately.

## Developer Information
EVE Fast Fitting Assessment was created with the intention of bringing a multitude of public and authenticated CREST endpoints togehter, to give other developers a good starting point when they want to start with their own CREST powered applications.
Also to learn more about CREST myself ;).

The application is roughly divided into three parts:
* A new .NET CREST library ([CrestSharp](https://github.com/rischwa/eve-fast-fitting-assessment/tree/master/CrestSharp)) focused on ease of use for its users. It supports transparent caching and lazy loading, so the user doesn't really "feel" what happens behind the scenes, while also providing the means for finer grained control, if wanted. You can find more info on CrestSharp in its own Readme in this repository.
* A new [.NET fitting engine](https://github.com/rischwa/eve-fast-fitting-assessment/tree/master/FittingEngine) build on top of CRESTs public type and dogma system. You can find more information on it in its Readme in the FittingEngine folder.
* The [.NET/WPF GUI](https://github.com/rischwa/eve-fast-fitting-assessment/tree/master/EVE%20Fast%20Fitting%20Assessment) and orchestration of the parts to provide the FFA application as a whole.

If you have any questions, please feel free to contact me under jonas.jacobi@web.de or Rischwa Amatin in game.

### CREST Endpoints used in FFA
If you wan to start with CREST, you can take a look into this application. It utilizes the following CREST endpoints:
* solarSystem for locations
* market for fitting price calculation
* inventory/types/dogma for the fitting engine and general type information
* authenticated CREST on the character
  * general character information for name / id / image
  * characterFittingsWrite for writing fittings
  * characterLocationRead for reading the characters current location
  * characterNavigationWrite for setting the autopilot target
  
### Building FFA yourself
You can check out this repository and open it with Visual Studio 2015.
Before you can run it, you need to register your own application on https://developers.eveonline.com/applications , because I cannot provide my own credentials publically, of course.
If you register it, you have to set the callback URL to http://localhost:49871/crestAuth/ and request the following scopes:
* characterFittingsRead
* characterFittingsWrite
* characterLocationRead
* characterNavigationWrite

Once you have done that, you'll get a client id and client secret, which you must set in `EVE Fast Fitting Assessment\App.xaml.cs` on the top.
