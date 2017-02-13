-module(dr_problem1).
-compile(export_all).

-define(PEOPLE, [nhat, nhi]).
-define(monday, 2).
-define(sunday, 8).

solve() ->
    Samples = [#{{person, 1} => P1,
		 {person, 2} => P2,
		 today => Day}
	       || P1 <- ?PEOPLE,
		  P2 <- ?PEOPLE,
		  P1 /= P2,
		  Day <- lists:seq(?monday, ?sunday)],
    Clauses = [{person, 1}, {person, 2}],
    [X || X <- Samples,
	  lists:all(fun(Clause) ->
			    all_unique(Clause, X)
		    end, Clauses)].

all_unique(P, Sample) ->
    #{P := Name, today := Day} = Sample,
    all_unique(clauses(P), Sample, truth(Name, Day)).

all_unique([], _Sample, _UniqBool) ->
    true;
all_unique([{Key, Val} | T], Sample, UniqBool) ->
    #{Key := SampleVal} = Sample,
    FuncName = case Key of
		   {K, _V} ->
		       K;
		   _ ->
		       Key
	       end,
    Check = case ?MODULE:FuncName(Val) of
		List when is_list(List) ->
		    lists:member(SampleVal, List);
		Res ->
		    Res == SampleVal
	    end,
    if UniqBool == Check ->
	    all_unique(T, Sample, Check);
       true ->
	    false
    end.

clauses({person, 1}) -> [{{person, 1}, {name, nhat}},
			 {today, {yesterday, 8}}];
clauses({person, 2}) -> [{today, {tomorrow, 6}},
			 {{person, 2}, {say_truth, 4}}].

truth(nhat, 2) -> false;
truth(nhat, 3) -> false;
truth(nhat, 4) -> false;
truth(nhat, _) -> true;
truth(nhi, 3) -> false;
truth(nhi, 5) -> false;
truth(nhi, 7) -> false;
truth(nhi, _) -> true.

today({today, N}) -> N;
today({yesterday, 8}) -> 2;
today({yesterday, N}) -> N + 1;
today({tomorrow, 2}) -> 8;
today({tomorrow, N}) -> N - 1.

person({name, Name}) -> Name;
person({say_truth, Day}) -> [X || X <- ?PEOPLE, truth(X, Day)].
