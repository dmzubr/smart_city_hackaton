import mysql.connector
from mysql.connector import Error


class Database:

    def __init__(self, config):

        self.host = config['host']
        self.port = config['port']
        self.database = config['database']
        self.user = config['user']
        self.password = config['password']

    def get_db_conn(self):
        conn = mysql.connector.connect(host=self.host,
                                       port=self.port,
                                       database=self.database,
                                       user=self.user,
                                       password=self.password)
        return conn


    def get_trigger_word_list(self, records):

        triggers_objs_list = []

        for row in records:
            triggers_objs_list.append({'TriggerWordId':  row[0], 'Name': row[1]})

        return triggers_objs_list

    def get_wall_post_list(self, records):

        triggers_objs_list = []

        for row in records:

            obj = {
                'SNWallPostId': row[0],
                'OuterId': row[1],
                'PublishDateTime': row[2],
                'Text': row[3],
                'WallOwnerOuterId': row[4],
                'OuterAuthorId': row[5],
                'WallPostURL': row[6],
                'LikesQuantity': row[7],
                'RepostQuantity': row[8],
                'CommentQuantity': row[9],
                'ViewsQuantity': row[10],
                'LastModifiedDateTime': row[11],
                'EmotionMark': row[12],
                'IsTarget': row[13],
            }

            if len(obj['Text']) > 0:
                triggers_objs_list.append(obj)

        return triggers_objs_list

    def get_comment_list(self, records):

        triggers_objs_list = []

        for row in records:
            obj = {
                'SNCommentId': row[0],
                'OuterId': row[1],
                'PublishDateTime': row[2],
                'Text': row[3],
                'SNWallPostId': row[4],
                'OuterAuthorId': row[5],
                'LikesQuantity': row[6],
                'CommentQuantity': row[7],
                'LastModifiedDateTime': row[8],
                'EmotionMark': row[9],
                'IsTarget': row[10]
            }
            if len(obj['Text']) > 0:
                triggers_objs_list.append(obj)

        return triggers_objs_list


    def set_wall_post_trigger(self, obj):

        sql = f"""
        INSERT INTO SNWallPostTriggerWord
        (
          SNWallPostId,
          TriggerWordId
        )
        VALUES
        (
          '{obj["SNWallPostId"]}',
          '{obj["TriggerWordId"]}'
        );"""

        cnx = self.get_db_conn()
        cursor = cnx.cursor()
        cursor.execute(sql)
        cnx.commit()

        cursor.close()
        cnx.close()

    def set_commit_trigger(self, obj):

        sql = f"""
        INSERT INTO SNCommentTriggerWord
        (
          SNCommentId,
          TriggerWordId
        )
        VALUES
        (
          '{obj["SNCommentId"]}',
          '{obj["TriggerWordId"]}'
        );"""

        cnx = self.get_db_conn()
        cursor = cnx.cursor()
        cursor.execute(sql)
        cnx.commit()

        cursor.close()
        cnx.close()

    def update_post_emotion(self, obj):

        try:

            obj['IsTarget'] = True
            conn = self.get_db_conn()

            if conn.is_connected():

                query = f"""
                    UPDATE `SNWallPost` 
                    SET `EmotionMark`={obj['EmotionMark']}     	                     	    
                    WHERE SNWallPostId={obj['SNWallPostId']};
                """

                cursor = conn.cursor()
                cursor.execute(query)
                conn.commit()

        except Error as e:
            print(f"Error when updating emotions mark for Post #{obj['SNCommentId']}")
            print(e)

        finally:
            conn.close()

    def update_commit_record(self, obj, trigger=False):

        if trigger:
            try:
                conn = self.get_db_conn()

                if conn.is_connected():

                    query = f"""
                        UPDATE `SNComment` 
                        SET `IsTarget`={obj['IsTarget']}     	                     	    
                        WHERE SNCommentId={obj['SNCommentId']};
                    """

                    cursor = conn.cursor()
                    cursor.execute(query)
                    conn.commit()

            except Error as e:
                print(f"Error when updating emotions mark for Post #{obj['SNCommentId']}")
                print(e)

            finally:
                conn.close()
        else:

            try:

                obj['IsTarget'] = True
                conn = self.get_db_conn()

                if conn.is_connected():

                    query = f"""
                        UPDATE `SNComment` 
                        SET `EmotionMark`={obj['EmotionMark']}     	                     	    
                        WHERE SNCommentId={obj['SNCommentId']};
                    """

                    cursor = conn.cursor()
                    cursor.execute(query)
                    conn.commit()

            except Error as e:
                print(f"Error when updating emotions mark for Post #{obj['SNCommentId']}")
                print(e)

            finally:
                conn.close()


    def update_record(self, obj, prop, target=True):

        if target:

            query = f"""
                        UPDATE SNWallPost 
                        SET IsTarget={prop}     	                     	    
                        WHERE SNWallPostId={obj['SNWallPostId']};
                    """

        else:

            query = f"""UPDATE SNWallPost SET WallPostURL = "{obj['WallPostURL']}" WHERE SNWallPostId = {obj['SNWallPostId']};"""

        try:
            conn = self.get_db_conn()

            if conn.is_connected():

                cursor = conn.cursor()
                cursor.execute(query)
                conn.commit()

        except Error as e:
            print(f"Error when updating emotions mark for Post #{obj['SNWallPostId']}")
            print(e)

        finally:
            conn.close()


    def select(self, db_name):

        if db_name == "TriggerWord":
            sql_select_Query = "select * from TriggerWord"

        if db_name == "WallPost":
            sql_select_Query = "SELECT * FROM SNWallPost"

        if db_name == 'Comment':
            sql_select_Query = f"""
                                SELECT *    
                                FROM SNComment
                                WHERE EmotionMark IS NULL
                                ORDER BY LastModifiedDateTime desc;"""

        if db_name == "WallPostEmotion":
            sql_select_Query = f"""
                                SELECT *    
                                FROM SNWallPost
                                WHERE EmotionMark IS NULL
                                ORDER BY LastModifiedDateTime desc;"""

        try:
            connection = self.get_db_conn()
            cursor = connection.cursor()
            cursor.execute(sql_select_Query)
            records = cursor.fetchall()

            print("Total number of rows in Laptop is: ", cursor.rowcount)

            print("\nPrinting each laptop record")

            if db_name == "TriggerWord":
                return self.get_trigger_word_list(records)

            if db_name == "WallPost" or db_name == "WallPostEmotion":
                return self.get_wall_post_list(records)

            if db_name == 'Comment':
                return self.get_comment_list(records)

        except Error as e:
            print("Error reading data from MySQL table", e)

        finally:

            if (connection.is_connected()):
                connection.close()
                cursor.close()
                print("MySQL connection is closed")

if __name__ == "__main__":
    from config_parser import Config
    config = Config("../configs.yaml")
    db = Database(config.database)
    L = db.select('SNWallPost')
    print(L)