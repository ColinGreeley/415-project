import json
import os
import time

businessLines=0
hoursLines=0
categoriesLines=0
attributesLines=0

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
    with open('../Data/yelp_academic_dataset_business.json', 'r',encoding="latin-1") as f:
        outfile = open('../ParsedData/Business/business.sql', 'w',encoding="latin-1")
        outfileCat = open('../ParsedData/Category/categories.sql', 'w',encoding="latin-1")
        outfileAtt = open('../ParsedData/Attribute/attributes.sql', 'w',encoding="latin-1")
        outfileHours = open('../ParsedData/Hour/hours.sql', 'w',encoding="latin-1")

       
        outfile.write("INSERT INTO Business (business_id,name,city,state,zipcode,latitude,longitude,address,numTips,is_open,stars,numCheckins ) VALUES ")
        outfileCat.write("INSERT INTO Categories (business_id,category) VALUES")
        outfileAtt.write("INSERT INTO Attributes(business_id,attribute_key,attribute) VALUES")
        outfileHours.write("INSERT INTO Hours(business_id,day,open_time,close_time) VALUES")

        line = f.readline()
        businessCount=0
        categoryCount=0
        attributeCount=0
        hourCount=0

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

            category = data['categories']
            if(category):          
                categories=category.split(',')      
                for c in categories:
                    categoryCount+=1
                    outfileCat.write('(\''+cleanStr4SQL(data["business_id"])+'\',\''+cleanStr4SQL(c)+'\')')
                    outfileCat.write(',\n')
                    

            attributes = data['attributes']
            if(attributes):
                for keys in attributes.keys():
                    attributeCount+=1
                    if type(attributes[keys]) == dict:
                        recurparse(data["business_id"], attributes[keys], outfileAtt)
                    else:
                        outfileAtt.write('(\''+data["business_id"]+'\',\''+cleanStr4SQL(keys)+'\',\''+cleanStr4SQL(attributes[keys])+'\'),\n')

            hours = data['hours']
            if(hours):
                for h in hours.keys():
                    hourCount+=1
                    hou = hours[h].split('-')
                    outfileHours.write('(\''+data["business_id"]+'\',\''+h+'\',\''+hou[0]+'\',\''+hou[1]+'\'),\n')

            outfile.write("),\n")
            line = f.readline()
            businessCount += 1
    return (businessCount,categoryCount,attributeCount,hourCount)
    outfile.close()
    outfileCat.close()
    outfileAtt.close()
    outfileHours.close()

    f.close()

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
    for currentFile in range(0, fileAmount):
        sqlformatterV2(fileName+str(currentFile)+'.sql',
                       fileName+'Pt'+str(currentFile)+'.sql')

def runBusinessParser():
    start=time.time()
    businessLines,categoriesLines,attributesLines,hoursLines= parseBusinessData()
    print("businessLines:"+str(businessLines))
    print("categoryLines:"+str(categoriesLines))
    print("attributeLines:"+str(attributesLines))
    print("hourLines:"+str(hoursLines))
    divideFile(int(businessLines/300000),'../ParsedData/Business/business',"INSERT INTO Business (business_id,name,city,state,zipcode,latitude,longitude,address,numTips,is_open,stars,numCheckins ) VALUES " ,businessLines)
    divideFile(int(attributesLines/300000),'../ParsedData/Attribute/attributes',"INSERT INTO Attributes(business_id,attribute_key,attribute) VALUES" ,attributesLines)
    divideFile(int(categoriesLines/300000),'../ParsedData/Category/categories', "INSERT INTO Categories (business_id,category) VALUES" ,categoriesLines)
    divideFile(int(hoursLines/300000),'../ParsedData/Hour/hours', "INSERT INTO Hours(business_id,day,open_time,close_time) VALUES" ,hoursLines)
    end=time.time()
    print("business Parser Time: "+ str(end-start))






