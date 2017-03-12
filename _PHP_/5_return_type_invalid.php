<?php
   // This will return "Fatal error: UncaughtTypeError:"
   declare(strict_types = 1);
   function returnIntValue(int $value): int {
      return $value + 1.0;
   }
   print(returnIntValue(5));
?>