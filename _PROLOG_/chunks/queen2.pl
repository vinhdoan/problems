solution(Queens) :- permutation([1,2,3,4,5,6,7,8], Queens), safe(Queens).
%permutation([], []).
%permutation([X | L], P):- permutation(L, L1), insert(X, L1, P).
del(X, [X | Tail], Tail).
del(X,[Y | Tail], [Y | Tail1]) :- del(X, Tail, Tail1).
insert(X, List, Biggerlist) :- del(X, Biggerlist, List).
safe([]).
safe([Queen | Others]) :- safe(Others), noattack(Queen, Others, 1).
noattack(_, [], _).
noattack(Y, [Y1 | Ylist], Xdist) :-
	Y1 - Y =\= Xdist, Y - Y1 =\= Xdist,
	Dist1 is Xdist + 1, noattack(Y, Ylist, Dist1).

% Query
% ?- solution(Queens).
