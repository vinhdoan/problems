-module(http_file).
-compile(export_all).

-define(DIR, "/tmp").

upload(SessionID, _Env, _Input) ->
    form_delivery(SessionID).

form_delivery(SessionID) ->
    mod_esi:deliver(SessionID, 
                    [
                     "<html><body>"
                     "<form action=\"uploaded\" enctype=\"multipart/form-data\" method=\"post\">"
                     %% "<form action=\"../http_sv/result\" enctype=\"multipart/form-data\" method=\"post\">"
                     "File<br>"
                     "<input type=\"file\" name=\"fileToUploadName\" id=\"fileToUploadId\" multiple><br>"
                     "<input type=\"submit\" value=\"Submit\">"
                     "</form>"
                     "</body></html>"
                    ]).

uploaded(SessionID, Env, Input) ->
    HttpContentType = proplists:get_value(http_content_type, Env),
    HCTTokens = string:tokens(HttpContentType, "; "),
    Boundary = get_boundary(HCTTokens),
    FilesData = parse_input(Input, Boundary),
    handle_files(SessionID, FilesData).

handle_files(_, []) ->
    ok;
handle_files(SessionID, [{FileName, FileContent} | T]) ->
    file:write_file(filename:join(?DIR, FileName), FileContent),
    mod_esi:deliver(SessionID, FileName ++ " was uploaded successfully<br>"),
    handle_files(SessionID, T).
    
parse_input(Input, Boundary) ->
    Files = re:split(Input, "--" ++ Boundary, [{return, list}, trim]) -- [[], "--\r\n"],
    apply(fun Loop([], Acc) ->
                  Acc;
              Loop([File | T], Acc) ->
                  Data = re:split(File,
                                  "\r\n.*?filename=\"(.*?)\"\r\nContent-Type:.*?\r\n\r\n((.|\r|\n)*)\r\n$", 
                                  [{return,list},trim]),
                  Loop(T, [{lists:nth(2, Data),
                            lists:nth(3, Data)} | Acc])
          end, [Files, []]).

get_boundary([]) ->
    throw({error, "No boundary found"});
get_boundary([[$b,$o,$u,$n,$d,$a,$r,$y,$= | Boundary] | _]) ->
    Boundary;
get_boundary([_H | T]) ->
    get_boundary(T).
