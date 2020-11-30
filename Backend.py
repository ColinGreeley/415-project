import psycopg2
import pandas as pd
from flask import Flask, jsonify, request
from flask_cors import CORS
from password import password


def connect_to_server():
    connection = psycopg2.connect(user="group",
                                password="group",
                                host="127.0.0.1",
                                port="5432",
                                database="group")
    return connection

def recommended_business_query(user_id, min_stars, min_reviews):
    recommendation_query = """select *
                            from (select distinct business.name, sum(review.stars)/count(review.stars) as average_rating, count(review.stars) as reviews
                                from business, review, (select distinct review.user_id, review.business_id, review.stars, review.useful
                                    from review, (select review.*
                                        from users, review 
                                        where review.user_id=users.user_id 
                                        and users.user_id like '{}'
                                        and review.stars > {}) as user_reviewed_business
                                    where review.business_id=user_reviewed_business.business_id
                                    and review.stars > {}) as users_who_have_reviewed_the_same_business
                                where review.user_id=users_who_have_reviewed_the_same_business.user_id
                                and business.business_id=review.business_id
                                and review.business_id!=users_who_have_reviewed_the_same_business.business_id
                                group by business.name) as recommended_business
                            where recommended_business.average_rating > {}
                            and recommended_business.reviews > {}
                            order by recommended_business.average_rating * recommended_business.reviews desc""".format(user_id, min_stars, min_stars, min_stars, min_reviews)
    return recommendation_query

def recommended_friend_query(user_id, limit):
    recommendation_query = """select users.name, users.user_id, recommended_friends.friend_count
                            from users, (select distinct friends.friend_id as user_id, count(friends.friend_id) as friend_count
                                from friends, (select *
                                    from friends
                                    where friends.user_id like '{}') as user_friends
                                where friends.user_id=user_friends.friend_id
                                and friends.friend_id not like '{}'
                                group by friends.friend_id
                                order by friend_count desc) as recommended_friends
                            where users.user_id=recommended_friends.user_id
                            limit {}""".format(user_id, user_id, limit)
    return recommendation_query


if __name__ == "__main__":
    connection = connect_to_server()
    cursor = connection.cursor()

    user_id = '--ZNfWKj1VyVElRx6-g1fg'
    business_query = recommended_business_query(user_id=user_id, min_stars=3, min_reviews=1)
    friend_query = recommended_friend_query(user_id=user_id, limit=10)

    cursor.execute(business_query)
    records = pd.DataFrame(cursor.fetchall())
    print(records)
    
    cursor.execute(friend_query)
    records = pd.DataFrame(cursor.fetchall())
    print(records)
