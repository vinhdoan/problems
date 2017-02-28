-module(foo).
-export([out/1]).
-include("yaws_api.hrl").
out(Arg) ->
    {ehtml,
     [{html, [],
       [{body, [],
	 [{h1, [], "Appmod Data"},
	  {p, [],
	   yaws_api:f("appmoddata = ~s",
		      [Arg#arg.appmoddata])},
	  {p, [],
	   yaws_api:f("appmod_prepath = ~s",
		      [Arg#arg.appmod_prepath])}]}]}]}.
