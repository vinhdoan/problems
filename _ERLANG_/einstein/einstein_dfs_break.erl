-module(einstein_dfs_break).
-compile(export_all).

-define(ruleE, [r1, r2, r3, r5, r6, r7, r8, r9, r12, r13, r16]).
-define(ruleF, [r4, r10, r11, r14, r15]).

result() ->
    f(e(init())).

init() ->
    [init(1), init(2), init(3), init(4), init(5)].

init(N) ->
    #{color => na,
      drink => na,
      nation => na,
      pet => na,
      position => N,
      tobacco => na}.

e(L) ->
    e([{L, ?ruleE}], []).

e([], Acc) ->
    Acc;
e([{State, []} | Stack], Acc) ->
    e(Stack, [State | Acc]);
e([{State, [Rule | Rules]} | Stack], Acc) ->
    Successors = ruleE_gen(State, Rule),
    if Successors == [] ->
            e(Stack, Acc);
       true ->
            e(zip(Successors, Rules) ++ Stack,
              Acc)
    end.

ruleE_gen(State, Rule) ->
    ruleE_gen({State, {State, 1}}, Rule, []).

ruleE_gen({[], _}, _, Acc) ->
    Acc;
ruleE_gen({[House | T], {State, N}},
         Rule,
         Acc) ->
    case ruleE_match(House, ?MODULE:Rule()) of
        failed ->
            ruleE_gen({T, {State, N + 1}}, Rule, Acc);
        NewHouse ->
            NewHouses =
                lists:sublist(State, N - 1) ++
                [NewHouse] ++
                lists:nthtail(N, State),
            ruleE_gen({T, {State, N + 1}}, Rule, [NewHouses | Acc])
    end.

ruleE_match(House, [{K1, V1}, {K2, V2}]) ->
    #{K1 := HV1, K2 := HV2} = House,
    case {lists:member(HV1, [na, V1]),
          lists:member(HV2, [na, V2])} of
        {true, true} ->
            House#{K1 := V1, K2:= V2};
        _ ->
            failed
    end.

f(L) ->
    %% f([{L, ?ruleF}], []).
    f(zip(L, ?ruleF), []).

f([], Acc) ->
    Acc;
f([{State, []} | Stack], Acc) ->
    f(Stack, [State | Acc]);
f([{State, [Rule | Rules]} | Stack], Acc) ->
    Successors = ruleF_gen(State, Rule),
    if Successors == [] ->
            f(Stack, Acc);
       true ->
            f(zip(Successors, Rules) ++ Stack,
              Acc)
    end.

ruleF_gen(State, Rule) ->
    ruleF_gen({State, {State, 1}}, Rule, []).

ruleF_gen({[_House | []], _}, _, Acc) ->
    Acc;
ruleF_gen({[House1, House2 | Houses], {State, N}},
          Rule,
          Acc) ->
    NeighborType = neighbor_type(Rule),
    case ruleF_match({House1, House2, State, NeighborType},
                     ?MODULE:Rule()) of
        [] ->
            ruleF_gen({[House2 | Houses], {State, N + 1}}, Rule, Acc);
        NewPairOfHouses ->
            NewStates =
                lists:foldl(
                  fun({}, Res) -> Res;
                     ({NewHouse1, NewHouse2}, Res) ->
                          [lists:sublist(State, N - 1) ++
                               [NewHouse1, NewHouse2] ++
                               lists:nthtail(N + 1, State) | Res]
                  end, [], NewPairOfHouses),
            ruleF_gen({[House2 | Houses], {State, N + 1}},
                      Rule,
                      NewStates ++ Acc)
    end.

ruleF_match({House1, House2, State, NeighborType},
            [{K1, V1}, {K2, V2}]) ->
    OtherHouses = State -- [House1, House2],
    IsOtherHousesInvolved =
        lists:any(fun(#{K1 := HV1, K2 := HV2}) ->
                          HV1 == V1 orelse HV2 == V2
                  end, OtherHouses),
    if IsOtherHousesInvolved ->
            [];
       true ->
            #{K1 := H1V1, K2 := H1V2} = House1,
            #{K1 := H2V1, K2 := H2V2} = House2,
            Left = lists:member(left, NeighborType),
            Right = lists:member(right, NeighborType),
            [if Left ->
                     case {lists:member(H1V1, [na, V1]),
                           lists:member(H2V2, [na, V2]),
                           H2V1 /= V1, H1V2 /= V2} of
                         {true, true, true, true} ->
                             {House1#{K1 := V1},
                              House2#{K2 := V2}};
                         _ ->
                             {}
                     end;
                true ->
                     {}
             end,
             if Right ->
                     case {lists:member(H2V1, [na, V1]),
                           lists:member(H1V2, [na, V2]),
                           H1V1 /= V1, H2V2 /= V2} of
                         {true, true, true, true} ->
                             {House1#{K2 := V2},
                              House2#{K1 := V1}};
                         _ ->
                             {}
                     end;
                true ->
                     {}
             end]
    end.

zip(List, Elem) ->
    lists:zip(List, lists:duplicate(length(List), Elem)).

r1()  -> [{  nation,     england}, {   color,     red}].
r2()  -> [{  nation,      sweden}, {     pet,     dog}].
r3()  -> [{  nation,     denmark}, {   drink,     tea}].
r4()  -> [{   color,       green}, {   color,   white}].
r5()  -> [{   color,       green}, {   drink,  coffee}].
r6()  -> [{ tobacco,   pall_mall}, {     pet,    bird}].
r7()  -> [{   color,      yellow}, { tobacco, dunhill}].
r8()  -> [{position,           3}, {   drink,    milk}].
r9()  -> [{  nation,      norway}, {position,       1}].
r10() -> [{ tobacco,      blends}, {     pet,     cat}].
r11() -> [{     pet,       horse}, { tobacco, dunhill}].
r12() -> [{ tobacco, blue_master}, {   drink,    beer}].
r13() -> [{  nation,     germany}, { tobacco,  prince}].
r14() -> [{  nation,      norway}, {   color,    blue}].
r15() -> [{ tobacco,      blends}, {   drink,   water}].
r16() -> [{     pet,        fish}, {     pet,    fish}].

neighbor_type(r4)  -> [left];
neighbor_type(r10) -> [left, right];
neighbor_type(r11) -> [left, right];
neighbor_type(r14) -> [left, right];
neighbor_type(r15) -> [left, right].
