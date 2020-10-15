import json
import os
import time
userLines = 0
friendLines = 0


def cleanStr4SQL(s):
    if "'" in s:
        s = s.replace("'", "`")
    if "\n" in s:
        s = s.replace("\n", " ")
    if "[" in s:
        s = s.replace("[", "\"[")
    if "]" in s:
        s = s.replace("]", "]\"")

    return s


def parseUserData():

    # read the JSON file
    # Assumes that the data files are available in the current directory. If not, you should set the path for the yelp data files.
    with open('../Data/yelp_academic_dataset_user.json', 'r', encoding="latin-1") as f:
        outfile = open('../ParsedData/User/users.sql', 'w', encoding="latin-1")
        outfileFriend = open(
            '../ParsedData/Friend/friends.sql', 'w', encoding="latin-1")

        # read each JSON abject and extract data

        userCount = 0
        friendCount = 0
        curretID = ""

        line = f.readline()
        start1=time.time()
        while line:
            outfile.write("INSERT INTO Users (average_stars,cool,fans,funny,name,tipcount,usefull,user_id,yelping_since,total_likes) VALUES ")

            data = json.loads(line)

            curretID = data["user_id"]

            friends = data["friends"]
            if(friends):
                friendsSplit=friends.split(', ')
                for fr in friendsSplit:
                    outfileFriend.write("INSERT INTO Friends (user_id,friend_id) VALUES ")

                    friendCount+=1
                    outfileFriend.write('(\''+curretID+'\',\''+fr+'\');\n')

            outfile.write('(')
            outfile.write(str(data["average_stars"])+',')
            outfile.write(str(data["cool"])+',')
            outfile.write(str(data["fans"])+',')
            outfile.write(str(data["funny"])+',')
            outfile.write('\'' + cleanStr4SQL(data["name"])+'\''+',')
            outfile.write('0,')
            outfile.write(str(data["useful"])+',')
            outfile.write('\''+data["user_id"]+'\''+',')
            outfile.write('\'' + data["yelping_since"]+'\''+',0);\n')
            userCount += 1
            line = f.readline()

            if(userCount%20000==0):
                lap=time.time()
                print(str(userCount/20000)+ ": "+str(lap-start1))
    
    outfile.close()
    outfileFriend.close()
    f.close()
    return(userCount,friendCount)

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
  

def runUserParser():
    start=time.time()
    userLines,friendLines= parseUserData()
    print("userLines:"+str(userLines))
    print("friendLines:"+str(friendLines))
    divideFile(int(userLines/300000),'../ParsedData/User/users',"INSERT INTO Users (average_stars,cool,fans,funny,name,tipcount,usefull,user_id,yelping_since,total_likes) VALUES ",userLines)
    divideFile(int(friendLines/300000),'../ParsedData/Friend/friends',"INSERT INTO Users (average_stars,cool,fans,funny,name,tipcount,usefull,user_id,yelping_since,total_likes) VALUES ",friendLines)
    end= time.time()
    print("user time: "+str(end-start))