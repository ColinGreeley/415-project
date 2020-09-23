--numCheckins update statements  DOES NOT WORK
update business 
set  numCheckins = B.s
from(
select business_id, count(business_id) as s
from CHECKIN
group by business_id) B
where business.business_id = B.business_id;

--update TipCount  WORKS
update business
set numTips = b.count
from(select businessid, count(likes)
from Tip
group by businessid ) b 
where business_id = b.businessid;

--TotalLikes
update Users 
set total_likes = B.sum
from(
select userid , sum(likes)
from Tip
group by userid) B
where user_id = B.userid;

--tipCount
update Users
set tipCount = C.s
from( 
select userid, Count(tiptext) as s
from Tip
group by userid) C
where user_id = C.userid;
