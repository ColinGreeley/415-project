import pathlib




AttributeFiles=4
BusinessFiles=1
CategoryFiles=2
CheckinFiles=70
FriendFiles=63
HourFiles=3
ReviewFiles=26
TipFiles=4
UserFiles=6
absolutePath=str(pathlib.Path().absolute())

formatedPath=""
splitPath=absolutePath.split('\\')
for i in range(0,len(splitPath)-1):
    formatedPath+=splitPath[i]+'/'
formatedPath+=splitPath[len(splitPath)-1]


outfile = open("uploadSQL.sql", 'w',encoding="latin-1")

# outfile.write('\\i '+formatedPath+'/SQLFiles/uploadTables.sql\n')


# for i in range(0,BusinessFiles):
#     outfile.write('\\i '+formatedPath+'/ParsedData/Business/business'+str(i)+'.sql\n')

# for i in range(0,CategoryFiles):
#     outfile.write('\\i '+formatedPath+'/ParsedData/Category/categories'+str(i)+'.sql\n')    

# for i in range(0,AttributeFiles):
#     outfile.write('\\i '+formatedPath+'/ParsedData/Attribute/attributes'+str(i)+'.sql\n')

# for i in range(0,HourFiles):
#     outfile.write('\\i '+formatedPath+'/ParsedData/Hour/hours'+str(i)+'.sql\n')    

# for i in range(0,CheckinFiles):
#     outfile.write('\\i '+formatedPath+'/ParsedData/Checkin/checkin'+str(i)+'.sql\n')    

# for i in range(0,UserFiles):
#     outfile.write('\\i '+formatedPath+'/ParsedData/User/users'+str(i)+'.sql\n')    

# for i in range(0,TipFiles):
#     outfile.write('\\i '+formatedPath+'/ParsedData/Tip/tips'+str(i)+'.sql\n')    

# for i in range(0,ReviewFiles):
#     outfile.write('\\i '+formatedPath+'/ParsedData/Review/Review'+str(i)+'.sql\n')   

for i in range(0,FriendFiles):
    outfile.write('\\i '+formatedPath+'/ParsedData/Friend/friends'+str(i)+'.sql\n')    

outfile.close()