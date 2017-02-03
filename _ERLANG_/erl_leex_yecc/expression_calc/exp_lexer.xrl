Definitions.

D   = [0-9]
Add = \+
Sub = -
Mul = \*
Div = /
L   = [A-Za-z]
WS  = ([\000-\s]|%.*)

Rules.

{Div} : {token,{divi,TokenLine,list_to_atom(TokenChars)}}.
{Add} : {token,{add,TokenLine,list_to_atom(TokenChars)}}.
{Sub} : {token,{sub,TokenLine,list_to_atom(TokenChars)}}.
{Mul} : {token,{mul,TokenLine,list_to_atom(TokenChars)}}.
{D}+  : {token,{integer,TokenLine,list_to_integer(TokenChars)}}.
{D}+\.{D}+((E|e)(\+|\-)?{D}+)?      : {token,{float, TokenLine,list_to_float(TokenChars)}}.
[()]  : {token,{list_to_atom(TokenChars),TokenLine}}.
{WS}+  : skip_token.


Erlang code.
