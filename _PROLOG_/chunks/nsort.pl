% Tính hữu hiệu:
% - Sinh ra tất cả các hoán vị, ktra hoán vị nào đc sắp xếp
% - Số phần tử lớn -> ảnh hưởng thời gian thực thi
% - Các phần tử trùng lắp -> tạo ra nhiều kết quả trùng nhau
nsort(X,Y):- permutation(X,Y), sorted(Y).
sorted([]).
sorted([X]).
sorted([X, Y| Z]):- X =< Y, sorted([Y| Z]).
permutation([],[]).
permutation(L,[X|P]):- del(X, L, L1), permutation(L1, P).
del( X, [X|Tail], Tail).
del(X,[Y|Tail], [Y|Tail1]):- del(X, Tail, Tail1).
