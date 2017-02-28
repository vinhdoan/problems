/* Compile and execute:
    $ g++ main.cpp support.cpp -o write
    $ ./write
*/

#include <iostream>

using namespace std;

int count ;
extern void write_extern();

main() {
   count = 5;
   write_extern();
   cin.get();
}
