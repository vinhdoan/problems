/* Example form for web browser:
<form action="/cgi-bin/cpp_radiobutton.cgi" 
   method="post" 
   target="_blank">
   <input type="radio" name="subject" value="maths" 
                                    checked="checked"/> Maths 
   <input type="radio" name="subject" value="physics" /> Physics
   <input type="submit" value="Select Subject" />
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
   Cgicc formData;  // CGI Lib: ftp://ftp.gnu.org/gnu/cgicc/
  
   cout << "Content-type:text/html\r\n\r\n";
   cout << "<html>\n";
   cout << "<head>\n";
   cout << "<title>Radio Button Data to CGI</title>\n";
   cout << "</head>\n";
   cout << "<body>\n";

   form_iterator fi = formData.getElement("subject");  
   if( !fi->isEmpty() && fi != (*formData).end()) {  
      cout << "Radio box selected: " << **fi << endl;  
   }
  
   cout << "<br/>\n";
   cout << "</body>\n";
   cout << "</html>\n";
   
   return 0;
}