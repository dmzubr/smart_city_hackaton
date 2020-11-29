import random

from fake_useragent import UserAgent
from urllib.request import Request, urlopen
from bs4 import BeautifulSoup


class Proxy:
    def __init__(self):
        self.proxies = []
        self.ua = UserAgent()

        self.main()

    def main(self):

        proxies_req = Request('https://www.sslproxies.org/')
        proxies_req.add_header('User-Agent', self.ua.random)
        proxies_doc = urlopen(proxies_req).read().decode('utf8')
        soup = BeautifulSoup(proxies_doc, 'html.parser')
        proxies_table = soup.find(id='proxylisttable')


        for row in proxies_table.tbody.find_all('tr'):
            self.proxies.append({
                'ip': row.find_all('td')[0].string,
                'port': row.find_all('td')[1].string
            })


    def random_proxy(self):
        return random.randint(0, len(self.proxies) - 1)


    def get_proxy(self):
        return self.proxies[self.random_proxy()]


if __name__ == "__main__":
    proxy = Proxy()
    for _ in range(10):
        print(proxy.get_proxy())
