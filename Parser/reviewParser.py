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

def sqlformatterV2(filename, outname):
    outfile = open(outname, 'w',encoding="latin-1")
    j = 0
    with open(filename, 'r',encoding="latin-1") as f:
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

def divideFile(fileAmount,fileName,insertText,totalLines): 

    infile = open(fileName+".sql", 'r',encoding="latin-1")
    lineCount=totalLines
    if fileAmount==0:
        linesPerFile = int(lineCount/1)
    else:
        linesPerFile = int(lineCount/fileAmount)

    linesPerFile = int(lineCount/fileAmount)

    # skips first line: "Insert Into ...."
    infile.readline()

    for currentFile in range(0, fileAmount-1):
        outfile = open(fileName+str(currentFile)+'.sql', 'w',encoding="latin-1")
        outfile.write(
            insertText)
        for LineNum in range(linesPerFile*currentFile, linesPerFile*(currentFile+1)-1):
            line = infile.readline()
            outfile.write(line)
        outfile.close()

    # final file incase there wasnt an even division
    outfile = open(fileName+str(fileAmount-1)+'.sql', 'w',encoding="latin-1")
    outfile.write(insertText)
    line = infile.readline()
    while(line):
        outfile.write(line)
        line = infile.readline()
    outfile.close()
    infile.close()
   
def parseReview():
    infile=open("../Data/yelp_academic_dataset_review.json","r", encoding="latin-1")
    outfile = open('../ParsedData/Review/review.sql', 'w',encoding="latin-1")
       
    count=0
    line=infile.readline()
    while(line):
        outfile.write("INSERT INTO Review (review_id,user_id,business_id,stars,useful,funny,cool,text,day,month,year,hour,minute,second) VALUES ")

        count+=1       
        data = json.loads(line)
        outfile.write("(")
        outfile.write("\'"+cleanStr4SQL(data['review_id'])+"\'" + ',')
        outfile.write("\'"+cleanStr4SQL(data['user_id'])+"\'" + ',')
        outfile.write("\'"+cleanStr4SQL(data['business_id'])+"\'" + ',')
        outfile.write(str(data['stars']) + ',')
        outfile.write(str(data['useful']) + ',')
        outfile.write(str(data['funny'])+",")
        outfile.write(str(data['cool'])+",")
        outfile.write("\'"+cleanStr4SQL(str(data['text']))+"\'"+",")
        
        dateString = data['date']
        date,time=dateString.split(' ')

        (year, month, day) = date.split('-')

        hour, minutes, seconds = time.split(':')
        outfile.write(day+","+month+","+year+","+hour+","+minutes+","+seconds)
        outfile.write(');\n')
        line=infile.readline()

    return count

def runReviewParser():
    startTime=time.time()
    totalLines=parseReview()
    divideFile(int(totalLines/300000),"../ParsedData/Review/review","INSERT INTO Review (review_id,user_id,business_id,stars,useful,funny,cool,text,day,month,year,hour,minute,second) VALUES ",totalLines)
    endTime=time.time()
    print("Review Total Time:"+str(endTime-startTime))

