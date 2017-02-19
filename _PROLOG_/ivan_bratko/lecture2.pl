% membr
membr(X, [X|_]).
membr(X, [_H|T]) :-
	membr(X, T).

% conc
conc([], L , L).
conc([X|L1], L2, [X|L3]) :-
	conc(L1, L2, L3).

% add
add(X, L, [X|L]).

% del
del(X, [X|T], T).
del(X, [Y|T], [Y|T1]) :-
	del(X, T, T1).

% insert
insert(X, L, BL) :-
	del(X, BL, L).

% sublist
sublist(S, L) :-
	conc(_L1, L2, L),
	conc(S, _L3, L2).

% permutation
permute([], []).
permute([X|L], P) :-
	permute(L, L1),
	insert(X, L1, P).

                                      
