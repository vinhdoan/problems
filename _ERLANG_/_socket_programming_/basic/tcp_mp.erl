-module(tcp_mp).
-export([test/1]).

-define(S_IP, {127,0,0,1}).
-define(S1_PORT, 12345).
-define(S2_PORT, 23456).
-define(C_PORT, 34567).
-define(MSG, "A message").

test(N) ->
    ClientFun = fun(Port) -> client(N, Port) end,
    % Test server 1
    info(1),
    Server1Fun = fun() -> server1(N) end,
    Client1Fun = fun() -> ClientFun(?S1_PORT) end,
    spawn(Server1Fun),
    spawn(Client1Fun),
    timer:sleep(3000),
    % Test server 2
    info(2),
    Server2Fun = fun() -> server2(N) end,
    Client2Fun = fun() -> ClientFun(?S2_PORT) end,
    spawn(Server2Fun),
    spawn(Client2Fun),
    ok.

%---------------------------------------------------------------------
% Process of server 1 hands over control of ListenSocket to
% a new spawned process after receiving a TCP connection, then
% controls the AcceptSocket
%---------------------------------------------------------------------

server1(N) ->
    {ok, ListenSocket} = gen_tcp:listen(?S1_PORT, []),
    server1_receive(N, ListenSocket).

server1_receive(0, ListenSocket) ->
    gen_tcp:close(ListenSocket);
server1_receive(N, ListenSocket) ->
    {ok, AcceptSocket} = gen_tcp:accept(ListenSocket),
    NewListenProcFun =
	fun() ->
		server1_receive(N-1, ListenSocket)
	end,
    % New process takes control of ListenSocket by
    % calling gen_tcp:accept/1
    spawn(NewListenProcFun),
    % This process is changed to AcceptSocket handling the message
    receive
	X ->
	    io:format("Port ~p of server process ~p got ~p~n",
		      [inet:port(AcceptSocket), self(), X]),
	    % Sleep to preserve the ListenSocket because
	    % it will be freed when first process dies
	    timer:sleep(3000)
    end.

%---------------------------------------------------------------------
% Process of server 2 hands over control of AcceptSocket to
% a new spawned process after receiving a TCP connection, then
% continues controlling the ListenSocket
%---------------------------------------------------------------------

server2(N) ->
    {ok, ListenSocket} = gen_tcp:listen(?S2_PORT, []),
    server2_accept(N, ListenSocket).

server2_accept(0, ListenSocket) ->
    gen_tcp:close(ListenSocket);
server2_accept(N, ListenSocket) ->
    {ok, AcceptSocket} = gen_tcp:accept(ListenSocket),
    AcceptProcFun =
	fun() ->
		server2_receive(AcceptSocket)
	end,
    AcceptPid = spawn(AcceptProcFun),
    gen_tcp:controlling_process(AcceptSocket, AcceptPid),
    server2_accept(N-1, ListenSocket).

server2_receive(AcceptSocket) ->
    receive
	X ->
	    io:format("Port ~p of server process ~p got ~p~n",
		      [inet:port(AcceptSocket), self(), X])
    end.

%---------------------------------------------------------------------
% Client connects and sends messages to server port
%---------------------------------------------------------------------

client(0, _ServerPort) ->
    ok;
client(N, ServerPort) ->
    {ok, ConnectSocket} = gen_tcp:connect(?S_IP, ServerPort, []),
    io:format("Client open port ~p~n", [inet:port(ConnectSocket)]),
    io:format("Client sends message number ~p to server port ~p~n",
	      [N, ServerPort]),
    gen_tcp:send(ConnectSocket, ?MSG),
    gen_tcp:close(ConnectSocket),
    timer:sleep(1000),
    client(N-1, ServerPort).

%---------------------------------------------------------------------
% Support functions
%---------------------------------------------------------------------

info(N) ->
    io:format("###---------------###~n"
	      "### TEST SERVER ~p ###~n"
	      "###---------------###~n",
	      [N]).
