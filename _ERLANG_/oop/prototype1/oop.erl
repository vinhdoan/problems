-module(oop).
-compile(export_all).
-include_lib("eunit/include/eunit.hrl").

-define(FUNC,
	element(2, element(2, process_info(self(), current_function)))).
-define(CLASS, ?FUNC).
-define(CALL(FunName, Args), apply(?MODULE, FunName, Args)).
-define(HANDLER(X),
	list_to_atom(atom_to_list(X) ++ "_handler")).

-define(CLASS_DEF_1(ClassName),
	ClassName(State) ->
	       ?CALL(?CLASS, [State, ?PARENT(?CLASS)])).
-define(CLASS_DEF_2(ClassName),
	ClassName(State, Parent) ->
	       ?CALL(?CLASS, [State, Parent, []])).
-define(CLASS_DEF_3(ClassName),
	ClassName(State, default, Child) ->
	       ?CALL(?CLASS, [State, ?PARENT(?CLASS), Child]);
	    ClassName(State, undefined, Child) ->
	       ?CALL(?HANDLER(?CLASS), [State, Child, undefined]);
	    ClassName(State, Parent, Child) ->
	       ?CALL(Parent, [State, default, [?CLASS | Child]])).

-define(HDL_NO_BASE_DEF(ClassHandler, Behaviors),
	ClassHandler(State, Child, undefined) ->
	       receive
		   Behaviors;
		   X ->
		       case Child of
			   [] ->
			       ok;
			   [FirstChild | GrandChild] ->
			       ?CALL(?HANDLER(FirstChild), [State, GrandChild, X])
		       end,
		       ?CALL(?FUNC, [State, Child, undefined])
	       end).

-define(HDL_BASE_DEF(ClassHandler, Behaviors),
	ClassHandler(State, Child, Data) -> Behaviors).


%%%%%%%%%%%%%%%%%%%
%%% FOR TESTING %%%
%%% Define Here %%%
%%%%%%%%%%%%%%%%%%%
test() ->
    P1 = spawn(?MODULE, classA, [none]),
    P1 ! a,
    P2 = spawn(?MODULE, classB, [none]),
    P2 ! a,
    P2 ! c,
    P3 = spawn(?MODULE, classC, [none]),
    P3 ! a,
    P4 = spawn(?MODULE, classD, [none]),
    P4 ! a,
    P4 ! e.

%%%%%%%%%%%%%%%%%%%
%%% INHERITANCE %%%
%%% Define Here %%%
%%%%%%%%%%%%%%%%%%%
-define(PARENT(X), case X of
		       classB ->
			   classA;
		       classD ->
			   classC;
		       _ ->
			   undefined
		   end).

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%% class(State, Parent, Child) %%%
%%% Define Here                 %%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
?CLASS_DEF_1(classA).
?CLASS_DEF_2(classA).
?CLASS_DEF_3(classA).

?CLASS_DEF_1(classB).
?CLASS_DEF_2(classB).
?CLASS_DEF_3(classB).

?CLASS_DEF_1(classC).
?CLASS_DEF_2(classC).
?CLASS_DEF_3(classC).

?CLASS_DEF_1(classD).
?CLASS_DEF_2(classD).
?CLASS_DEF_3(classD).

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%% class_handler(State, Child, Data) %%%
%%% Define Here                       %%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
?HDL_NO_BASE_DEF(classA_handler,
		 a ->
			begin
			    io:format("~p", [{a, handled_by_A}]),
			    ?CALL(?FUNC, [State, Child, undefined])
			end;
		     b ->
			begin
			    io:format("~p", [{b, handled_by_A}]),
			    ?CALL(?FUNC, [State, Child, undefined])
			end).

?HDL_BASE_DEF(classB_handler,
	      io:format("~p~n", [{Data, handled_by_B}])
	     ).

?HDL_NO_BASE_DEF(classC_handler,
		 a ->
			begin
			    io:format("~p~n", [{a, handled_by_C}]),
			    ?CALL(?FUNC, [State, Child, undefined])
			end;
		     b ->
			begin
			    io:format("~p~n", [{b, handled_by_C}]),
			    ?CALL(?FUNC, [State, Child, undefined])
			end
			    ).

?HDL_BASE_DEF(classD_handler,
	      io:format("~p~n", [{Data, handled_by_E}])).
