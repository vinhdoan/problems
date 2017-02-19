#!/bin/bash
while read x; do
    echo -n `date +%d/%m\ %H:%M:%S`;
    echo -n " ";
    echo $x;
done