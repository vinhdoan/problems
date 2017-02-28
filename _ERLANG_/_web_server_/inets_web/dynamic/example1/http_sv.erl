-module(http_sv).
-compile(export_all).

result(SessionID, Env, Input) ->
%%% Send HTTP payload without modifying any HTTP headers
    %% Data = io_lib:format("~p<br>~p<br>~p", [Env, Input, SessionID]),
    %% mod_esi:deliver(SessionID, Data).

%%% It's possible to separate the HTML content to be sent
    %% Data1 = io_lib:format("~p<br>", [Env]),
    %% mod_esi:deliver(SessionID, Data1),
    %% Data2 = io_lib:format("~p<br>", [Input]),
    %% mod_esi:deliver(SessionID, Data2),
    %% Data3 = io_lib:format("~p<br>", [SessionID]),
    %% mod_esi:deliver(SessionID, Data3).

%%% Send HTTP payload, with 1 modified HTTP header and 1 added HTTP header
    mod_esi:deliver(SessionID, [
                                "Content-Type: text/html\r\nMath-Phi: Tuan Nhat Tran\r\n\r\n", %% \r\n\r\n to mark the end of HTTP header
                                "<html><body>"
                                "Hello, World!<br><br>"
                                %% "SessionID: " ++ io_lib:format("~p<br><br>", [SessionID]) ++
                                %%     "Env Var: " ++ io_lib:format("~p<br><br>", [Env]) ++
                                %%     "Input Data: " ++ io_lib:format("~p<br>", [Input]) ++
                                %%     "</body></html>"
                               ]),
    mod_esi:deliver(SessionID,
                    ["One-More-Header: mathandphi\r\n\r\n", %% This HTTP header will not be accepted
                     "~pSessionID: " ++ io_lib:format("~p<br><br>", [SessionID]) ++ %% This is to show that HTML content can be separated
                         "Env Var: " ++ io_lib:format("~p<br><br>", [Env]) ++
                         "Input Data: " ++ io_lib:format("~p<br>", [Input]) ++
                         "</body></html>"]).

