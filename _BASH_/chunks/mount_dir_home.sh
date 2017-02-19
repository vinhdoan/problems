cd /mnt
sudo mount -t vboxsf sharefolder sharefolder
sudo mount -t vboxsf Source Source
sudo mount -t vboxsf Workspace Workspace
sudo mount -t vboxsf LocalPjs LocalPjs
cp ~/tntScripts/* /mnt/Source/LinuxScripts
