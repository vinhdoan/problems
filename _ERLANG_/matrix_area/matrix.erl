-module(matrix).
-compile(export_all).

-define(DATA_FILE, "data.dat").

calc() ->
    Matrix = get_matrix(),
    SubMatricesList =
        [lists:nthtail(Num, Matrix) || Num <- lists:seq(0, length(Matrix) - 1)],
    NoOfSubMatrices = length(SubMatricesList),
    %% Calculate & compare sub-matrices, "breaking" used
    try
        lists:foldl(
          fun(SubMatrix, {Cnt, Max}) ->
                  Val = calc_submatrix(SubMatrix),
                  NewMax = if
                               Val > Max ->
                                   Val;
                               true ->
                                   Max
                           end,
                  if
                      NewMax >= NoOfSubMatrices - Cnt ->
                          throw({break, NewMax});
                      true ->
                          {Cnt + 1, NewMax}
                  end
          end,
          {1, 0},
          SubMatricesList)
    catch
        throw:{break, Res} ->
            Res
    end.

%%% Read data from file
get_matrix() ->
    {ok, S} = file:open(?DATA_FILE, read),
    Res = get_lines(S, []),
    file:close(S),
    Res.

get_lines(S, Matrix) ->
    case io:get_line(S, '') of
        eof ->
            Matrix;
        LineStr ->
            LineList = 
                [list_to_integer(X) || X <- string:tokens(LineStr, " \n")],
            get_lines(S, Matrix ++ [LineList])
    end.

%%% Calculate a sub-matrix, "breaking" from foldl used
calc_submatrix([H | _] = SubMatrix) ->
    try
        NoOfLines = length(SubMatrix),
        {_, Res} = 
            lists:foldl(
              fun(Line, {Cnt, Acc}) ->
                      NewAcc = [X1 * X2 || {X1, X2} <- lists:zip(Line, Acc)],
                      SeqLen = longest_seq(NewAcc),
                      if
                          SeqLen == Cnt ->
                              throw({break, Cnt});
                          SeqLen < Cnt ->
                              throw({break, min(longest_seq(Acc), Cnt - 1)});
                          Cnt == NoOfLines ->
                              throw({break, Cnt});
                          true ->
                              {Cnt + 1, NewAcc}
                      end
              end,
              {1, lists:duplicate(length(H), 1)},
              SubMatrix),
        longest_seq(Res)
    catch
        throw:{break, SeqLen} ->
            SeqLen
    end.                  

%% Get length of longest sequence of 1s in a line, "breaking" not used
longest_seq(List) ->
    ListLen = length(List),
    {_, _, Res} = 
        lists:foldl(
          fun(X, {Ite, Cnt, Max}) ->
                  NewIte = Ite + 1,
                  NewCnt = Cnt + 1 * X,
                  NewMax = case {X, NewIte} of
                               {1, I} when I < ListLen ->
                                   Max;
                               {X, _} ->
                                   if NewCnt > Max ->
                                           NewCnt;
                                      true ->
                                           Max
                                   end
                           end,
                  {NewIte, NewCnt * X, NewMax}
          end,
          {0, 0, 0},
          List),
    Res.
