<?php
   session_start();
   
   if (isset($_SESSION['counter'])) {
      $_SESSION['counter'] = 1;
   }else {
      $_SESSION['counter']++;
   }
   
   $msg = "You have visited this page ".  $_SESSION['counter'];
   $msg .= "in this session.";
   
   echo ( $msg );
?>

<p>
   To continue  click following link <br />
   NOTE: SID will be a empty string if normal cookie is allowed by web browser
   Otherwise, SID will be defined when the session started
   <a  href = "nextpage.php?<?php echo htmlspecialchars(SID); ?>">Link</a>
</p>