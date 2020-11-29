import re
import datetime

from src.resource import VK
from src.database import Database
from src.controller import Controller
from config_parser import Config


def get_data(file_path):
    with open(file_path) as f:
        rows = f.readlines()
        for row in rows:
            yield row.strip()

def handle_date(unixtime):
    value = datetime.datetime.fromtimestamp(unixtime)
    date = value.strftime('%Y-%m-%d %H:%M:%S')
    return date

def run():

    config = Config('configs.yaml')

    vk = VK(config.vk)
    database = Database(config.database)
    controller = Controller(config.controller)
    group_urls_path = 'data/vk/group_short_url.txt'

    n = 0

    while True:

        offset = n * 100

        for url in get_data(group_urls_path):

            group_info = controller.get_group_info(vk, url)
            group_id = group_info[0]['id']

            try:

                wall_posts = controller.get_target_post(vk, group_id, offset, url)

                for post in wall_posts:

                    post['Text'] = ' '.join(list(filter(None, re.split('\W|\d', post['Text']))))
                    post['PublishDateTime'] = handle_date(post['PublishDateTime'])

                    controller.save_data_to_bd(bd=database, table='WallPost', target_object=post)

                for post in wall_posts:

                    comments = controller.get_target_comments(vk, group_id, post_id=post['OuterId'], offset=0)

                    for comment in comments:

                        comment['Text'] = ' '.join(list(filter(None, re.split('\W|\d', comment['Text']))))
                        comment['PublishDateTime'] = handle_date(comment['PublishDateTime'])

                        controller.save_data_to_bd(bd=database, table='Comment', target_object=comment)

                    for comment in comments:

                        user = controller.get_target_user(vk, comment['OuterId'])

                        controller.save_data_to_bd(bd=database, table='SocialNetworkUser', target_object=user)

            except Exception as e:
                print(e)

        n += 1


if __name__ == "__main__":
    run()