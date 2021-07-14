# RPICommander

Recently i found myself with 10+ RPIs for various projects and decided to create one tool too rule them all

this tool can run commands automatically on your device/s from a customized list of commands and devices.

*putty must be installed in order to use this tool*

you can download putty from here: https://www.putty.org/

features:*

*you can easily add and remove commands and devices.

done with C# winforms.

ENJOY! 


set of example commands: 
Update and Upgrade sudo apt-get update && sudo apt-get upgrade -y
Reboot sudo reboot
Xscreenaver sudo apt-get install xscreensaver -y
Samba sudo apt-get install samba samba-common-bin -y && sudo nano /etc/samba/smb.conf
Pi-Hole curl -sSL https://install.pi-hole.net | bash
PiVPN curl -L https://install.pivpn.io | bash
Change Hostname sudo nano /etc/hostname
ip to static sudo nano /etc/dhcpcd.conf
ip to dhcp sudo nano /etc/dhcpcd.conf
gravity update pihole -g
pihole update pihole -up
add vpn user pivpn -add
reset pihole pass sudo pihole -a -p
