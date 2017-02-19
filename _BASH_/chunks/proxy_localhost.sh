#!/bin/sh

username=xtuatra
mylmwp=esekiws5426.rnd.ki.sw.ericsson.se
echo $username $mylmwp

sudo iptables -t nat -A OUTPUT -p tcp --dport 8888 -d $mylmwp -j DNAT --to-destination 127.0.0.1:4444;

ssh -R 4444:localhost:8888 -o GatewayPorts=yes $username@$mylmwp
