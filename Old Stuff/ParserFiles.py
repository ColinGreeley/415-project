import json

def cleanStr4SQL(s):
    return s.replace("'","`").replace("\n"," ")

mainList = ['business_id', 'name', 'address', 'city', 'state', 'postal_code', 'latitude',
            'longitude', 'stars', 'is_open', 'attributes', 'categories', 'hours','review_count' ]

attributesList = ['GoodForKids', 'NoiseLevel', 'RestaurantsDelivery', 'GoodForMeal', 'Alcohol', 'Caters', 'WiFi', 'RestaurantsTakeOut', 'BusinessAcceptsCreditCards', 'Ambience', 'BusinessParking', 'RestaurantsTableService', 'RestaurantsGoodForGroups', 'OutdoorSeating', 'HasTV', 'BikeParking', 'RestaurantsReservations', 'RestaurantsPriceRange2',
                  'RestaurantsAttire', 'ByAppointmentOnly', 'BYOBCorkage', 'CoatCheck', 'HappyHour', 'Smoking', 'Music', 'BestNights', 'WheelchairAccessible', 'BusinessAcceptsBitcoin', 'GoodForDancing', 'HairSpecializesIn', 'AcceptsInsurance', 'Corkage', 'BYOB', 'DogsAllowed', 'DriveThru', 'AgesAllowed', 'DietaryRestrictions', 'Open24Hours', 'RestaurantsCounterService']
parkList = ['garage', 'street', 'validated', 'lot', 'valet']
goodformealsList = ['dessert', 'latenight',
                    'lunch', 'dinner', 'brunch', 'breakfast']
ambianceList = ['romantic', 'intimate', 'touristy', 'hipster',
                'divey', 'classy', 'trendy', 'upscale', 'casual']
musicList = ['dj', 'background_music', 'jukebox',
             'live', 'video', 'karaoke', 'no_music']
bestnightsList = ['monday', 'tuesday', 'wednesay',
                  'thursday', 'friday', 'saturday', 'sunday']
hairList = ['straightperms', 'coloring', 'extensions',
            'africanamerican', 'curly', 'kids', 'perms', 'asian']
dietaryRestrictionsList = ['dairy-free', 'gluten-free',
                           'vegan', 'kosher', 'halal', 'soy-free', 'vegetarian']

hoursList = ['Monday', 'Tuesday', 'Wednesday',
             'Thursday', 'Friday', 'Saturday', 'Sunday']


def recurparse(diction, file):
    i = 0
    for key in diction.keys():
        if(type(diction[key]) == dict):
            recurparse(diction[key], file)
        else:
            file.write('('+key+','+diction[key]+') ')
            if i < len(diction)-1:
                file.write(', ')
        i += 1


def parseBusinessData():

    # read the JSON file
    # Assumes that the data files are available in the current directory. If not, you should set the path for the yelp data files.
    with open('yelp_business.JSON', 'r') as f:
        outfile = open('business.txt', 'w')

        line = f.readline()
        count_line = 0
        # read each JSON abject and extract data
        K = 0
        while line:
            K += 1
            # outfile.write(str(K)+"- BuisnessInfo: ")
            data = json.loads(line)
            j = 0
            for item in mainList:

                if item in data.keys():
                    if item=='catagories':
                        categories = data['categories'].split(', ')
                        outfile.write("\ncategories: [")
                        outfile.write(str(categories))  #category list
                    elif item=='attributes':
                        #goes through all the attributes lables them Null if not around
                        attributes=data['attributes']
                        outfile.write("\nattributes: [ ")
                       
                        i=0
                        for keys in attributes.keys():
                            if type(attributes[keys]) == dict:
                                recurparse(attributes[keys], outfile)
                            else:
                                outfile.write(
                                    '(\''+keys+'\',\''+attributes[keys]+'\') ')
                            if i < len(attributes)-1:
                                outfile.write(', ')
                            i+=1
                        outfile.write(']')

                    elif item=='hours':                        
                        hours=data['hours']
                        outfile.write("\nhours:")
                        for h in hours.keys():
                            outfile.write('(\''+h+', \''+hours[h]+'\'), ')                    
                    else:
                        outfile.write('\''+cleanStr4SQL(str(data[item]))+'\'')
                                                                    
            if j<len(mainList)-1:
                empty_list = list()
                outfile.write(str(empty_list))

                if j < len(mainList)-1:
                    outfile.write(';')
                j+=1
            outfile.write('\n')
            line = f.readline()
            count_line += 1

    print(count_line)
    outfile.close()
    f.close()

