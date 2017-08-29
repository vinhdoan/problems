-module(ep).
-export([gen_name/0]).

gen_name() ->
    {ok, P} = python:start([{python_path, "pylib"}, {python, "python3"}]),
    R = python:call(P, fakergen, name, []),
    python:stop(P),
    R.
