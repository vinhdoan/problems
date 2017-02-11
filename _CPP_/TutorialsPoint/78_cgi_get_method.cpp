/* Access from web browser:
    /.../78.cgi?first_name=ZARA&last_name=ALI
*/

/* Example form for web browser:
<form action="/cgi-bin/cpp_get.cgi" method="get">
First Name: <input type="text" name="first_name">  <br />
 
Last Name: <input type="text" name="last_name" />
<input type="submit" value="Submit" />
</form>
*/

#include <iostream>
#include <vector>  
#include <string>  
#include <stdio.h>  
#include <stdlib.h> 

#include <cgicc/CgiDefs.h> 
#include <cgicc/Cgicc.h> 
#include <cgicc/HTTPHTMLHeader.h> 
#include <cgicc/HTMLClasses.h>  

using namespace std;
using namespace cgicc;

int main () {
   Cgicc formData;      // CGI Lib: ftp://ftp.gnu.org/gnu/cgicc/
   
   cout << "Content-type:text/html\r\n\r\n";
   cout << "<html>\n";
   cout << "<head>\n";
   cout << "<title>Using GET and POST Methods</title>\n";
   cout << "</head>\n";
   cout << "<body>\n";

   form_iterator fi = formData.getElement("first_name");  
	
   if( !fi->isEmpty() && fi != (*formData).end()) {  
      cout << "First name: " << **fi << endl;  
   }else{
      cout << "No text entered for first name" << endl;  
   }
	
   cout << "<br/>\n";
	
   fi = formData.getElement("last_name"); 
	
   if( !fi->isEmpty() &&fi != (*formData).end()) {  
      cout << "Last name: " << **fi << endl;  
   }else{
      cout << "No text entered for last name" << endl;  
   }
	
   cout << "<br/>\n";

   cout << "</body>\n";
   cout << "</html>\n";
   
   return 0;
}