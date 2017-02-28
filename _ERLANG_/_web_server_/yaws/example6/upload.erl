-module(upload).
-compile(export_all).

-include("yaws_api.hrl").
-record(upload, {file_handle,
		 filename,
		 last}).

-define(DIR, "/tmp/").

out(Arg) ->
    out(Arg, method(Arg)).

out(_Arg, 'GET') ->
    {ehtml,
     [{html,[],
       [{body,[],[
		  {p,[],"Hello, pick a file to upload"},
		  {form,
		   [{action, ""}, {method, "post"},
		    {enctype, "multipart/form-data"}],
		   [{input, [{type, "file"}, {name, "file"},
			     {multiple, ""}], []},
		    {br, [], []},
		    {input, [{type, "submit"}, {value, "Upload"}], []}]}
		 ]

	}]}]};

out(Arg, 'POST') when Arg#arg.state == undefined ->
    State = #upload{},
    multipart(Arg, State);
out(Arg, 'POST') ->
    multipart(Arg, Arg#arg.state);
out(_, Method) ->
    err("Method " ++ atom_to_list(Method) ++ " not allowed.").

err(Err) ->
    {ehtml,
     {p, [], "error: " ++ Err}}.

multipart(Arg, State) ->
    Parse = yaws_api:parse_multipart_post(Arg),
    case Parse of
	[] ->
	    ok;
	{cont, Cont, Res} ->
	    case addFileChunk(Arg, Res, State) of
		{done, Result} ->
		    Result;
		{cont, NewState} ->
		    {get_more, Cont, NewState}
	    end;
	{result, Res} ->
	    case addFileChunk(Arg, Res, State#upload{last = true}) of
		{done, Result} ->
		    Result;
		{cont, _} ->
		    err("wrong addFileChunk result.")
	    end
    end.

addFileChunk(Arg, [{part_body, Data} | Res], State) ->
    addFileChunk(Arg, [{body, Data} | Res], State);
addFileChunk(_Arg, [], State) when State#upload.last == true,
				   State#upload.filename /= undefined,
				   State#upload.file_handle /= undefined ->
    file:close(State#upload.file_handle),
    Res = {ehtml,
	   {p, [], "File(s) uploaded successfully"}},
    {done, Res};
addFileChunk(_Arg, [], State) when State#upload.last == true ->
    {done, err("No filename or file hanlder.")};
addFileChunk(_Arg, [], State) ->
    {cont, State};
addFileChunk(Arg, [{head, {_Name, Opts}} | Res], State) ->
    case lists:keysearch("filename", 1, Opts) of
	{value, {_, Fname0}} ->
	    Fname = yaws_api:sanitize_file_name(basename(Fname0)),
	    file:make_dir(?DIR),
	    case file:open([?DIR, Fname], [write]) of
		{ok, Fd} ->
		    S2 = State#upload{filename = Fname,
				      file_handle = Fd},
		    addFileChunk(Arg, Res, S2);
		_Err ->
		    {done, err("Cannot open file.")}
	    end;
	false ->
	    %% needs handle this case
	    {done, err("Filename(s) not found.")}
    end;
addFileChunk(Arg, [{body, Data} | Res], State) when State#upload.filename /= undefined ->
    case file:write(State#upload.file_handle, Data) of
	ok ->
	    addFileChunk(Arg, Res, State);
	_Err ->
	    {done, err("Cannot write file.")}
    end.

basename(FilePath) ->
    case string:rchr(FilePath, $\\) of
	0 ->
	    %% probably not a DOS name
	    filename:basename(FilePath);
	N ->
	    %% probably a DOS name, remove everything after last \
	    basename(string:substr(FilePath, N + 1))
    end.

method(Arg) ->
    Rec = Arg#arg.req,
    Rec#http_request.method.
