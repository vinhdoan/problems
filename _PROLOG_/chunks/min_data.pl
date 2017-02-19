min_data(null, inf_p) :- !.
min_data(t(L, X, R), M) :- 
	min_data(L, ML),
	min_data(R, MR),
	min(X, ML, MR, M), !.

min(A, B, C, R) :-
	min(A, B, T),
	min(T, C, R).
min(A, inf_p, A).
min(A, B, A) :- A < B.
min(_, B, B).
