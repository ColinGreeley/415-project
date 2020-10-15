CREATE TABLE Tip(
	
	tip_text VARCHAR,
	likes INTEGER,
	user_id VARCHAR NOT NULL,
	business_id VARCHAR NOT NULL,
	day Integer NOT NULL,
	month Integer NOT NULL,
	year Integer NOT NULL,
	hour Integer NOT NULL,
	minute Integer NOT NULL,
	second INteger NOT NULL,
	PRIMARY KEY(businessid,userid,year,month,day,hour,minute,second),
	FOREIGN KEY(businessid) REFERENCES Business(business_id),
	FOREIGN KEY(userid) REFERENCES Users(user_id)
);