-module(udp_mp).
-export([test/1]).

-define(S_IP, {127,0,0,1}).
-define(S_PORT, 1234).
-define(C_PORT, 2345).
-define(MSG, "A message").

test(N) ->
    ServerFun = fun() -> server(N) end,
    ClientFun = fun() -> client(N) end,
    spawn(ServerFun),
    spawn(ClientFun),
    timer:sleep(1000).

server(N) ->
    {ok, OpenSocket} = gen_udp:open(?S_PORT),
    server_receive(N, OpenSocket).

server_receive(0, OpenSocket) ->
    gen_udp:close(OpenSocket);
server_receive(N, OpenSocket) ->
    receive
	X ->
	    io:format("Got: ~p~n", [X]),
	    server_receive(N-1, OpenSocket)
    end.

client(N) ->
    {ok, OpenSocket} = gen_udp:open(?C_PORT),
    client_send(N, OpenSocket).

client_send(0, OpenSocket) ->
    gen_udp:close(OpenSocket);
client_send(N, OpenSocket) ->
    io:format("Client sends ~pth message~n", [N]),
    gen_udp:send(OpenSocket, ?S_IP, ?S_PORT, ?MSG),
    client_send(N-1, OpenSocket).
