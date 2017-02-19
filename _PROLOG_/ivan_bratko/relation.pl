parent(pam, bob).
parent(tom, bob).
parent(tom, liz).
parent(bob, ann).
parent(bob, pat).
parent(pat, jim).
female(pam).
female(liz).
female(pat).
female(ann).
male(tom).
male(bob).
male(jim).
offspring(X,Y) :- parent(Y,X).
mother(X,Y) :- parent(X,Y), female(X).
father(X,Y) :- parent(X,Y), male(X).
grandparent(X,Z) :- parent(X,Y), parent(Y,Z).
sister(X,Y) :- parent(Z,X), parent(Z,Y), female(X), different(X,Y).
happy(X) :- hasachild(X).

pre(X,Y) :- parent(X,Y).
pre(X,Z) :- parent(X,Y), pre(Y,Z).

pres(X,Y) :- parent(X,Y).
pres(X,Z) :- pres(X,Y), parent(Y,Z).
