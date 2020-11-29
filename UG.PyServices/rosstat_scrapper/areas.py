import requests
import datetime
import csv

from scrapper import Scrapper


def get_region_url(start_page, target_region, scrapper):
    main_html_page = scrapper.get_page(start_page)
    bs_page = scrapper.pars_page(main_html_page)
    regions_page = scrapper.get_list(bs_page, 'a', attrs={})

    for i in regions_page:

        if target_region == i.string:
            return i['href']

    raise ValueError('NO %s in regions list, check the value' % target_region)


def run():
    scrapper = Scrapper()
    start_page = "https://rosstat.gov.ru/free_doc/new_site/bd_munst/munst.htm"

    cities = {}
    with open("cities_codes.tsv", "r", encoding='utf8', errors='ignore') as tsvfile:
        tsvreader = csv.reader(tsvfile, delimiter="\t")

        for line in tsvreader:
            city, oktmo = line
            cities[city] = {'oktmo': oktmo}

    with open("regions.csv", "r", encoding='utf8', errors='ignore') as csvfile:
        csvreader = csv.reader(csvfile, delimiter=",")

        for i, line in enumerate(csvreader):
            if i == 0:
                continue

            city = line[1]
            region = line[2]

            cities[city]['region'] = region

    # List with meta records of received values
    res = []

    for city in cities:
        target_region = cities[city]['region']
        source_oktmo = cities[city]['oktmo']
        oktmo = cities[city]['oktmo'][:8]

        region_main_url = get_region_url(start_page, target_region, scrapper)

        munr = str(oktmo)[:3] + '00000'

        qry = "Pokazateli:8006001;munr:%s;tippos:10,9,8,7,6,5,4,3,2,1;oktmo:%s;god:2019;period:17;" % (munr, oktmo)

        data = {
            "rdLayoutType": "Au",
            "_Pokazateli": "on",
            "_munr": "on",
            "_tippos": "on",
            "_oktmo": "on",
            "_mest": "on",
            "_god": "on",
            "_period": "on",
            "a_Pokazateli": 1,
            "a_period": 2,
            "a_oktmo": 3,
            "a_god": 1,
            "a_tippos": 2,
            "Qry": qry,
            "QryGm": "Pokazateli_z:1;period_z:2;oktmo_z:3;god_s:1;tippos_s:2;munr_b:1;",
            "QryFootNotes": "",
            "YearsList": "2008;2009;2010;2011;2012;2013;2014;2015;2016;2017;2018;2019;2020;",
            "tbl": "(unable to decode value)"
        }

        region_main_url = region_main_url.replace("http://www.gks.ru", "https://rosstat.gov.ru")
        page = scrapper.get_page_post(page_url=region_main_url, data=data)
        bs_page = scrapper.pars_page(page)
        areas_page = scrapper.get_list(bs_page, 'td', attrs={'align': "right"})

        try:
            areas_page = int(areas_page[0].text)
        except ValueError:
            areas_page, _ = areas_page[0].text.split(',')
            areas_page = int(areas_page)
        except Exception:
            pass

        print("%s otkmo %s munr %s url %s count %s" % (city, oktmo, munr, region_main_url, areas_page))
        if type(areas_page) is int: # len(areas_page) > 0: # areas_page is not None and areas_page > 0:
            res_rec = {
                'oktmo': source_oktmo,
                'metric_code': metric_code,
                'value': areas_page,
                'period_start': datetime.datetime(year, 1, 1, 0, 0, 0).strftime('%Y-%m-%d %H:%M:%S'),
                'period_end': datetime.datetime(year, 12, 31, 23, 59, 59).strftime('%Y-%m-%d %H:%M:%S')
            }
            res.append(res_rec)

    pass_value_to_hook(res)


def pass_value_to_hook(vals_recs_list):
    post_url = 'https://smart-city-hack-back.cashee.ru/api/OuterMetricValue/HandleScrapperOne'
    data = {
        'values': vals_recs_list
    }
    print(f'Pass scrapped data to service')
    resp = requests.post(post_url, json=data, verify=False)
    print(f'{resp.status_code} : {resp.content}')


if __name__ == "__main__":
    metric_code = 'TOTAL_AREA'
    year = 2019
    run()
