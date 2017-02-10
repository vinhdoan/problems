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
   // register signal SIGINT and signal handler  
   signal(SIGINT, signalHandler);  // #include <csignal>

   while(1){
      cout << "Going to sleep...." << endl;
      sleep(1);   // #include <unistd.h>
   }

   cin.get();
   return 0;
}