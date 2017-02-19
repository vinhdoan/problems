%% Ktra xem có thành phần nào chưa gán trị trong Term (return "no") hay ko (return "yes")
ground(Term) :- nonvar(Term), atomic(Term).
ground(Term) :- nonvar(Term), functor(Term, F, N), ground_N(N, Term).
ground_N(0,_).
ground_N(N, Term) :- N > 0, arg(N, Term, Arg), ground(Arg), N1 is N - 1, 
                                      ground_N(N1, Term).

%% Thực thi hàm P cho tất cả các phần tử X thuộc list
%% trả về yes nếu tất cả các kết quả là yes
%% bị gián đoạn nếu gặp kết quả no
applist(_,[]).
applist(P,[X|L]):-
              Q =..[P,X], call(Q),
              applist(P,L).
