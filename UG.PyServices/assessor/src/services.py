import requests
import json

import nltk
nltk.download('punkt')

from nltk.stem.snowball import SnowballStemmer
from nltk.tokenize import word_tokenize


def asses_text_emotions(in_strs_list):

    emotions_assesor_rest_url = 'https://7015.lnsigo.mipt.ru/model'

    req_body = {
        'x': [in_strs_list]
    }

    headers = {'Content-type': 'application/json'}
    response = requests.post(emotions_assesor_rest_url, data=json.dumps(req_body), headers=headers)
    resp_json = response.json()

    res = []

    for ass_str_res in resp_json:
        st = ass_str_res[0].lower()
        if st == 'negative':
            res.append(0)
        elif st == 'neutral':
            res.append(1)
        else:
            res.append(2)

    return res


def fill_rec_emotions(text):

    assessment_res = asses_text_emotions(text)
    assessment = assessment_res[0]

    return assessment

stemmer = SnowballStemmer("russian")

def trigger_check(text_to_check, triggers_words_list):

    stemmed_triggers_list = [stemmer.stem(word) for word in triggers_words_list]


    catchted_triggers_ids_list = get_catched_triggers_ids_from_text(text_to_check, stemmed_triggers_list,
                                                                    list(triggers_words_list.keys()))

    return catchted_triggers_ids_list


def get_catched_triggers_ids_from_text(text, stemmed_triggers_word_list, triggers_list):

    res = []

    text = text.lower()
    text_words = word_tokenize(text)
    stemmed_text_words = [stemmer.stem(word) for word in text_words]
    intersected_words = [word for word in stemmed_text_words if word in stemmed_triggers_word_list]

    for intersected_word in intersected_words:
        index = stemmed_triggers_word_list.index(intersected_word)

        if index > -1:

            related_trigger_obj = triggers_list[index]

            try:
                res.append(related_trigger_obj)
            except TypeError as e:
                print(e)

    return res