#!/bin/bash
#Check rights
ROOT_UID="0"
#Check if run as root
if [ "$UID" -ne "$ROOT_UID" ] ; then
	echo "You must be root to install this!"
	exit 1
fi
#install dependencies
apt-get install python-dev python-beautifulsoup

#confirm path
installpath = /etc/init.d/fortinetautologin
echo "Do you wish to change the path (Default: /etc/init.d/)?"
select yn in "Yes" "No"; do
    case $yn in
        Yes ) echo Enter the new path; read installpath; break;;
        No ) break;;
    esac
done
installpath=$installpath/fortinetautologin
#Write file to path
echo Install Path = $installpath
cat fortinet1.py > $installpath
echo "Enter username : "
read username
echo "username = \""$username"\"" >>$installpath
echo "#Fill in your password here" >>$installpath
echo "Enter password : "
read password
echo "password = \""$password"\"" >>$installpath
cat fortinet2.py >>$installpath
chmod +x $installpath
echo Start now by .$installpath
