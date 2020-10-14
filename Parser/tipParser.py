import json
import os
import time

totalLines=0

def cleanStr4SQL(s):
    if "'" in s:
        s=s.replace("'","`")
    if "\n" in s:
        s=s.replace("\n"," ")
    if "\\" in s:
        s=s.replace("\\"," ")
    return s

def divideTips(fileAmount):
    fileName = '../ParsedData/Tip/tips'

 
    infile = open(fileName+".sql", 'r', encoding="latin-1")
    lineCount=totalLines

    linesPerFile = int(lineCount/fileAmount)

    # skips first line: "Insert Into ...."
    infile.readline()

    for currentFile in range(0, fileAmount-1):
        outfile = open(fileName+str(currentFile)+'.sql', 'w', encoding="latin-1")
        outfile.write(
            "INSERT INTO Tip (business_id,year,month,day,hour,minute,second,likes,tip_text,user_id) VALUES \n")
        for LineNum in range(linesPerFile*currentFile, linesPerFile*(currentFile+1)-1):
            line = infile.readline()
            outfile.write(line)
        outfile.close()

    # final file incase there wasnt an even division
    outfile = open(fileName+str(fileAmount-1)+'.sql', 'w', encoding="latin-1")
    outfile.write(
        "INSERT INTO Tip (business_id,year,month,day,hour,minute,second,likes,tip_text,user_id) VALUES \n")
    line = infile.readline()
    while(line):
        outfile.write(line)
        line = infile.readline()
    outfile.close()
    infile.close()
    for currentFile in range(0, fileAmount):
        sqlformatter('../ParsedData/Tip/tips'+str(currentFile)+'.sql',
                       '../ParsedData/Tip/tipsPt'+str(currentFile)+'.sql')

def sqlformatter(filename, outname):
    outfile = open(outname, 'w', encoding="latin-1")
    j = 0
    with open(filename, 'r', encoding="latin-1") as f:
        line = f.readline()
        line2 = f.readline()

        while line2:
            j += 1

            outfile.write(line)
            line = line2
            line2 = f.readline()

    f.close()
    os.remove(filename)

    i = len(line)-1
    while i > 0:
        if line[i] == ',':
            line = line[0:i]+';'
            outfile.write(line)
            outfile.close()
            return

        i -= 1

def parseTips():
    infile=open("../Data/yelp_academic_dataset_tip.json","r", encoding="latin-1")
    outfile = open('../ParsedData/Tip/tips.sql', 'w',encoding="latin-1")
    outfile.write("INSERT INTO Tip (business_id,year,month,day,hour,minute,second,likes,tip_text,user_id) VALUES ")
       
    count=0
    line=infile.readline()
    while(line):
        count+=1       
        data = json.loads(line)
        outfile.write("(")
        outfile.write("\'"+cleanStr4SQL(data['business_id'])+"\'" + ',')
        temp = data['date']
        (year, month, s) = temp.split('-')
        day, time = s.split(' ')
        hour, minutes, seconds = time.split(':')
        outfile.write(year+","+month+","+day+","+hour+","+minutes+","+seconds+",")
        outfile.write(str(data['compliment_count']) + ',')
        outfile.write("\'"+cleanStr4SQL(data['text'])+"\'" + ',')
        outfile.write("\'"+cleanStr4SQL(data['user_id'])+"\'"+"),")
        outfile.write('\n')
        line=infile.readline()

    return count

def runTipParser():
    startTime=time.time()
    totalLines=parseTips()
    divideTips(int(totalLines/300000))
    endTime=time.time()
    print("Tips Total Time:"+str(endTime-startTime))