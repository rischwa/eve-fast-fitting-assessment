#.NET Fitting Engine for EVE Online
This is a .NET fitting engine for CCPs space MMO EVE Online.
It was created for the CREST API challenge in February 2016 and is build on top of CRESTs type and dogma information.

As CREST does not support dogma expressions yet, the engine uses a small sqlite expression database in addition to the CREST information.

Due to the short timeframe of the challenge and the comparatively HUGE task a new fitting is, the interface of the library is build to feed the EVE Fast Fitting Assessment application and not really for general consumption.
Because of this I won't write more about it here at this point in time.

I will create a new interface and refactor the implementation (although the actual implementation is already okayish), after the freeze for the challenge is over.
I therefore cannot recommend to use it yourself yet, only if you want to take a look into how dogma / fitting engines work internally.
