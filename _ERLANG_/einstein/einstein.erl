-module(einstein).
-export([query/2,
	 result/0]).

%%% Ex: einstein:query(pet, fish).
query(K, V) ->
    [begin
	 [R] = take(X, K, V),
	 R
     end || X <- result()].

%%% Ex: einstein:result().
result() ->
    f(e()).

f(L) ->
    [[X1, X2, X3, X4, X5]
     || X1 <- take(L, position, 1),
	X2 <- take(L, position, 2),
	X3 <- take(L, position, 3),
	X4 <- take(L, position, 4),
	X5 <- take(L, position, 5),
	is_diff(X1, X2, X3, X4, X5),
	r4([X1, X2, X3, X4, X5]),
	r10([X1, X2, X3, X4, X5]),
	r11([X1, X2, X3, X4, X5]),
	r14([X1, X2, X3, X4, X5]),
	r15([X1, X2, X3, X4, X5])].

is_diff(X1, X2, X3, X4, X5) ->
    is_diff(X1, X2, X3, X4, X5, color) andalso
	is_diff(X1, X2, X3, X4, X5, pet) andalso
	is_diff(X1, X2, X3, X4, X5, drink) andalso
	is_diff(X1, X2, X3, X4, X5, tobacco) andalso
	is_diff(X1, X2, X3, X4, X5, nation).

is_diff(X1, X2, X3, X4, X5, K) ->
    [#{K:=V1}, #{K:=V2}, #{K:=V3}, #{K:=V4}, #{K:=V5}] =
	[X1, X2, X3, X4, X5],
    length(lists:usort([V1, V2, V3, V4, V5])) == 5.

% LeftRight = left | right | nextto
neighbor(L, {K1, V1}, {K2, V2}, LeftRight) ->
    case {take(L, K1, V1), take(L, K2, V2)} of
	{[], _} ->
	    false;
	{_, []} ->
	    false;
	{[#{position := P1}], [#{position := P2}]} ->
	    case LeftRight of
		nextto ->
		    abs(P2 - P1) == 1;
		left ->
		    P2 - P1 == 1;
		right ->
		    P1 - P2 == 1
	    end;
	{_, _} ->
	    false
    end.

r4(L) ->
    neighbor(L, {color, green}, {color, white}, left).

r10(L) ->
    neighbor(L, {tobacco, blends}, {pet, cat}, nextto).

r11(L) ->
    neighbor(L, {pet, horse}, {tobacco, dunhill}, nextto).

r14(L) ->
    neighbor(L, {nation, norway}, {color, blue}, nextto).

r15(L) ->
    neighbor(L, {tobacco, blends}, {drink, water}, nextto).

take(L, K, V) ->
    [X || X = #{K := U} <- L, V == U].

e() ->
    [#{position => Position,
       color => Color,
       pet => Pet,
       tobacco => Tobacco,
       nation => Nation,
       drink => Drink}
     || Color <- [red, white, green, blue, yellow],
	Pet <- [dog, bird, cat, horse, fish],
	Tobacco <- [pall_mall, dunhill, blends, blue_master, prince],
	Nation <- [england, sweden, denmark, germany, norway],
	Drink <- [tea, coffee, milk, beer, water],
	Position <- [1, 2, 3, 4, 5],
	case Nation of
	    germany ->
		Tobacco == prince;
	    norway ->
		Position == 1 andalso Color /= blue;
	    england ->
		Color == red;
	    sweden ->
		Pet == dog;
	    denmark ->
		Drink == tea
	end,
	case Color of
	    green ->
		Position < 5 andalso Drink == coffee;
	    white ->
		Position > 1;
	    yellow ->
		Tobacco == dunhill;
	    red ->
		Nation == england;
	    blue ->
		Nation /= norway
	end,
	case Tobacco of
	    prince ->
		Nation == germany;
	    blue_master ->
		Drink == beer;
	    pall_mall ->
		Pet == bird;
	    blends ->
		Pet /= cat andalso Drink /= water;
	    dunhill ->
		Color == yellow andalso Pet /= horse
	end,
	case Position of
	    1 ->
		Nation == norway;
	    3 ->
		Drink == milk;
	    _ ->
		true
	end,
	case Drink of
	    water ->
		Tobacco /= blends;
	    beer ->
		Tobacco == blue_master;
	    milk ->
		Position == 3;
	    tea ->
		Nation == denmark;
	    coffee ->
		Color == green
	end,
	case Pet of
	    horse ->
		Tobacco /= dunhill;
	    cat ->
		Tobacco /= blends;
	    bird ->
		Tobacco == pall_mall;
	    dog ->
		Nation == sweden;
	    _ ->
		true
	end
    ].
