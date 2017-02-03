-module('access').
-compile(export_all).

-include("yaws_api.hrl").

validate_username_password(Arg) ->

	{ok, Username} = yaws_api:queryvar(Arg, "Username"),
	{ok, Password} = yaws_api:queryvar(Arg, "Password"),
	case {Username, Password} of
		{"Tuan", "Tran"} ->
			{true, success};
		{"Tuan", _} ->
			{false, bad_password};
		{_, _} ->
			{false, no_user}
	end.

out({false, Reason}, _Arg) ->
	io:format("~p:~p Unable to login user: ~p", [?MODULE, ?LINE, Reason]),
	{status, 401};
out({true, Uuid}, _Arg) ->
	io:format("~p:~p Welcome: ~p", [?MODULE, ?LINE, Uuid]),
	{html, "Hello World"}.

out(Arg) ->
	out(validate_username_password(Arg), Arg).