import json


def cleanStr4SQL(s):
    if "'" in s:
        s=s.replace("'","`")
    if "\n" in s:
        s=s.replace("\n"," ")
    if "\\" in s:
        s=s.replace("\\"," ")
    return s


def parseTipData():
  
    # read the JSON file
    with open('yelp_tip.JSON', 'r') as file:
        outfile = open('./sql_files/tipData1.sql', 'w')
        outfile.write("INSERT INTO Tip (business_id,year,month,day,hour,minute,second,likes,tip_text,user_id) VALUES ")

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

parseTipData()