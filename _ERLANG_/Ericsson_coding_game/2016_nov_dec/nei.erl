-module(nei).
-compile(export_all).

f([HL|TL]) ->
    C = lists:foldl(fun({F, L}, [{FH, LH}|T] = A) ->
 			case m({F, L}, {FH, LH}) of
 			    no_merge ->
 				[{F, L}|A];
 			    R ->
 				[R|T]
 			end
		    end, [HL], TL),
    lists:reverse(C).

m({A, B}, {C, D}) when A > C ->
    m({C, D}, {A, B});
m({A, B}, {C, D}) when C =< B ->
    {A, max(B, D)};
m(_, _) ->
    no_merge.
