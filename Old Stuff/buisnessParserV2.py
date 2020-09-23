import json

def cleanStr4SQL(s):
    if "'" in s:
        s = s.replace("'", "`")
    if "\n" in s:
        s = s.replace("\n", " ")
    
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

    with open(filename,'r') as f:
        filestr=''
        line=f.readline()
        print("woot")
        while line:
            
            filestr+=line
            line=f.readline()
    f.close()
    outfile=open(filename,'w')
    i=len(filestr)-1
    print("searching")
    while i>0:
        if filestr[i]==',':
            filestr=filestr[0:i]+';'
            outfile.write(filestr)
            return

        i-=1

                


def parseBusinessData():

    # read the JSON file
    # Assumes that the data files are available in the current directory. If not, you should set the path for the yelp data files.
    with open('c:/Users/pauld/OneDrive/Desktop/cpts/451/YelpProject/yelp_business.JSON', 'r') as f:
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
            outfile.write('0,')
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



parseBusinessData()
sqlformatter('./sql_files/business.sql')
sqlformatter('./sql_files/attributrs.sql')
sqlformatter('./sql_files/hours.sql')
sqlformatter('./sql_files/catagories.sql')



