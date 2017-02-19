permute([], []).
permute([X|L], P) :-
	permute(L, L1),
	insert(X, L1, P).

insert(X, L, R) :-
	append(L1, L2, L),
	append(L1, [X|L2], R).