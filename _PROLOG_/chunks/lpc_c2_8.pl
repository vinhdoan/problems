maplist([], _ , []).
maplist([X | Tail], F, [NewX| NewTail]) :- G =..[F, X, NewX], call(G), maplist( Tail, F, NewTail).
																	 
res(X, yes) :- atomic(X), !.
res(_, no).