def parseUserData():
    #write code to parse yelp_user.JSON
     with open('./yelp_user.JSON','r') as f: 
        outfile =  open('./user.txt', 'w')
        line = f.readline()
        count_line = 0
        count = 1
        data = json.loads(line)

        while line: # looping through each line in the JSON file
            data = json.loads(line)
            user_id = str(cleanStr4SQL(data['user_id']))
            name = str(cleanStr4SQL(data['name']))
            yield_since = str(cleanStr4SQL(data['yelping_since']))
            useful = data['useful']
            cool = data['cool']
            funny = data['funny']
            tipcount = data['tipcount']
            fans = data['fans']
            average_stars = data['average_stars']
            friends = data['friends']


            outfile.write(str(count) + ". "+ str(user_id)+ " " + str(name) + " " + str(yield_since) +  " " + str(tipcount) + " " +  str(fans) + " " + str(average_stars) + " (" + str(funny) + ";" + str(useful) + ";" + str(cool) + " )")
            outfile.write("\n")
            outfile.write(str(friends))
            outfile.write("\n")

            line = f.readline()
            count_line += 1
            count +=1
        print(count_line)
        outfile.close()
        f.close()

def parseCheckinData():
    #write code to parse yelp_checkin.JSON
    with open('./yelp_checkin.JSON','r') as f: 
        outfile =  open('./checkin.txt', 'w')
        line = f.readline()
        count_line = 0
        count = 1
        data = json.loads(line)

        while line: # looping through each line in the JSON file
            data = json.loads(line)
            business_id = str(cleanStr4SQL(data['business_id'])) 
            outfile.write(str(count) + ". "+ business_id)
            outfile.write("\n")
            for item in data['date'].split(","): #get the tuple of the whole check-in
                (date,time) = item.split(" ") # gets the tuple of the date and time of a check-in
                (year,month,day) = date.split("-") #gets the date with year, month and day separated
                outfile.write("(" + year + " " + month + " " + day  + " " + time + ")" + "\t") #create a tuple to the checkin text file
            outfile.write("\n")      
            line = f.readline() #read next line
            count_line += 1 #increment count line  
            count += 1  #counting 
    print(count_line) #printing the number of lines for the whole JSON file
    outfile.close() 
    f.close()

#The Tip Data Parser
def parseTipData():
    #write code to parse yelp_tip.JSON
    with open('./yelp_tip.JSON','r') as f:  #Assumes that the data files are available in the current directory. If not, you should set the path for the yelp data files.
        outfile =  open('./yeldTip.txt', 'w')
        line = f.readline()
        count_line = 0
        #read each JSON abject and extract data
        while line:
            data = json.loads(line)
            outfile.write(cleanStr4SQL(data['business_id'])+' ; ') 
            outfile.write(cleanStr4SQL(data['date'])+' ; ')
            outfile.write(str(data['likes'])+' ; ') 
            outfile.write(cleanStr4SQL(data['text'])+' ; ') 
            outfile.write(cleanStr4SQL(data['user_id']))
            outfile.write('\n')

            line = f.readline()
            count_line +=1
    print(count_line)
    outfile.close()
    f.close()

    pass

parseBusinessData()
parseUserData()
parseCheckinData()
parseTipData()
