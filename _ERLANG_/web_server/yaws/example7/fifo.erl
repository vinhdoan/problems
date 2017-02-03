%%%-------------------------------------------------------------------
%%% @author root <root@tuantranu>
%%% @copyright (C) 2016, root
%%% @doc
%%%
%%% @end
%%% Created :  9 Sep 2016 by root <root@tuantranu>
%%%-------------------------------------------------------------------
-module(fifo).

-behaviour(gen_server).

%% API
-export([start/0, stop/0, show/0]).

%% gen_server callbacks
-export([init/1, handle_call/3, handle_cast/2, handle_info/2,
	 terminate/2, code_change/3]).

-define(SERVER, ?MODULE).

-record(state, {pipe_file,
		total = 0,
		users = #{} % UserId -> Pid of WS gen_server process
	       }).

%%%===================================================================
%%% API
%%%===================================================================
start() ->
    start_link().

stop() ->
    gen_server:stop(?SERVER).

show() ->
    gen_server:call(?SERVER, show).
%%--------------------------------------------------------------------
%% @doc
%% Starts the server
%%
%% @spec start_link() -> {ok, Pid} | ignore | {error, Error}
%% @end
%%--------------------------------------------------------------------
start_link() ->
    gen_server:start_link({local, ?SERVER}, ?MODULE, [], []).

%%%===================================================================
%%% gen_server callbacks
%%%===================================================================

%%--------------------------------------------------------------------
%% @private
%% @doc
%% Initializes the server
%%
%% @spec init(Args) -> {ok, State} |
%%                     {ok, State, Timeout} |
%%                     ignore |
%%                     {stop, Reason}
%% @end
%%--------------------------------------------------------------------
init([]) ->
    Pipe = "/tmp/test.pipe",
    os:cmd("mkfifo " ++ Pipe),
    Fifo = open_port(Pipe, [eof]),
    {ok, #state{pipe_file = Fifo}}.

%%--------------------------------------------------------------------
%% @private
%% @doc
%% Handling call messages
%%
%% @spec handle_call(Request, From, State) ->
%%                                   {reply, Reply, State} |
%%                                   {reply, Reply, State, Timeout} |
%%                                   {noreply, State} |
%%                                   {noreply, State, Timeout} |
%%                                   {stop, Reason, Reply, State} |
%%                                   {stop, Reason, State}
%% @end
%%--------------------------------------------------------------------
handle_call(get_user_id, _From, State = #state{total = Total}) ->
    {reply, "user" ++ integer_to_list(Total + 1), State};
handle_call(show, _From, State) ->
    {reply, State, State};
handle_call(_Request, _From, State) ->
    {noreply, State}.

%%--------------------------------------------------------------------
%% @private
%% @doc
%% Handling cast messages
%%
%% @spec handle_cast(Msg, State) -> {noreply, State} |
%%                                  {noreply, State, Timeout} |
%%                                  {stop, Reason, State}
%% @end
%%--------------------------------------------------------------------
handle_cast({connection_close, Pid}, State = #state{users = Users}) ->
    UpdatedUsers = maps:from_list(lists:keydelete(Pid, 2, maps:to_list(Users))),
    {noreply, State#state{users = UpdatedUsers}};
handle_cast({connection_open, {Pid, UserId}},
	    State = #state{users = Users, total = Total}) ->
    UpdatedUsers = Users#{UserId => Pid},
    {noreply, State#state{users = UpdatedUsers, total = Total + 1}};
handle_cast(_Msg, State) ->
    {noreply, State}.

%%--------------------------------------------------------------------
%% @private
%% @doc
%% Handling all non call/cast messages
%%
%% @spec handle_info(Info, State) -> {noreply, State} |
%%                                   {noreply, State, Timeout} |
%%                                   {stop, Reason, State}
%% @end
%%--------------------------------------------------------------------
handle_info({_Port, {data, PipeData}},
	    #state{users = Users} = State) ->
    try
	[UserId, Data] = string:tokens(PipeData, ":"),
	#{UserId := Pid} = Users,
	websocket:send2client(Pid, Data)
    catch
	_:_ ->
	    io:format("Something wrong sending via pipe")
    end,
    {noreply, State};
handle_info(_Info, State) ->
    {noreply, State}.

%%--------------------------------------------------------------------
%% @private
%% @doc
%% This function is called by a gen_server when it is about to
%% terminate. It should be the opposite of Module:init/1 and do any
%% necessary cleaning up. When it returns, the gen_server terminates
%% with Reason. The return value is ignored.
%%
%% @spec terminate(Reason, State) -> void()
%% @end
%%--------------------------------------------------------------------
terminate(_Reason, #state{pipe_file = Fifo}) ->
    port_close(Fifo),
    ok.

%%--------------------------------------------------------------------
%% @private
%% @doc
%% Convert process state when code is changed
%%
%% @spec code_change(OldVsn, State, Extra) -> {ok, NewState}
%% @end
%%--------------------------------------------------------------------
code_change(_OldVsn, State, _Extra) ->
    {ok, State}.

%%%===================================================================
%%% Internal functions
%%%===================================================================
