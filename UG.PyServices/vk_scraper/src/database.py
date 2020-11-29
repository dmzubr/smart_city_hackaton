from datetime import datetime

import mysql.connector


class Database:

    def __init__(self, config):
        self.host = config['host']
        self.port = config['port']
        self.database = config['database']
        self.user = config['user']
        self.password = config['password']

        self.posts_id = []
        self.users_id = []
        self.comments_id = []
        self._get_all_data(['SNUser', 'SNWallPost', 'SNComment'])


    def get_db_conn(self):

        conn = mysql.connector.connect(host=self.host,
                                       port=self.port,
                                       database=self.database,
                                       user=self.user,
                                       password=self.password)
        return conn


    def save(self, table, target_object):

        if table == 'SocialNetworkUser':
            self.add_users(target_object)
        elif table == 'WallPost':
            self.add_post(target_object)
        elif table == 'Comment':
            self.add_comment(target_object)
        else:
            print('Table %s does not exist' % table)


    def add_users(self, users_object):

        if users_object['OuterId'] not in self.users_id:

            if len(users_object["Phone"]) < 5:
                users_object["Phone"] = 'xxx-xxx'

            add_SocialNetworkUser = ("INSERT INTO SNUser"
                                     
                                     "(SocialNetworkId, OuterId, NameAlias, FirstName, LastName, UserPageURL, \
                                     City, FriendsQuantity) "
                                     
                                     "VALUES (%(SocialNetworkId)s, %(OuterId)s, %(NameAlias)s, %(FirstName)s, \
                                     %(LastName)s, %(UserPageURL)s, %(City)s, %(FriendsQuantity)s)")

            self.execute(add_SocialNetworkUser, users_object)

            self.users_id.append(users_object['OuterId'])
            print("User saved %s" % users_object)


    def add_post(self, post_object):

        if post_object['OuterId'] not in self.posts_id:

            if post_object['PublishDateTime'] == '':
                post_object['PublishDateTime'] = datetime.strptime('Jun 1 1990  1:33PM', '%b %d %Y %I:%M%p')

            add_WallPost = ("INSERT INTO SNWallPost"
                            
                            "(OuterId, PublishDateTime, Text, WallOwnerOuterId, \
                            OuterAuthorId, WallPostURL, LikesQuantity, RepostQuantity, CommentQuantity, ViewsQuantity) "
                            
                            "VALUES (%(OuterId)s, %(PublishDateTime)s, %(Text)s, %(WallOwnerOuterId)s, \
                                     %(OuterAuthorId)s, %(WallPostURL)s, %(LikesQuantity)s, %(RepostQuantity)s, %(CommentQuantity)s, \
                                     %(ViewsQuantity)s)")

            self.execute(add_WallPost, post_object)

            self.posts_id.append(post_object['OuterId'])

            print("Post saved %s" % post_object)


    def add_comment(self, comit_obgect):

        if comit_obgect['OuterId'] not in self.comments_id:

            if comit_obgect['PublishDateTime'] == '':
                comit_obgect['PublishDateTime'] = datetime.strptime('Jun 1 1990  1:33PM', '%b %d %Y %I:%M%p')

            sql_select_Query = f"""select * from SNWallPost
                                    WHERE `OuterId`={comit_obgect['SNWallPostId']};
                                    """
            wall_post_id = self.get_id(sql_select_Query)
            comit_obgect['SNWallPostId'] = wall_post_id

            add_WallPost = ("INSERT INTO SNComment"
                            
                            "(OuterId, LikesQuantity, SNWallPostId, Text, \
                            PublishDateTime, OuterAuthorId, CommentsQuantity) "
                            
                            "VALUES (%(OuterId)s, %(LikesQuantity)s, %(SNWallPostId)s, %(Text)s, \
                                     %(PublishDateTime)s, %(OuterAuthorId)s, %(CommentsQuantity)s)")

            self.execute(add_WallPost, comit_obgect)

            self.comments_id.append(comit_obgect['OuterId'])

            print("Comment saved %s" % comit_obgect)

    def execute(self, sql, added_object):

        cnx = self.get_db_conn()
        cursor = cnx.cursor()
        cursor.execute(sql, added_object)
        cnx.commit()

        cursor.close()
        cnx.close()


    def _get_all_data(self, tables):

        for table_name in tables:

            sql_select_Query = f"""select OuterId from {table_name}"""
            connection = self.get_db_conn()
            cursor = connection.cursor()
            cursor.execute(sql_select_Query)
            records = cursor.fetchall()

            for _id in records:

                if table_name == 'SNWallPost':
                    self.posts_id.append(_id[0])

                if table_name == 'SNUser':
                    self.users_id.append(_id[0])

                if table_name == 'SNComment':
                    self.comments_id.append(_id[0])

            connection.close()


    def get_id(self, query):

        connection = self.get_db_conn()
        cursor = connection.cursor()
        cursor.execute(query)
        records = cursor.fetchall()

        return records[0][0]