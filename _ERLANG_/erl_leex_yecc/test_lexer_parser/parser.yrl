Nonterminals
predicates predicate elem.

Terminals
integer float word.

Rootsymbol predicates.

predicates -> predicate : ['$1'].
predicates -> predicate predicates : ['$1'] ++ '$2'.

predicate -> word elem : {unwrap('$1'), '$2'}.

elem -> integer : unwrap('$1').
elem -> float : unwrap('$1').

Erlang code.
unwrap({_,_,V}) -> V.