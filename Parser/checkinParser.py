import json
import os
import time


def cleanStr4SQL(s):
    if "'" in s:
        s = s.replace("'", "`")
    if "\n" in s:
        s = s.replace("\n", " ")
    return s

def getFileLineCount(fileName):
    infile = open(fileName+".sql", 'r')
    lineCount = 0

    # Skips the first line "INSERT INTO ...."
    line = infile.readline()
    # gets the first line
    line = infile.readline()
    while(line):
        lineCount += 1
        line = infile.readline()
    infile.close()
    return lineCount

def divideCheckin(fileAmount):
    fileName = '../ParsedData/Checkin/checkin'

 
    infile = open(fileName+".sql", 'r')
    lineCount=totalLines

    linesPerFile = int(lineCount/fileAmount)

    # skips first line: "Insert Into ...."
    infile.readline()

    for currentFile in range(0, fileAmount-1):
        outfile = open('../ParsedData/Checkin/checkin'+str(currentFile)+'.sql', 'w')
        outfile.write(
            "INSERT INTO Checkin(business_id,year,month,day,hour,minute,second) Values\n")
        for LineNum in range(linesPerFile*currentFile, linesPerFile*(currentFile+1)-1):
            line = infile.readline()
            outfile.write(line)
        outfile.close()

    # final file incase there wasnt an even division
    outfile = open('../ParsedData/Checkin/checkin'+str(fileAmount-1)+'.sql', 'w')
    outfile.write(
        "INSERT INTO Checkin(business_id,year,month,day,hour,minute,second) Values\n")
    line = infile.readline()
    while(line):
        outfile.write(line)
        line = infile.readline()
    outfile.close()
    infile.close()
    for currentFile in range(0, fileAmount):
        sqlformatterV2('../ParsedData/Checkin/checkin'+str(currentFile)+'.sql',
                       '../ParsedData/Checkin/checkinPt'+str(currentFile)+'.sql')

def sqlformatterV2(filename, outname):
    outfile = open(outname, 'w')
    j = 0
    with open(filename, 'r') as f:
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

def parseCheckinData():
    totalLines=0
    # write code to parse yelp_checkin.JSON
    with open("../Data/yelp_academic_dataset_checkin.json", "r") as f:
        outfile = open('../ParsedData/Checkin/checkin.sql', 'w')
        line = f.readline()
        
        data = json.loads(line)
        outfile.write(
            "INSERT INTO Checkin(business_id,year,month,day,hour,minute,second) Values\n")
        while line:  # looping through each line in the JSON file

            data = json.loads(line)
            business_id = str(cleanStr4SQL(data['business_id']))
            # get the tuple of the whole check-in
            for item in data['date'].split(", "):
                totalLines+=1

                # gets the tuple of the date and time of a check-in
                (date, time) = item.split(" ")
                # gets the date with year, month and day separated
                (year, month, day) = date.split("-")
                (hour, minute, sec) = time.split(":")
                outfile.write("(\'"+business_id+"\'," + year + "," + month + "," + day + "," +
                              hour + ","+minute+","+sec+"),\n")  # create a tuple to the checkin text file
            line = f.readline()  # read next line
        
    outfile.close()
    f.close()
    print("checkin Total Lines:" +sum(totalLines))
    return totalLines

start=time.time()
totalLines=parseCheckinData()
divideCheckin(40)
end=time.time()

elapsed=end-start
print("elapsedtime: "+str(elapsed))
