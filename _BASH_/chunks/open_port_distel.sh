#!/bin/sh

username=xtuatra
mylmwp=esekiws5426.rnd.ki.sw.ericsson.se
echo $username $mylmwp
sudo iptables -t nat -A OUTPUT -p tcp --dport 30000 -d $mylmwp -j DNAT --to-destination 127.0.0.1:30000;
sudo iptables -t nat -A OUTPUT -p tcp --dport 30001 -d $mylmwp -j DNAT --to-destination 127.0.0.1:30001;
sudo iptables -t nat -A OUTPUT -p tcp --dport 4369 -d $mylmwp -j DNAT --to-destination 127.0.0.1:2222;

ssh -L 2222:localhost:4369 -L 30000:localhost:30000 -L 30001:localhost:30001 -N -o GatewayPorts=yes $username@$mylmwp;
