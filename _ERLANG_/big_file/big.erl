%%%-------------------------------------------------------------------
%%% @author Tuan Tran <>
%%% @copyright (C) 2016, Tuan Tran
%%% @doc
%%%
%%% @end
%%% Created : 10 Oct 2016 by Tuan Tran <>
%%%-------------------------------------------------------------------
-module(big).

-behaviour(gen_server).
-compile(export_all).

%% API
-export([start/0, stop/0]).
-export([start_link/1,
	 start_link/2]).

%% gen_server callbacks
-export([init/1, handle_call/3, handle_cast/2, handle_info/2,
	 terminate/2, code_change/3]).

-define(SERVER, ?MODULE).

-record(master_state, {
	  %% Configurable
	  max_slaves = 10,
	  max_len = 100,
	  %% Runtime
	  slaves = []
	 }).

-record(slave_state, {
	  id
	 }).

%%%===================================================================
%%% API
%%%===================================================================
start() ->
    ?MODULE:start_link(?SERVER).

stop() ->
    gen_server:stop(?SERVER).

%%--------------------------------------------------------------------
%% @doc
%% Starts the server
%%
%% @spec start_link() -> {ok, Pid} | ignore | {error, Error}
%% @end
%%--------------------------------------------------------------------
start_link(?SERVER = RegName) ->
    {ok, _Pid} = gen_server:start_link({local, RegName}, ?MODULE, [RegName], []).

start_link(Slave, State) ->
    {ok, Pid} = gen_server:start_link(?MODULE, [Slave], []),
    ListOfSlaves = [{Slave, Pid} | State#master_state.slaves],
    State#master_state{slaves = ListOfSlaves}.

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
init([?SERVER]) ->
    State = #master_state{},
    NewState = recruit_slaves(State#master_state.max_slaves, State),
    {ok, NewState};

init([{slave, N}]) ->
    {ok, #slave_state{id = N}}.

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
handle_call(start, _From, State = #master_state{slaves = Slaves,
						max_slaves = MaxSlaves,
						max_len = MaxLen}) ->
    {ok, S} = file:open("/mnt/Temps/tuan.txt", [read]),
    io:format("Test: ~p~n", [file:pread(S, [{10,20}])]),
    Reply = assign(Slaves, {MaxSlaves, MaxLen}, S),
    file:close(S),
    {reply, Reply, State};
handle_call({config, Config}, _From, State) ->
    NewState = 
	try
	    configure(State, Config)
	catch
	    _:_ ->
		State
	end,
    Reply = NewState,
    {reply, Reply, State};
handle_call(_Request, _From, State) ->
    Reply = State,
    {reply, Reply, State}.

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
handle_cast({count_length, S, Position, Number}, State = #slave_state{}) ->
    %% Text = file:pread(S, [{Position, Number}]),
    Text = file:read(S, 5),
    io:format("~p~n", [Text]),
    %% ?SERVER ! count_length(Text),
    {noreply, State};
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
handle_info(Info, State = #master_state{}) ->
    io:format("~p~n", [Info]),
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
terminate(_Reason, #master_state{slaves = Slaves}) ->
    kill_slaves(Slaves);
terminate(_Reason, _State) ->
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
configure(#master_state{} = State, [{Key, Value} | T]) ->
    configure(setelement(index(Key), State, Value), T).

index(max_slaves) ->
    2;
index(max_len) ->
    3.

assign(Slaves, {MaxSlaves, MaxLen}, S) ->
    assign(Slaves, {MaxSlaves, MaxLen}, S, 0).

assign([],
       {_MaxSlaves, _MaxLen}, _S,
       _Position) ->
    ok;
assign([{_Name, Pid} | Slaves],
       {_MaxSlaves, MaxLen} = Config, S,
       Position) ->
    gen_server:cast(Pid, {count_length, S, Position, MaxLen}),
    assign(Slaves, Config, S, Position + MaxLen).

recruit_slaves(0, State) ->
    State;
recruit_slaves(N, State) ->
    NewState = ?MODULE:start_link({slave, N}, State),
    recruit_slaves(N - 1, NewState).

kill_slaves([]) ->
    ok;
kill_slaves([{_Name, Pid} | T]) ->
    gen_server:stop(Pid),
    kill_slaves(T).

count_letters(Text) ->
    count_letters(Text, #{}).

count_letters([], Map) ->
    Map;
count_letters([Letter | Text], Map) ->
    NewMap = Map#{[Letter] => maps:get([Letter], Map, 0) + 1},
    count_letters(Text, NewMap).

count_length(Text) ->
    count_length(Text, 0).

count_length([], N) ->
    N;
count_length([_H | T], N) ->
    count_length(T, N + 1).
