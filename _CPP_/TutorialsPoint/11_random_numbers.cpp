#include <iostream>
#include <ctime> // used for time()
#include <cstdlib> // used for rand(), srand()

using namespace std;
 
int main () {
   int i,j;
 
   // set the seed
   srand( (unsigned)time( NULL ) );

   /* generate 10  random numbers. */
   for( i = 0; i < 10; i++ ) {
      // generate actual random number
      j= rand();
      cout <<" Random Number : " << j << endl;
   }

   cin.get();
   return 0;
}