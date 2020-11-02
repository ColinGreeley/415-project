

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
  

def friendParser():
    infile=open("../Data/friends.csv","r")
    outfile=open('../ParsedData/Friend/friends.sql', 'w', encoding="latin-1")
    linecount=0
    percentage=0
    ##gets rid of header
    line=infile.readline()
    #gets first line
    line=infile.readline()

    while(line):
        friendsSplit=line.split(',')
        friend2=friendsSplit[1].split('\n')[0]
        outfile.write("INSERT INTO Friends (user_id,friend_id) VALUES ")
        
        outfile.write('(\''+friendsSplit[0]+'\',\''+friend2+'\');\n')
        line=infile.readline()
        linecount+=1
        if(linecount%200000==0):
            percentage+=1
            print(percentage)
    
    infile.close()
    outfile.close()
    return linecount

friendLines=friendParser()
divideFile(int(friendLines/300000),'../ParsedData/Friend/friends',"INSERT INTO Users (average_stars,cool,fans,funny,name,tipcount,usefull,user_id,yelping_since,total_likes) VALUES ",friendLines)
