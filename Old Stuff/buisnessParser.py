import json

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


def cleanStr4SQL(s):
    if "'" in s:
        s = s.replace("'", "`")
    if "\n" in s:
        s = s.replace("\n", " ")
    return s


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


parseBusinessData()
