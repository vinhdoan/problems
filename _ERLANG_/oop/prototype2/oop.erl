-module(oop).
-compile(export_all).

%% oop.erl as an interface
%%   Public APIs:
%%   - new(Class)
%%   - new(Class, Parameters)
%%      new object-process, call constructor callback function
%%      return a #Ref associated with the object-process
%%   - call(Object, Method, Arguments)
%%      Object is a #Ref indicating object-process
%%   - delete(Class)
%%      remove object, call destructor
%% oop.erl as a public behavior
%%   Callback functions:
%%   - base
%%      return list of base classes
%%   - constructor
%%      create internal data structure of object-process
%%      [{private,   _},
%%       {protected, _},
%%       {public,    _}]
%%   - destructor
%%   - handler
%%
%% oopImpl.erl as oop behavior implementation
%%
%%
%%
%% A class is defined in an module, whose behavior set to 'oop'
