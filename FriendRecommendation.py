import psycopg2
import pandas as pd
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
    ap.add_argument("-l", "--limit", type=int, default=30)
    args = vars(ap.parse_args())
    return args

def recommended_friend_query(user_id, limit):
    recommendation_query = """select users.name, users.user_id, recommended_friends.friend_count
                            from users, (select distinct friends.friend_id as user_id, count(friends.friend_id) as friend_count
                                from friends, (select *
                                    from friends
                                    where friends.user_id like '{}') as user_friends
                                where friends.user_id=user_friends.friend_id
                                and friends.friend_id not like '{}'
                                group by friends.friend_id) as recommended_friends
                            where users.user_id=recommended_friends.user_id
                            order by recommended_friends.friend_count desc
                            limit {}""".format(user_id, user_id, limit)
    return recommendation_query


if __name__ == "__main__":
    connection = connect_to_server()
    cursor = connection.cursor()
    args = get_args()

    start = time.time()
    print("\n[INFO] Getting recommended friends for user {}".format(args["user_id"]))
    friends_query = recommended_friend_query(user_id=args["user_id"], 
                                                limit=args["limit"])
    cursor.execute(friends_query)
    records = pd.DataFrame(cursor.fetchall())
    records.columns = ['User name','User ID','Mutual friends']
    print("\n[INFO] Getting recommended fruends took {:.5f} seconds".format(time.time()-start))
    print('\n', records)
