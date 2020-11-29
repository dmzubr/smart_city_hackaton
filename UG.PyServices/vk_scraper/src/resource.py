import vk_api


class VK:

    def __init__(self, config):

        vk_session = vk_api.VkApi(login=config['login'], password=config['password'])
        vk_session.auth(token_only=True)
        self.vk = vk_session.get_api()
        self.tools = vk_api.VkTools(vk_session)

    def get_group_info(self, group_name):

        info = self.vk.groups.getById(group_ids=group_name)

        return info

    def get_user_wall_data(self, owner_id, offset):

        owner_id = owner_id * -1
        wall = self.vk.wall.get(owner_id=owner_id, extended=1, count=100, offset=offset)

        return wall

    def get_comments(self, owner_id, post_id, offset):

        owner_id = owner_id * -1
        comments = self.vk.wall.getComments(owner_id=owner_id, post_id=post_id, need_likes=1, count=100,
                                            extended=1, offset=offset)
        return comments

    def get_users(self, id_):

        users = self.tools.get_all_iter('groups.getMembers',
                                        100, {'group_id': id_,
                                              "fields": ['bdate', 'city', 'can_post', 'screen_name',
                                                         'followers_count', 'wall_default'], 'version': '5.107'})
        return users

    def get_user(self, id_):

        user = self.vk.users.get(user_ids=id_, fields=['bdate', 'city', 'can_post', 'screen_name',
                                                       'followers_count', 'wall_default', 'contacts', 'counters'])
        return user[0]
