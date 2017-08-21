-module(channel).
-behaviour(gen_server).

-export([start_link/0, stop/0]).
-export([alloc/0, free/1]). % client interface
-export([init/1, handle_call/3, handle_cast/2, terminate/2]). % callback functions

start_link() ->
    gen_server:start_link({local, channel}, channel, [], []).

stop() ->
    gen_server:call(channel, stop).

%%%-Client interface functions-------------------------------------------

alloc() ->
    gen_server:call(channel, alloc).

free(Channel) ->
    gen_server:call(channel, {free, Channel}).

%%%-gen_server callback functions----------------------------------------

init(_Arg) ->
    {ok, channels()}.

handle_call(stop, _Client, Channels) ->
    {stop, normal, ok, Channels};

handle_call(alloc, _Client, Channels) ->
    {Ch, Channels2} = alloc(Channels),
    {reply, {ok, Ch}, Channels2};

handle_call({free, Channel}, _Client, Channels) ->
    Channels2 = free(Channel, Channels),
    {reply, ok, Channels2}.

handle_cast(_, S) ->
    {noreply, S}.

terminate(_Reason, _Channels) ->
    ok.

%%%-Internal functions---------------------------------------------------

channels() ->
    [ch1, ch2, ch3].

alloc([Channel | Channels]) ->
    {Channel, Channels};
alloc([]) ->
    false.

free(Channel, Channels) ->
    [Channel | Channels].
