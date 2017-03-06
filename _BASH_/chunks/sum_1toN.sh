#!/bin/sh

echo "Chuong trinh tinh tong 1 toi $1"
index=0
tong=0
while [ $index -lt $1 ]
do
    index=$(($index + 1))
    tong=$(($tong + $index))
done
echo "Tong 1 toi $1 la $tong"
exit 0
