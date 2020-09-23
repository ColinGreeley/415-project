CREATE OR REPLACE FUNCTION UpdatetipCount() RETURNS trigger AS '
BEGIN 
   UPDATE  Users
   SET  tipCount= tipCount + 1
   WHERE Users.user_id = NEW.userid;
   
   UPDATE  Business
   SET  numTips = numTips + 1
   WHERE Business.business_id = NEW.businessid;
   RETURN NEW; 
END
' LANGUAGE plpgsql;


CREATE trigger tipCount
AFTER INSERT ON Tip
FOR EACH ROW
WHEN (NEW.businessid IS NOT NULL AND NEW.userid IS NOT NULL)
EXECUTE PROCEDURE UpdatetipCount();

'---1lKK3aKOuomHnwAkAow'
insert into tip(tiptext,likes,userid,businessid,day,month,year,hour,minute,second) 
Values("woot",3,'--0WZ5gklOfbUIodJuKfaQ','-_nz_8EPGQKKTd8loQDgXQ',1,2,1111,4,3,2);

select tipCount from Users where user_id='--0WZ5gklOfbUIodJuKfaQ';

select numTips from Business where business_id ='-_nz_8EPGQKKTd8loQDgXQ';