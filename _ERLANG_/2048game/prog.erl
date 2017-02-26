-module(prog).
-compile(export_all).

-define(cell_width, 6).
%% Clone:
%% https://github.com/julianduque/erlang-color

f(N) ->
    Spaces = string:chars($ , ?cell_width),
    ColorSpaces = on_red(Spaces),
    io:format("\e[H\e[J"),
    io:format("~s~n~s~n~s~n",
	      [ColorSpaces,
	       on_red(form_str(N)),
	       ColorSpaces]),
    {ok, [Char]} = io:fread("", "~c"),
    case Char of
	"w" ->
	    f(N + 1);
	"s" ->
	    f(N - 1);
	"q" ->
	    ok;
	_ ->
	    f(N)
    end.

form_str(N) ->
    Str = integer_to_list(N),
    Len = string:len(Str),
    StrL = string:right(Str, Len + (?cell_width - Len) div 2),
    string:left(StrL, ?cell_width).


%----------------
-define(ESC, <<"\e[">>).
-define(RST, <<"0">>).
-define(BOLD, <<"1">>).
-define(SEP, <<";">>).
-define(END, <<"m">>).

%% Colors
-define(BLACK, <<"30">>).
-define(RED, <<"31">>).
-define(GREEN, <<"32">>).
-define(YELLOW, <<"33">>).
-define(BLUE, <<"34">>).
-define(MAGENTA, <<"35">>).
-define(CYAN, <<"36">>).
-define(WHITE, <<"37">>).
-define(DEFAULT, <<"39">>).

%% Background colors
-define(BLACK_BG, <<"40">>).
-define(RED_BG, <<"41">>).
-define(GREEN_BG, <<"42">>).
-define(YELLOW_BG, <<"43">>).
-define(BLUE_BG, <<"44">>).
-define(MAGENTA_BG, <<"45">>).
-define(CYAN_BG, <<"46">>).
-define(WHITE_BG, <<"47">>).
-define(DEFAULT_BG, <<"49">>).

%% RGB
-define(RGB_FG, [<<"38">>, ?SEP, <<"5">>]).
-define(RGB_BG, [<<"48">>, ?SEP, <<"5">>]).

%% True 24-bit colors
-define(TRUE_COLOR_FG, [<<"38">>, ?SEP, <<"2">>]).
-define(TRUE_COLOR_BG, [<<"48">>, ?SEP, <<"2">>]).

black(Text)      -> [color(?BLACK),      Text, reset()].
blackb(Text)     -> [colorb(?BLACK),     Text, reset()].
red(Text)        -> [color(?RED),        Text, reset()].
redb(Text)       -> [colorb(?RED),       Text, reset()].
green(Text)      -> [color(?GREEN),      Text, reset()].
greenb(Text)     -> [colorb(?GREEN),     Text, reset()].
yellow(Text)     -> [color(?YELLOW),     Text, reset()].
yellowb(Text)    -> [colorb(?YELLOW),    Text, reset()].
blue(Text)       -> [color(?BLUE),       Text, reset()].
blueb(Text)      -> [colorb(?BLUE),      Text, reset()].
magenta(Text)    -> [color(?MAGENTA),    Text, reset()].
magentab(Text)   -> [colorb(?MAGENTA),   Text, reset()].
cyan(Text)       -> [color(?CYAN),       Text, reset()].
cyanb(Text)      -> [colorb(?CYAN),      Text, reset()].
white(Text)      -> [color(?WHITE),      Text, reset()].
whiteb(Text)     -> [colorb(?WHITE),     Text, reset()].
on_black(Text)   -> [color(?BLACK_BG),   Text, reset_bg()].
on_red(Text)     -> [color(?RED_BG),     Text, reset_bg()].
on_green(Text)   -> [color(?GREEN_BG),   Text, reset_bg()].
on_blue(Text)    -> [color(?BLUE_BG),    Text, reset_bg()].
on_yellow(Text)  -> [color(?YELLOW_BG),  Text, reset_bg()].
on_magenta(Text) -> [color(?MAGENTA_BG), Text, reset_bg()].
on_cyan(Text)    -> [color(?CYAN_BG),    Text, reset_bg()].
on_white(Text)   -> [color(?WHITE_BG),   Text, reset_bg()].

rgb(RGB, Text) ->
  [?ESC, ?RGB_FG, ?SEP, rgb_color(RGB), ?END, Text, reset()].

on_rgb(RGB, Text) ->
  [?ESC, ?RGB_BG, ?SEP, rgb_color(RGB), ?END, Text, reset_bg()].

true(RGB, Text) ->
  [?ESC, ?TRUE_COLOR_FG, ?SEP, true_color(RGB), ?END, Text, reset()].

on_true(RGB, Text) ->
  [?ESC, ?TRUE_COLOR_BG, ?SEP, true_color(RGB), ?END, Text, reset()].

%% Internal
color(Color) ->
  <<?ESC/binary, Color/binary, ?END/binary>>.

colorb(Color) ->
  <<?ESC/binary, Color/binary, ?SEP/binary, ?BOLD/binary, ?END/binary>>.

rgb_color([R, G, B]) when R >= 0, R =< 5, G >= 0, G =< 5, B >= 0, B =< 5 ->
  integer_to_list(16 + (R * 36) + (G * 6) + B).

true_color([R1, R2, G1, G2, B1, B2]) ->
  R = erlang:list_to_integer([R1, R2], 16),
  G = erlang:list_to_integer([G1, G2], 16),
  B = erlang:list_to_integer([B1, B2], 16),
  true_color([R, G, B]);

true_color([R, G, B]) when R >= 0, R =< 255, G >= 0, G =< 255, B >= 0, B =< 255 ->
  [integer_to_list(R), ?SEP, integer_to_list(G), ?SEP, integer_to_list(B)].

reset() ->
  <<?ESC/binary, ?RST/binary, ?END/binary>>.

reset_bg() ->
  <<?ESC/binary, ?DEFAULT_BG/binary, ?END/binary>>.
