% solution(boardposition)
solution([]).
solution([p(X, Y) | Others]) :-
	solution(Others), member(Y, [1, 2, 3, 4, 5, 6, 7, 8]),
	noattack(p(X, Y), Others).
noattack(_, []).
	noattack(p(X, Y), [p(X1,Y1) | Others]) :-
	Y =\= Y1, Y1 - Y =\= X1 - X, Y1 - Y =\= X - X1,
	noattack(p(X, Y), Others).
member(I, [I | _]).
member(I, [_ | Rest]) :- member(I, Rest).

% Query:
% ?- solution([p(1,Y1), p(2,Y2), p(3,Y3), p(4,Y4), p(5,Y5), p(6,Y6), p(7,Y7), p(8,Y8)]).
