import datetime
from time import sleep


class Controller:

    def __init__(self, config):
        self.friends_threshold = config['friends_threshold']
        self.target_year = config['target_year']

    def handle_date(self, unixtime):
        value = datetime.datetime.fromtimestamp(unixtime)
        date = value.strftime('%Y-%m-%d %H:%M:%S')
        return date

    def save_data_to_bd(self, bd, table, target_object):
        bd.save(table, target_object)

    def get_target_user(self, resource, group_id):

        user = resource.get_user(id_=group_id)

        user_target_object = {}

        user_id = user['id']
        user_target_object['OuterId'] = user_id

        try:
            screen_name = user['screen_name']
        except KeyError:
            screen_name = ""
        try:
            first_name = user['first_name']
        except KeyError:
            first_name = ""
        try:
            last_name = user['last_name']
        except KeyError:
            last_name = ""
        try:
            city = user['city']['title']
        except KeyError:
            city = ""
        try:
            followers_count = user['followers_count']
        except KeyError:
            followers_count = 0
        try:
            phone = user['contacts']['mobile_phone']
        except KeyError:
            phone = ""
        try:
            friends = user['counters']['friends']
        except KeyError:
            friends = 0

        user_target_object['SocialNetworkId'] = 1
        user_target_object['OuterId'] = user_id
        user_target_object['NameAlias'] = screen_name
        user_target_object['UserPageURL'] = "https://vk.com/id%s" % user_id
        user_target_object['Phone'] = phone
        user_target_object['FirstName'] = first_name
        user_target_object['LastName'] = last_name
        user_target_object['City'] = city
        user_target_object['FriendsQuantity'] = friends
        user_target_object['FollowersQuantity'] = followers_count

        print("user", user_target_object)

        return user_target_object


    def get_target_post(self, resource, user_id, offset, url):

        posts = []
        wall_data = resource.get_user_wall_data(owner_id=user_id, offset=offset)

        for post in wall_data['items']:

            sleep(0.3)
            target_post_object = {}

            try:
                post_id = post['id']
                post_text = post['text']

                if post_text == "":
                    print('No text in post, skip')
                    continue

                post_author = post['from_id']

                try:
                    date = self.handle_date(post['date'])
                    print(date)

                    dt = datetime.datetime.strptime(date, '%Y-%m-%d %H:%M:%S')
                    if dt.year not in self.target_year:
                        continue

                except KeyError:
                    post['date'] = ''
                try:
                    like_count = post['likes']['count']
                except KeyError:
                    like_count = 0
                try:
                    views_count = post['views']['count']
                except KeyError:
                    views_count = 0
                try:
                    reposts_count = post['reposts']['count']
                except KeyError:
                    reposts_count = 0
                try:
                    comments_count = post['comments']['count']
                    target_post_object['CommentQuantity'] = comments_count
                except KeyError:
                    comments_count = 0

                target_post_object['Text'] = post_text
                target_post_object['OuterId'] = post_id
                target_post_object['WallOwnerOuterId'] = user_id
                target_post_object['OuterAuthorId'] = post_author
                target_post_object['PublishDateTime'] = post['date']
                target_post_object['LikesQuantity'] = like_count
                target_post_object['ViewsQuantity'] = views_count
                target_post_object['RepostQuantity'] = reposts_count
                target_post_object['CommentQuantity'] = comments_count
                target_post_object['WallPostURL'] = "https://vk.com/%s?w=wall%s_%s" % (url, post_author, post_id)

                print("post", target_post_object)

                posts.append(target_post_object)

            except KeyError as e:
                print('User id %s error %s' % (user_id, e))

        return posts

    def get_target_comments(self, resource, user_id, post_id, offset):

        comments = []
        posts_comment = resource.get_comments(user_id, post_id, offset)

        for comment in posts_comment['items']:

            sleep(0.3)
            target_comment_object = {}

            comment_id = comment['id']
            comment_date = comment['date']
            comment_text = comment['text']

            if comment_text == "":
                print('No text in comment, skip')
                continue

            comment_author = comment['from_id']

            try:
                like_count = comment['likes']['count']
            except KeyError:
                like_count = 0
            try:
                comment_thread_count = comment['thread']['count']
            except KeyError:
                comment_thread_count = 0

            target_comment_object['OuterId'] = comment_id
            target_comment_object['LikesQuantity'] = like_count
            target_comment_object['SNWallPostId'] = post_id
            target_comment_object['Text'] = comment_text
            target_comment_object['PublishDateTime'] = comment_date
            target_comment_object['OuterAuthorId'] = comment_author
            target_comment_object['CommentsQuantity'] = comment_thread_count
            print("comment", target_comment_object)

            comments.append(target_comment_object)

        return comments

    def get_group_info(self, resource, id_):

        info = resource.get_group_info(id_)

        return info