-module(websocket).
-compile(export_all).

-include("yaws_api.hrl").

-define(FIFO_SERVER, fifo).

out(Arg) ->
    case get_upgrade_header(Arg#arg.headers) of
	true ->
	    {content, "text/plain", "You're not a WS client!!!"};
	false ->
	    {websocket, ?MODULE, []}
    end.

get_upgrade_header(#headers{other = L}) ->
    lists:foldl(fun ({http_header, _, K0, _, _V}, undefined) ->
			K = case is_atom(K0) of
				true ->
				    atom_to_list(K0);
				false ->
				    K0
			    end,
			case string:to_lower(K) of
			    "upgrade" ->
				true;
			    _ ->
				false
			end;
		    (_, Acc) ->
			Acc
		end, undefined, L).

handle_message({text, Message}) ->
    %% gen_server:cast(fifo, {pid4u, self()}),
    io:format("~p:~p Got ~p from one of our clients~n",
	      [?MODULE, ?LINE, Message]),
    StrMsg = binary_to_list(Message),
    [Type, Text] = string:tokens(StrMsg, ":"),
    StrReply = string:join([Type, string:to_upper(Text)], ":"),
    BinReply = list_to_binary(StrReply),
    {reply, {text, <<BinReply/binary>>}}.
    %% Format to reply:
    %% {reply, {text, <<"{\"message\":\"text\"}">>}}.

send2client(Pid, Data) ->
    io:format("Calling websocket:send2client~n", []),
    Type = "\"message\"",
    Text = "\"" ++ string:strip(Data, right, $\n) ++ "\"",
    StrSend = "{" ++ string:join([Type, Text], ":") ++ "}",
    BinSend = list_to_binary(StrSend),
    yaws_api:websocket_send(Pid, {text, <<BinSend/binary>>}).

init(_Args) ->
    UserId = gen_server:call(?FIFO_SERVER, get_user_id),
    gen_server:cast(?FIFO_SERVER, {connection_open, {self(), UserId}}),
    WelcomeText = "Hi! Your ID is " ++ UserId,
    timer:apply_after(3000, ?MODULE, send2client,
		      [self(), WelcomeText]),
    {ok, []}.

terminate(_Reason, _State) ->
    gen_server:cast(?FIFO_SERVER, {connection_close, self()}).
