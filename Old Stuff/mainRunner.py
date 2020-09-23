import json
import os

def cleanStr4SQL(s):
    if "'" in s:
        s = s.replace("'", "`")
    if "\n" in s:
        s = s.replace("\n", " ")
    if "\\" in s:
        s=s.replace("\\"," ")
    
    return s


def recurparse(id, diction, file):
    i = 0
    for key in diction.keys():
        if(type(diction[key]) == dict):
            recurparse(id, diction[key], file)
        else:
            file.write('(\''+id+'\',\''+key+'\',\''+diction[key]+'\'),\n')
            if i < len(diction)-1:
                file.write(', ')
        i += 1


def sqlformatter(filename):
    print("formatting "+ filename)
    with open(filename,'r') as f:
        filestr=''
        line=f.readline()
        while line:
            
            filestr+=line
            line=f.readline()
    f.close()
    outfile=open(filename,'w')
    i=len(filestr)-1
    while i>0:
        if filestr[i]==',':
            filestr=filestr[0:i]+';'
            outfile.write(filestr)
            return

        i-=1

                

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
    print("formatting "+ filename)

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


def parseBusinessData():

    # read the JSON file
    # Assumes that the data files are available in the current directory. If not, you should set the path for the yelp data files.
    with open('yelp_business.JSON', 'r') as f:
        outfile = open('./sql_files/business.sql', 'w')
        outfileCat = open('./sql_files/catagories.sql', 'w')
        outfileAtt = open('./sql_files/attributes.sql', 'w')
        outfileHours = open('./sql_files/hours.sql', 'w')

        line = f.readline()
        count_line = 0
        outfile.write(
            "INSERT INTO Business (business_id,name,city,state,zipcode,latitude,longitude,address,numTips,is_open,stars,numCheckins ) VALUES ")
        outfileCat.write(
            "INSERT INTO Categories (business_id,category) VALUES")
        outfileAtt.write(
            "INSERT INTO Attributes(business_id,attribute_key,attribute) VALUES")
        outfileHours.write(
            "INSERT INTO Hours(business_id,day,open_time,close_time) VALUES")
        while line:
            data = json.loads(line)

            outfile.write("(")

            outfile.write('\''+cleanStr4SQL(data["business_id"])+'\',')
            outfile.write('\''+cleanStr4SQL(data["name"])+'\',')
            outfile.write('\''+cleanStr4SQL(data["city"])+'\',')
            outfile.write('\''+cleanStr4SQL(data["state"])+'\',')
            outfile.write(str(data["postal_code"])+',')
            outfile.write(str(data["latitude"])+',')
            outfile.write(str(data["longitude"])+',')
            outfile.write('\''+cleanStr4SQL(data["address"])+'\',')
            outfile.write(str(data["review_count"])+',')
            outfile.write(str(data["is_open"])+',')
            outfile.write(str(data["stars"])+", 0")

            categories = data['categories'].split(',')
            for c in categories:
                outfileCat.write('(\''+cleanStr4SQL(data["business_id"])+'\',\''+cleanStr4SQL(c)+'\')')

                outfileCat.write(',\n')

            attributes = data['attributes']

            for keys in attributes.keys():
                if type(attributes[keys]) == dict:
                    recurparse(data["business_id"], attributes[keys], outfileAtt)
                else:
                    outfileAtt.write(     '(\''+data["business_id"]+'\',\''+cleanStr4SQL(keys)+'\',\''+cleanStr4SQL(attributes[keys])+'\'),\n')

            hours = data['hours']
            for h in hours.keys():
                hou = hours[h].split('-')
                outfileHours.write(
                    '(\''+data["business_id"]+'\',\''+h+'\',\''+hou[0]+'\',\''+hou[1]+'\'),\n')

            outfile.write("),\n")
            line = f.readline()
            count_line += 1

    print(count_line)
    outfile.close()
    outfileCat.close()
    outfileAtt.close()
    outfileHours.close()

    f.close()


def parseUserData():
  
    #read the JSON file
    with open('yelp_user.JSON','r') as f:  #Assumes that the data files are available in the current directory. If not, you should set the path for the yelp data files.
        outfile =  open('./sql_files/users.sql', 'w')
        outfileFriend =  open('./sql_files/friends.sql', 'w')
        curretID=""

        line = f.readline()
        count_line = 0
        #read each JSON abject and extract data qw
        outfile.write("INSERT INTO Users (average_stars,cool,fans,funny,name,tipcount,usefull,user_id,yelping_since,total_likes) VALUES ")
        outfileFriend.write("INSERT INTO Friends (user_id,friend_id) VALUES ")

        while line:
            data = json.loads(line)
            
            curretID=data["user_id"]

            friends=data["friends"]

            for fr in friends:
                outfileFriend.write('(\''+curretID+'\',\''+fr+'\'),\n')
            
            outfile.write('(')
            outfile.write(str(data["average_stars"])+',')
            outfile.write(str(data["cool"])+',')
            outfile.write(str(data["fans"])+',')
            outfile.write(str(data["funny"])+',')
            outfile.write('\'' +cleanStr4SQL(data["name"])+'\''+',')
            outfile.write('0,')
            outfile.write(str(data["useful"])+',')
            outfile.write('\''+data["user_id"]+'\''+',')
            outfile.write('\'' +data["yelping_since"]+'\''+',0),\n')
            count_line+=1
            line=f.readline()

    print(count_line)
    outfile.close()
    outfileFriend.close()
    f.close()


def parseTipData():
  
    # read the JSON file
    with open('yelp_tip.JSON', 'r') as file:
        outfile = open('./sql_files/tipData1.sql', 'w')
        outfile.write("INSERT INTO Tip (businessid,year,month,day,hour,minute,second,likes,tiptext,userid) VALUES ")

        # read each JSON object and extract data
        for line in file:
            data = json.loads(line)
            outfile.write("(")
            outfile.write("\'"+cleanStr4SQL(data['business_id'])+"\'" + ',')
            temp = data['date']
            (year, month, s) = temp.split('-')
            day, time = s.split(' ')
            hour, minutes, seconds = time.split(':')
            outfile.write(year+","+month+","+day+","+hour+","+minutes+","+seconds+",")
            outfile.write(str(data['likes']) + ',')
            outfile.write("\'"+cleanStr4SQL(data['text'])+"\'" + ',')
            outfile.write("\'"+cleanStr4SQL(data['user_id'])+"\'"+"),")
            outfile.write('\n')

    outfile.close()
    file.close()



parseBusinessData()
parseUserData()
parseCheckinData()

divideCheckin()

parseTipData()

sqlformatterV2('./sql_files/business.sql','./sql_files/businesses.sql')
sqlformatterV2('./sql_files/attributes.sql','./sql_files/attribute.sql')
sqlformatterV2('./sql_files/hours.sql','./sql_files/hour.sql')
sqlformatterV2('./sql_files/catagories.sql','./sql_files/catagorie.sql')
sqlformatterV2('./sql_files/users.sql','./sql_files/user.sql')
sqlformatterV2('./sql_files/friends.sql','./sql_files/friend.sql')



sqlformatterV2('sql_files/checkin1.sql','sql_files/checkinPt1.sql')
sqlformatterV2('sql_files/checkin2.sql','sql_files/checkinPt2.sql')
sqlformatterV2('sql_files/checkin3.sql','sql_files/checkinPt3.sql')
sqlformatterV2('sql_files/checkin4.sql','sql_files/checkinPt4.sql')

sqlformatterV2('sql_files/tipData1.sql','sql_files/tipData.sql')









