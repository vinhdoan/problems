evenlength([]).

evenlength([H|T]) :-
	oddlength(T).

oddlength([H|T]) :-
	evenlength(T).