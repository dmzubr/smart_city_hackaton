from time import sleep

from src.controller import Controller
from src.database import Database
from config_parser import Config


def run():

    config = Config('configs.yaml')
    database = Database(config.database)

    controller = Controller()

    while True:
        controller.fill_wall_post_trigger(database)
        controller.fill_comment_trigger(database)

        controller.fill_wall_post_emotion(database)
        controller.fill_comment_emotion(database)

        sleep(60*60)

        #controller.fix_linck(database)

if __name__ == "__main__":
    run()
