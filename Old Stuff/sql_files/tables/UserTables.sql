CREATE TABLE Users (

    average_stars FLOAT,
    cool   INTEGER,
    fans INTEGER,
    funny INTEGER,
    usefull INTEGER,
    name VARCHAR(50),
    tipcount INTEGER,
    user_id VARCHAR(22) PRIMARY KEY NOT NULL,
    yelping_since VARCHAR(30),
    total_likes  INTEGER
);


CREATE TABLE Friends(

    user_id VARCHAR(22) NOT NULL,
    friend_id VARCHAR(22) NOT NULL,
    PRIMARY KEY(user_id,friend_id),
    FOREIGN KEY(user_id) REFERENCES Users(user_id),
    FOREIGN KEY(friend_id) REFERENCES Users(user_id)
)