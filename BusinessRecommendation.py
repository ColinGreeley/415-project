import psycopg2
import pandas as pd
from flask import Flask, jsonify, request
from flask_cors import CORS
import argparse
import time


def connect_to_server():
    connection = psycopg2.connect(user="group",
                                password="group",
                                host="127.0.0.1",
                                port="5432",
                                database="group")
    return connection

def get_args():
    ap = argparse.ArgumentParser()
    ap.add_argument("-u", "--user_id", type=str, required=True)
    ap.add_argument("-s", "--min_stars", type=int, default=3)
    ap.add_argument("-r", "--min_reviews", type=int, default=1)
    ap.add_argument("-l", "--limit", type=int, default=30)
    args = vars(ap.parse_args())
    return args
    
def recommended_business_query(user_id, min_stars, min_reviews, limit):
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
                            order by recommended_business.average_rating * recommended_business.reviews desc
                            limit {}""".format(user_id, min_stars, min_stars, min_stars, min_reviews, limit)
    return recommendation_query



if __name__ == "__main__":
    connection = connect_to_server()
    cursor = connection.cursor()
    args = get_args()

    start = time.time()
    print("\n[INFO] Getting recommended businesses for user {}".format(args["user_id"]))
    business_query = recommended_business_query(user_id=args["user_id"], 
                                                min_stars=args["min_stars"], 
                                                min_reviews=args["min_reviews"],
                                                limit=args["limit"])
    cursor.execute(business_query)
    records = pd.DataFrame(cursor.fetchall())
    records.columns = ['Business name','Average stars','Reviews']
    print("\n[INFO] Getting recommended businesses took {:.5f} seconds".format(time.time()-start))
    print('\n', records)
