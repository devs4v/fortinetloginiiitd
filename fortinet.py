import urllib
import urllib2
from bs4 import BeautifulSoup
import re
from time import sleep

#Fill in your username here
username = ""
#Fill in your password here
password = ""
if(username == "" or password == ""):
	print "No username or password, buddy! You Shall Not Pass!\n Just edit the python file at Line #8 (username) and #10 (password) and you're good to go!"
	print "Exiting in 5 seconds"
	sleep(5)
	exit()

fortinetURL = "https://192.168.1.99:1003/"
checkURL = "http://en.wikipedia.org"
checkURLOkayString = "Wikipedia, the free encyclopedia"
timeToCheck = 60
intervalsBetweenRefresh = 10
refreshLink = "" # Refresh link...this is filled in after the first login succeeds

def checkConnection():
	#will check if the connection is okay or not
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
	refreshLink = matches[0]

def refreshPage():
	#open the page with the refreshLink
	#reset timer (perhaps)
	count = 1
	while True:
		if checkConnection() == 0:
			print "Not connected! Logging you in,", username
			submitForm()
			count = 1
		else:
			if count > intervalsBetweenRefresh:
				page = urllib2.urlopen(refreshLink)
				print "Refreshed your connection! Back to Napping!"
				count= 1
			else:
				count = count + 1
			print "You're connected! I'm sleeping!"
			sleep(timeToCheck)

#start
refreshPage()