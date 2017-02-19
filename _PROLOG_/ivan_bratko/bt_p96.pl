% flatten(List, Result)
flatten([H|T], R) :-
	flatten([H], R1),
	flatten(T, R2),
	append(R1, R2, R).
flatten([H|[]], R) :-

is_a_list([_H|_T].

% subset(Set, SubSet)
subset(S, R) :-
	subset(S, R, _L).
subset([], [], []).
subset([H|S1], [H|R1], L) :-
	subset(S1, R1, L).
subset([H|S1], R, [H|L1]) :-
	subset(S1, R, L1).

% dividelist(List, List1, List2)
dividelist(L, L1, L2) :-
	subset(L, L1, L2),
	samesize(L1, L2).

samesize([], []).
samesize([], [_H2|[]]).
samesize([_H1|[]], []).
samesize([_H1|T1], [_H2|T2]) :-
	samesize(T1, T2).

% reverse
revs(L, R) :-
	revs(L, [], R).
revs([], L, L).
revs([H | T], L, R) :-
	revs(T, [H | L], R).
	
% palindrome
palindrome(L) :-
	revs(L, L).
	
% shift
lshift([], []).
lshift([H | T], R) :-
	revs(T, T1),
	revs([H | T1], R).

rshift([], []).
rshift(L, [H | T1]) :-
	revs(L, [H | T]),
	revs(T, T1).

% translate
means(0, zero).
means(1, one).
means(2, two).
means(3, three).
means(4, four).
means(5, five).
means(6, six).
means(7, seven).
means(8, eight).
means(9, nine).
translate(L, R) :-
	translate(L, [], R).
translate([], A, R) :-
	revs(A, R).
translate([H | T], A, R) :-
	means(H, M),
	translate(T, [M | A], R).

% factorial
func(N, R) :-
	func(N, 1, R).
func(0, A, A) :- !.
func(N, A, R) :-
	NewA is N * A,
	NewN is N - 1,
	func_tag(NewN, NewA, R).