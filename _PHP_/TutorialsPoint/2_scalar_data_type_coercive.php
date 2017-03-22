<?php
   // Coercive mode
   function sum(int ...$ints) {
      return array_sum($ints);
   }
   print(sum(2, '3', 4.1));
?>