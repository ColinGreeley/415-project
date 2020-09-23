import json


def cleanStr4SQL(s):
    if "'" in s:
        s=s.replace("'","`")
    if "\n" in s:
        s=s.replace("\n"," ")
    if "[" in s:
        s=s.replace("[","\"[")
    if "]" in s:
        s=s.replace("]","]\"")
        
    return s

def parseUserData():
  
    #read the JSON file
    with open('yelp_user.JSON','r') as f:  #Assumes that the data files are available in the current directory. If not, you should set the path for the yelp data files.
        outfile =  open('./sql_files/users.sql', 'w')
        outfileFriend =  open('./sql_files/friends.sql', 'w')
        curretID=""

        line = f.readline()
        count_line = 0
        #read each JSON abject and extract data
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


parseUserData()