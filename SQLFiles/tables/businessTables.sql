CREATE TABLE Business(
    business_id CHAR(22) NOT NULL,
    name VARCHAR(300),
    address VARCHAR(300),
    city VARCHAR(50),
    state CHAR(2),
    zipcode VARCHAR(30),
    latitude double precision,
    longitude double precision,
    stars double precision,
    numTips INTEGER,
    is_open INTEGER,
    numCheckins INTEGER,
    PRIMARY KEY (business_id)
);

CREATE TABLE Categories(
    business_id char(22) NOT NULL,
    category VARCHAR(50) NOT NULL,
    FOREIGN KEY (business_id) REFERENCES Business(business_id),
    PRIMARY KEY (business_id,category)
);

CREATE TABLE  Attributes(
    business_id CHAR(22) NOT NULL,
    attribute_key VARCHAR(50) NOT NULL,
    attribute VARCHAR(300),
    FOREIGN KEY (business_id) REFERENCES Business(business_id),
    PRIMARY KEY (business_id,attribute_key) 
);

CREATE TABLE Hours(
    business_id CHAR(22) NOT NULL,
    day VARCHAR(10) NOT NULL,
    open_time VARCHAR(5),
    close_time VARCHAR(5),
    FOREIGN KEY (business_id) REFERENCES Business(business_id),
    PRIMARY KEY (business_id,day)
);

    CREATE TABLE CHECKIN(
        business_id VARCHAR(22) NOT NULL,
        year  INTEGER NOT NULL,
        month INTEGER NOT NULL,
        day INTEGER NOT NULL,
        hour INTEGER NOT NULL,
        minute INTEGER NOT NULL,
        second INTEGER NOT NULL,
		PRIMARY KEY (business_id,year,month,day,hour,minute,second),
        FOREIGN KEY (business_id) REFERENCES Business(business_id)
        
    );