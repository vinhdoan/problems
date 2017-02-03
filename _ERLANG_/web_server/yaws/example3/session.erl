-module(session).
-compile(export_all).
-include("yaws_api.hrl").
-import(yaws_api, [f/2]).

-record(myopaque, {udata,
                   times = 0,
                   foobar}).
out(A) ->
    H = A#arg.headers,
    C = H#headers.cookie,
    case yaws_api:find_cookie_val("baz", C) of
        [] ->
            M = #myopaque{},
            Cookie = yaws_api:new_cookie_session(M),
            Data = {ehtml,
                    {html,[],
                     ["I just set your cookie to ", Cookie, "<br>Click ",
                      {a, [{href,"session1.yaws"}], " here "},
                      "to revisit"]}},
            CO = yaws_api:setcookie("baz",Cookie,"/"),
            [Data, CO];
        Cookie ->case yaws_api:cookieval_to_opaque(Cookie) of
                     {ok, OP} ->
                         OP2 = OP#myopaque{times = OP#myopaque.times + 1},
                         yaws_api:replace_cookie_session(Cookie, OP2),
                         Data = {ehtml,
                                 {html,[],
                                  [
                                   "Click ",
                                   {a, [{href,"session1.yaws"}], " here "},
                                   "to revisit",
                                   {p, [], f("You have been here ~p times",
                                   [OP2#myopaque.times])},
                                   {p, [], f("Your cookie is ~s", [Cookie])}]}},
                         Data;
                     {error, no_session} ->
                         new_session()
                 end
    end.
new_session() ->
    M = #myopaque{},
    Cookie = yaws_api:new_cookie_session(M),
    Data = {ehtml,
            {html,[],
             ["I just set your cookie to ", Cookie, "Click ",
              {a, [{href,"session1.yaws"}], " here "},
              "to revisit"]}},
    CO = yaws_api:setcookie("baz",Cookie,"/"),
    [Data, CO].
    