#include <iostream>
using namespace std;

#define MKSTR( x ) #x

int main () {
   cout << MKSTR(HELLO C++) << endl;

   cin.get();
   return 0;
}