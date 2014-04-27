import urllib
import urllib2
from BeautifulSoup import BeautifulSoup
import re
from time import sleep
import gc
import ctypes
import time
import sys

class bcolors:
    HEADER = '\033[95m'
    OKBLUE = '\033[94m'
    OKGREEN = '\033[92m'
    WARNING = '\033[93m'
    FAIL = '\033[91m'
    ENDC = '\033[0m'

############Settings
#Fill in your username here
username = "prafiles"
#Fill in your password here
password = "pradmin"
#time in seconds, between each check
timeToCheck = 60
#time in minutes, between each refresh
intervalsBetweenRefresh = 30
#verbosity level. Set 0 to remove all output.
#Only the console title will show the status.
#Or you can type "python fortinet.py --noverbose" from the command line, without the quotes.
verbose = 1

##############DO NOT CHANGE ANYTHING AFTER THIS LINE

def userMesg(msg):
	print bcolors.OKBLUE + msg + bcolors.ENDC

userMesg("Fortinet Login")

if(username == "" or password == ""):
	print "No username or password, buddy! You Shall Not Pass!\n Just edit the python file at Line #12 (username) and #14 (password) and you're good to go!"
	print "Exiting in 5 seconds"
	sleep(5)
	exit()

fortinetURL = "https://192.168.1.99:1003/"
checkURL = "http://en.wikipedia.org"
checkURLOkayString = "Wikipedia, the free encyclopedia"

refreshLink = "" # Refresh link...this is filled in after the first login succeeds

page = ""
soup = ""
try:
	if sys.argv[1] == "--noverbose":
		verbose = 0
except IndexError, e:
	pass
	
	
def checkConnection():
	#will check if the connection is okay or not
	global page
	global soup

	page = urllib2.urlopen(checkURL)
	soup = BeautifulSoup(page.read())

	if soup.title.string != checkURLOkayString:
		return 0
	else:
		return 1
	#try to open wikipedia
	#if opens, return true
	#if not, then submitform

def submitForm():
	#create submit params
	#open link to submit request
	#get the refresh link
	global page
	global soup

	page = urllib2.urlopen(checkURL)
	soup = BeautifulSoup(page.read())

	magic =  soup.input.input['value']	#Get the magic value i.e. the token
	#prepare the POST parameters
	params = urllib.urlencode({'username' : username, 'password' : password, 'magic' : magic, '4Tredir' : checkURL})
	#post login
	page = urllib2.urlopen(fortinetURL, params)

	soup = BeautifulSoup(page.read())

	thescript = soup.script.string
	#get the refresh url
	matches=re.findall(r'\"(.+?)\"',thescript)
	#print matches[0]
	global refreshLink
	refreshLink = matches[0]


def refreshPage():
	#open the page with the refreshLink
	#reset timer (perhaps)
	count = 1
	global refreshLink
	global page
	global soup
	while True:
		if checkConnection() == 0:
			userMesg("Fortinet Login: Not connected")
			if verbose == 1:
				print time.strftime("%c"), "Not connected! Logging you in,", username
			submitForm()
			gc.collect()
			count = 1
		else:
			if count > intervalsBetweenRefresh:
				userMesg("Fortinet Login: Refreshing Connection")
				#print "refreshLink is :", refreshLink
				page = urllib2.urlopen(refreshLink)
				if verbose == 1:
					print time.strftime("%c"), "Refreshed your connection! Back to Napping!"
				count= 1
			else:
				count = count + 1
			gc.collect()
			userMesg("Fortinet Login: Connected")
			if verbose == 1:
				print time.strftime("%c"), "You're connected! I'm sleeping!"
			sleep(timeToCheck)

#start
refreshPage()
