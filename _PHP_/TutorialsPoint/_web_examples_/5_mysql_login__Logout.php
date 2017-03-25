<?php
   session_start();
   
   if(session_destroy()) {
      header("Location: 5_mysql_login__Login.php");
   }
?>