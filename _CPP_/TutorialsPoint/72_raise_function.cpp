#include <iostream>
#include <csignal>
#include <cstdlib>
#include <unistd.h>

using namespace std;

void signalHandler( int signum ) {
   cout << "Interrupt signal (" << signum << ") received.\n";

   // cleanup and close up stuff here  
   // terminate program  

   exit(signum);  // #include <cstdlib>

}

int main () {
   int i = 0;
   // register signal SIGINT and signal handler  
   signal(SIGINT, signalHandler);  // #include <csignal>

   while(++i){
      cout << "Going to sleep...." << endl;
		
      if( i == 3 ){
         raise( SIGINT);    // #include <csignal>
      }
		
      sleep(1);     // #include <unistd.h>
   }

   cin.get();
   return 0;
}