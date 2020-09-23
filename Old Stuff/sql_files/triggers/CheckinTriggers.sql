--increase checkin amount
CREATE OR REPLACE FUNCTION UpdateCheckinAmount() RETURNS trigger AS '
BEGIN 
   UPDATE  Business
   SET  numCheckins= numCheckins+1
   WHERE Business.business_id=NEW.business_id;
   RETURN NEW;
END
' LANGUAGE plpgsql;


CREATE trigger checkinCount
AFTER INSERT ON Checkin
FOR EACH ROW
WHEN (NEW.business_id IS NOT NULL)
EXECUTE PROCEDURE UpdateCheckinAmount();



INSERT INTO Checkin(business_id,year,month,day,hour, minute,second) VALUES
('kANF0dbeoW34s2vwh6Umfw',2030,6,14,2,33,6),
('kANF0dbeoW34s2vwh6Umfw',2011,6,16,2,33,26),
('kANF0dbeoW34s2vwh6Umfw',2001,6,11,2,33,26);

select numCheckins from Business where business_id='kANF0dbeoW34s2vwh6Umfw';
--should =3



--Total Likes
CREATE OR REPLACE FUNCTION UpdatetotalLikes() RETURNS trigger AS '
BEGIN 
   UPDATE  Users
   SET  total_likes= total_likes+NEW.likes
   WHERE Users.user_id=NEW.userid;
   RETURN NEW;
END
' LANGUAGE plpgsql;


CREATE trigger likesCount
AFTER INSERT ON tip
FOR EACH ROW
WHEN (NEW.userid IS NOT NULL)
EXECUTE PROCEDURE UpdatetotalLikes()

INSERT INTO Tip(businessid,userid,year,month,day,hour,minute,second,tipText,likes)VALUES
('Y6iyemLX_oylRpnr38vgMA','1l_O3VEl9TV_JjKAzzyz0Q',1982,12,1,7,40,21,'woot good',7),
('Y6iyemLX_oylRpnr38vgMA','1l_O3VEl9TV_JjKAzzyz0Q',2001,12,1,9,40,21,'woot good2',7);
select total_likes from Users where user_id='1l_O3VEl9TV_JjKAzzyz0Q';
--should =14
