Nonterminals
exp exp2 exp3.

Terminals
'(' ')' add sub mul divi integer float.

Rootsymbol exp.

% exp -> exp add exp2 : '$1' + '$3'.
% exp -> exp sub exp2 : '$1' - '$3'.
% exp -> exp2 : '$1'.
% exp2 -> exp2 mul exp3 : '$1' * '$3'.
% exp2 -> exp2 divi exp3 : '$1' / '$3'.
% exp2 -> exp3 : '$1'.
% exp3 -> integer : unwrap('$1').
% exp3 -> float : unwrap('$1').

exp -> exp add exp2 : {add, '$1', '$3'}.
exp -> exp sub exp2 : {sub, '$1', '$3'}.
exp -> exp2 : '$1'.
exp2 -> exp2 mul exp3 : {mul, '$1', '$3'}.
exp2 -> exp2 divi exp3 : {divi, '$1', '$3'}.
exp2 -> exp3 : '$1'.
exp3 -> integer : unwrap('$1').
exp3 -> float : unwrap('$1').
exp3 -> '(' exp ')' : {group, '$2'}.

Erlang code.
unwrap({_,_,V}) -> V.
