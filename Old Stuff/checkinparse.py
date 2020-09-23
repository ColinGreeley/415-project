import json
import os

def cleanStr4SQL(s):
    if "'" in s:
        s = s.replace("'", "`")
    if "\n" in s:
        s = s.replace("\n", " ")
    return s

def divideCheckin():
    infile=open('./sql_files/checkin.sql','r')
    outfile1=open('./sql_files/checkin1.sql','w')
    outfile2=open('./sql_files/checkin2.sql','w')
    outfile3=open('./sql_files/checkin3.sql','w')
    outfile4=open('./sql_files/checkin4.sql','w')

    outfile1.write("INSERT INTO Checkin(business_id,year,month,day,hour,minute,second) Values\n")
    outfile2.write("INSERT INTO Checkin(business_id,year,month,day,hour,minute,second) Values\n")
    outfile3.write("INSERT INTO Checkin(business_id,year,month,day,hour,minute,second) Values\n")
    outfile4.write("INSERT INTO Checkin(business_id,year,month,day,hour,minute,second) Values\n")

    line=infile.readline()
    line=infile.readline()


    for i in range(0,900000):
        outfile1.write(line)
        line=infile.readline()
    for i in range(0,900000):
        outfile2.write(line)
        line=infile.readline()
    for i in range(0,900000):
        outfile3.write(line)
        line=infile.readline()
    
    while(line):
        outfile4.write(line)
        line=infile.readline()
    
    infile.close()
    outfile1.close()
    outfile2.close()
    outfile3.close()
    outfile4.close()


def sqlformatterV2(filename,outname):
    outfile=open(outname,'w')
    j=0
    with open(filename,'r') as f:
        line=f.readline()
        line2=f.readline()
    
        while line2:
            j+=1
           
            outfile.write(line)
            line=line2
            line2=f.readline()          

            
    f.close()
    os.remove(filename)

    i=len(line)-1
    while i>0:
        if line[i]==',':
            line=line[0:i]+';'
            outfile.write(line)
            outfile.close()
            return

        i-=1


def parseCheckinData():

    #write code to parse yelp_checkin.JSON
    with open('./yelp_checkin.JSON','r') as f: 
        outfile = open('./sql_files/checkin.sql', 'w')
        line = f.readline()
        count_line = 0
        count = 1
        data = json.loads(line)
        outfile.write("INSERT INTO Checkin(business_id,year,month,day,hour,minute,second) Values\n")
        while line: # looping through each line in the JSON file
            data = json.loads(line)
            business_id = str(cleanStr4SQL(data['business_id']))
            for item in data['date'].split(","): #get the tuple of the whole check-in
                (date,time) = item.split(" ") # gets the tuple of the date and time of a check-in
                (year,month,day) = date.split("-") #gets the date with year, month and day separated
                (hour,minute,sec)=time.split(":")
                outfile.write("(\'"+business_id+"\'," + year + "," + month + "," + day  +  "," +hour+ ","+minute+","+sec+"),\n") #create a tuple to the checkin text file
            line = f.readline() #read next line
            count_line += 1 #increment count line  
            count += 1  #counting 
    print(count_line) #printing the number of lines for the whole JSON file
    outfile.close() 
    f.close()


    pass


parseCheckinData()

divideCheckin()

sqlformatterV2('sql_files/checkin1.sql','sql_files/checkinPt1.sql')
sqlformatterV2('sql_files/checkin2.sql','sql_files/checkinPt2.sql')
sqlformatterV2('sql_files/checkin3.sql','sql_files/checkinPt3.sql')
sqlformatterV2('sql_files/checkin4.sql','sql_files/checkinPt4.sql')


