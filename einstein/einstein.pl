%%% Ex: query(pet, fish, R)
query(K, V, R) :-
    result(L),
    key_to_num(K, N),
    check(L, N, V, R).

%%% Ex: result(L)
result(L):- init(L), e(L), f(L).

%house(_1Color, _2Drink, _3Nation, _4Pet, _5Position, _6Tobacco)
init(L) :-
    L = [house(_, _, _, _, 1, _),
         house(_, _, _, _, 2, _),
         house(_, _, _, _, 3, _),
         house(_, _, _, _, 4, _),
         house(_, _, _, _, 5, _)].

e(L) :-
    e([house(yellow,      _,       _,    _, _,     dunhill),
       house( green, coffee,       _,    _, _,           _)], L),
    e([house(     _,   milk,       _,    _, 3,           _),
       house(     _,   beer,       _,    _, _, blue_master),
       house(     _,    tea, denmark,    _, _,           _)], L),
    e([house(   red,      _, england,    _, _,           _),
       house(     _,      _,  norway,    _, 1,           _),
       house(     _,      _, germany,    _, _,      prince),
       house(     _,      _,  sweden,  dog, _,           _)], L),
    e([house(     _,      _,       _, bird, _,   pall_mall),
       house(     _,      _,       _, fish, _,           _)], L).

e([H | T], L) :- select(H, L, L2), e(T, L2).
e([], _).

f(L) :-
        left(house(green,     _,      _,     _, _,       _),
             house(white,     _,      _,     _, _,       _), L), %r4
    neighbor(house(    _,     _,      _,     _, _,  blends),
             house(    _,     _,      _,   cat, _,       _), L), %r10
    neighbor(house(    _,     _,      _, horse, _,       _),
             house(    _,     _,      _,     _, _, dunhill), L), %r11
    neighbor(house(    _,     _, norway,     _, _,       _),
             house( blue,     _,      _,     _, _,       _), L), %r14
    neighbor(house(    _,     _,      _,     _, _,  blends),
             house(    _, water,      _,     _, _,       _), L). %r15

left(X, Y, L) :- nextto(X, Y, L).
right(X, Y, L) :- nextto(Y, X, L).
neighbor(X, Y, L) :- left(X, Y, L); right(X, Y, L).

key_to_num(   color, 1).
key_to_num(   drink, 2).
key_to_num(  nation, 3).
key_to_num(     pet, 4).
key_to_num(position, 5).
key_to_num( tobacco, 6).

check([H|_T], N, V, H) :-
    arg(N, H, V), !.
check([_H|T], N, V, R) :-
    check(T, N, V, R).
