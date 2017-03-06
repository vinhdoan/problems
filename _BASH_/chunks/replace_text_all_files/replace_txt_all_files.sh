#!/bin/bash

while read line; do
    for file in `find $1 -type f`; do
        sed -i -e "s/$line/g" $file
    done
done < rtaf.conf
