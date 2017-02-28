-module(guitar_lib).
-behavior(erlydtl_library).
-export([version/0, inventory/1]).
-export([get_price/2, frets/2]).

version() ->
    1.
inventory(filters) ->
    [frets];
inventory(tags) -> [get_price].

%% In the lack of a fret number, we can give defaults
%% depending on the guitar brand
frets(undefined, Brand) ->
    case Brand of
	<<"Schecter">> -> 24;
	<<"Jackson">> -> 24;
	_ -> 22
    end;
frets(FretNum, _Brand) ->
    FretNum.

get_price(Vars, _Opts) ->
    case lists:keyfind(id, 1, Vars) of
	{id, Id} ->
	    %% Let it crash if service fails
	    {ok, Price} = get_price_by_id(Id),
	    [{value, Price}];
	false ->
	    %% No id specified, we can crash or we can
	    %% leave the context variables as they are
	    []
    end.

get_price_by_id(Id) ->
    {ok, Id * 1000}.
