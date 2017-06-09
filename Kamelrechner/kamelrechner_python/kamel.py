import requests
import re
import sys
import os


def getcamels(payload):
    url = "http://kamelrechner.de/de/result"
    headers = {'user-agent': 'hornycamel'}
    r = requests.post(url, data=payload, headers=headers)
    if r.status_code == 200:
        m = re.search('<span class="result" >([0-9]*)</span>', r.text)
        n = int(m.group(1))
        return n
    else:
        print("Fehler: " + str(r.status_code))
        print(r.text)
        return -1


if len(sys.argv) != 3:
    print("usage: python program.py inputfile outputfile")
    sys.exit()

fi = open(sys.argv[1], "r")
fo = open(sys.argv[2], "w")

lines = fi.readlines()
keys = lines[0].strip().split(" ")
lines = lines[1:]

for line in lines:
    payload = {}
    data = line.strip().split(" ")
    for i, key in enumerate(keys):
        payload[key] = data[i]
    n = getcamels(payload)
    fo.write(str(n) + os.linesep)
