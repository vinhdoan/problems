<html>
   
   <head>
      <title>Passing Argument by Reference</title>
   </head>
   
   <body>
      
      <?php
         function addFive($num) {
            $num += 5;
         }
         
         function addSix(&$num) {
            $num += 6;
         }
         
         $orignum = 10;
         addFive( $orignum );
         
         echo "Original Value is $orignum<br />"; // Original Value is 10
         
         addSix( $orignum );
         echo "Original Value is $orignum<br />"; // Original Value is 16 
      ?>
      
   </body>
</html>