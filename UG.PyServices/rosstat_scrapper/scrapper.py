from bs4 import BeautifulSoup
from proxy import Proxy
import requests
from fake_useragent import UserAgent


class Scrapper:

    def __init__(self):
        self.proxy = Proxy()
        self.session = requests.session()
        self.ua = UserAgent()

    def generate_headers(self):
        return {'User-Agent': self.ua.random, "Accept-Language": "en-US, en;q=0.5"}

    def get_page(self, page_url, encoding='cp1251'):
        headers = self.generate_headers()
        proxy = self.proxy.get_proxy()
        res = self.session.get(page_url, headers=headers, proxies=proxy)
        res.encoding = encoding
        return res

    def get_page_post(self, page_url, data, encoding='cp1251'):
        headers = self.generate_headers()
        proxy = self.proxy.get_proxy()
        res = self.session.post(page_url, headers=headers, proxies=proxy, data=data)
        res.encoding = encoding
        return res

    def pars_page(self, res, param="html.parser"):
        parsed_page = BeautifulSoup(res.text, param)
        return parsed_page

    def get_list(self, soup_html, html_elem, attrs={}):
        return soup_html.find_all(html_elem, attrs)


if __name__ == "__main__":
    scraper = Scrapper()
    start_page = "https://rosstat.gov.ru/free_doc/new_site/bd_munst/munst.htm"
    target_city = 'Архангельская область'
    page = scraper.get_page(start_page)
    page.encoding = 'cp1251'
    parsed_page = scraper.pars_page(page)
    some_list = scraper.get_list(parsed_page, 'a', attrs={})

    for i in some_list:
        if target_city == i.string:
            print(i.string)
            print(i['href'])
