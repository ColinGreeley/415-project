import buisnessParser
import checkinParser
import reviewParser
import tipParser
import userParser
import time

start=time.time()
print("running buisnessParser")
buisnessParser.runBusinessParser()
print("running CheckinParser")
checkinParser.runCheckinParser()
print("running ReviewParser")
reviewParser.runReviewParser()
print("running TipParser")
tipParser.runTipParser()
print("running UserParser")
userParser.runUserParser()
end=time.time()

print("TotalTime: "+str(end-start))
