#!/bin/sh
echo "Chuong trinh tinh tong 1- $1"
index=0
tong=0
while [ $index -lt $1 ]
do
	index=$(($index + 1))
	tong=$(($tong + $index))
done
echo "Tong 1-$1= $tong"
exit 0
