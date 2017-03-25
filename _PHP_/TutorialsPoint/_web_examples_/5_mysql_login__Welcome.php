<?php
   include('5_mysql_login__Session.php');
?>
<html">
   
   <head>
      <title>Welcome </title>
   </head>
   
   <body>
      <h1>Welcome <?php echo $login_session; ?></h1> 
      <h2><a href = "5_mysql_login__Logout.php">Sign Out</a></h2>
   </body>
   
</html>