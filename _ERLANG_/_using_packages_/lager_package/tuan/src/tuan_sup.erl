%%%-------------------------------------------------------------------
%% @doc tuan top level supervisor.
%% @end
%%%-------------------------------------------------------------------

-module(tuan_sup).

-behaviour(supervisor).

%% API
-export([start_link/0]).

%% Supervisor callbacks
-export([init/1]).

-define(SERVER, ?MODULE).

%%====================================================================
%% API functions
%%====================================================================

start_link() ->
    supervisor:start_link({local, ?SERVER}, ?MODULE, []).

%%====================================================================
%% Supervisor callbacks
%%====================================================================

%% Child :: {Id,StartFunc,Restart,Shutdown,Type,Modules}
init([]) ->
    SupFlags = #{strategy => one_for_one,
                 intensity => 1,
                 period => 5},

    AChild = #{id => tuanChild_1,
               start => {tuan, start_link, []},
               restart => transient,
               shutdown => 5000,
               type => worker,
               modules => [tuan]},

    {ok, {SupFlags, [AChild]}}.

%%====================================================================
%% Internal functions
%%====================================================================
