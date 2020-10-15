CREATE TABLE Review(
Review_ID VARCHAR(30) PRIMARY KEY NOT NULL,
User_ID VARCHAR(30),
Business_ID VARCHAR(30),
Stars FLOAT,
Useful INTEGER,
Funny INTEGER,
Cool Integer,
Text VARCHAR(5000),
Day Integer NOT NULL,
Month Integer NOT NULL,
Year Integer NOT NULL,
Hour Integer NOT NULL,
Minute Integer NOT NULL,
Second INteger NOT NULL,
FOREIGN KEY (User_ID) References Users(user_id),
FOREIGN KEY (Business_ID) References Business(business_id)
);


