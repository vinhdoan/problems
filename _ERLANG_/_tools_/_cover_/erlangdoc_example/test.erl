-module(test).
-export([s/0]).

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%% STEPS TO TEST COVER %%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

%% Start Cover: start a process which owns Cover DB where all coverage data will be stored
%%  1> cover:start().

%% Involved modules must be Cover compiled
%%  2> cover:compile_module(channel).

%% Run the test
%%  3> test:s().

%% Analyze modules coverage: {Module,{Cov,NotCov}}
%%  4> cover:analyse(channel,coverage,module).

%% Analyze functions coverage: {Function,{Cov,NotCov}}
%%  5> cover:analyse(channel,coverage,function).

%% Analyze clauses coverage: {Clause,{Cov,NotCov}}
%%  6> cover:analyse(channel,coverage,clause).

%% Analyze lines coverage: {Line,{Cov,NotCov}}
%%  7> cover:analyse(channel,coverage,line).

%% Analyze modules call statistics: {Module,Calls}
%%  8> cover:analyse(channel,calls,module).

%% Analyze functions call statistics: {Function,Calls}
%%  9> cover:analyse(channel,calls,function).

%% Analyze clauses call statistics: {Clause,Calls}
%%  10> cover:analyse(channel,calls,clause).

%% Analyze lines call statistics: {Line,Calls}
%%  11> cover:analyse(channel,calls,line).

%% Output analysis to file
%%  12> cover:analyse_to_file(channel).

%% After Cover is stopped, all Cover compiled modules are unloaded.
%% The code for channel is now loaded as usual from a .beam file in the current path.
%%  13> code:which(channel).
%%  14> cover:stop().
%%  15> code:which(channel).

s() ->
    {ok, _Pid} = channel:start_link(),
    {ok, Ch1} = channel:alloc(),
    ok = channel:free(Ch1),
    ok = channel:stop().
