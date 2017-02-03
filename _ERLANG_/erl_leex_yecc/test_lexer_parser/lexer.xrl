Definitions.

D   = [0-9]
L   = [A-Za-z]
WS  = ([\000-\s]|%.*)


Rules.

{L}+   : {token,{word,TokenLine,TokenChars}}.
{D}+   : {token,{integer,TokenLine,list_to_integer(TokenChars)}}.
{D}+\.{D}+((E|e)(\+|\-)?{D}+)?      : {token,{float, TokenLine,list_to_float(TokenChars)}}.
{WS}+  : skip_token.

Erlang code.

