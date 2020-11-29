import src.services as services

class Controller:

    def fill_wall_post_trigger(self, database):

        post_object_list = database.select('WallPost')


        triggers_objs_list = database.select('TriggerWord')

        triggers_words_list = {x['Name']: x['TriggerWordId'] for x in triggers_objs_list}

        for obj in post_object_list:

            catchted_triggers_name_list = services.trigger_check(obj['Text'], triggers_words_list)

            if len(catchted_triggers_name_list) > 0:

                obj['IsTarget'] = True
                database.update_record(obj, obj['IsTarget'])

                print('The trigger check on WallPost %s completed' % obj['SNWallPostId'])

                catchted_triggers_ind_list = [triggers_words_list[i] for i in catchted_triggers_name_list]
                catchted_triggers_ind_list = set(catchted_triggers_ind_list)
                catchted_triggers_ind_list = list(catchted_triggers_ind_list)

                for ind in catchted_triggers_ind_list:

                    database.set_wall_post_trigger({"SNWallPostId": obj['SNWallPostId'], "TriggerWordId": ind})


    def fill_comment_trigger(self, database):

        comment_object_list = database.select('Comment')

        triggers_objs_list = database.select('TriggerWord')

        triggers_words_list = {x['Name']: x['TriggerWordId'] for x in triggers_objs_list}

        for obj in comment_object_list:

            catchted_triggers_name_list = services.trigger_check(obj['Text'], triggers_words_list)

            if len(catchted_triggers_name_list) > 0:

                obj['IsTarget'] = True
                database.update_commit_record(obj, trigger=True)

                print('The trigger check on SNCommentId %s completed' % obj['SNCommentId'])

                catchted_triggers_ind_list = [triggers_words_list[i] for i in catchted_triggers_name_list]
                catchted_triggers_ind_list = set(catchted_triggers_ind_list)
                catchted_triggers_ind_list = list(catchted_triggers_ind_list)

                for ind in catchted_triggers_ind_list:

                    database.set_commit_trigger({"SNCommentId": obj['SNCommentId'], "TriggerWordId": ind})



    def fill_wall_post_emotion(self, database):

        wall_post_list = database.select('WallPostEmotion')

        for obj in wall_post_list:

            obj['EmotionMark'] = services.fill_rec_emotions(obj['Text'])
            database.update_post_emotion(obj)

            print('The emotion check on Post %s completed' % obj['SNWallPostId'])


    def fill_comment_emotion(self, database):

        object_list = database.select('Comment')

        for obj in object_list:

            obj['EmotionMark'] = services.fill_rec_emotions(obj['Text'])
            database.update_commit_record(obj)

            print('The emotion check on Comment %s completed' % obj['SNCommentId'])


    def fix_linck(self, database):

        object_list = database.select('WallPost')

        links = {}

        with open('fix_link', "r") as f:

            link = f.readlines()

            for l in link:

                short_url, _id = l.strip().split(" ")
                links[int(_id)] = short_url

        for obj in object_list:

            wall_id = obj['WallOwnerOuterId']

            try:
                url = links[wall_id]
            except KeyError:
                continue

            a = "https://vk.com/%s?w=wall%s_%s" % (url, obj['OuterAuthorId'], obj['OuterId'])

            obj['WallPostURL'] = a.strip()

            database.update_record(obj, obj['WallPostURL'], target=False)




