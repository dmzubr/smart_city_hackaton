# Функциональное описание проекта
Платформа для автоматизации сбора и обработки данных для расчёта индекса IQ городов в форме веб-приложения. Платформа интегрируется с внешними источниками данных (позволяет автоматически собирать и рассчитывать данные по 16 базовым показателям) и повышает достоверность данных за счёт их верификации для 14 индикаторов. Верификация производится путём автоматического сбора данных с соцсетей, их анализа и классификации, а также анализа эмоциональной тональности постов и комментариев с применением машинной обработки и понимания естественного языка (NLP & NLU).
Стек решения: .Net Core, python, Docker, k8s, MySQL, DeepPavlov, TypeScript, Angular. 
Уникальность: автоматическая верификация данных отчёта с применением NLU- алгоритмов для обработки больших данных.

# Демо
Доступно тут - https://smart-city-hack.cashee.ru/
Учетные записи для входа:
- region_manager : 01region_manager01
- admin : Ltybcrf28

# Логическая структура проекта
Система включает в себя следующие логические компоненты:
- реляционная БД (СУБД MySQL). Рантайм в виде docker контейнера под оркестрацией Kubernetes.
- пользовательский вэб-интерфейс. Рантайм в виде набора статических файлов, размещенных "под" nginx. Сервер крутится в виде docker контейнера под оркестрацией Kubernetes.
- сервис, предоставляющий REST API для работы клиентской части. Рантайм в виде docker контейнера под оркестрацией Kubernetes.
- скраппер данных соц сети ВК.
- сервис оценки данных ВК (оценка тональности + поиск ключевых слов)
- скраппер данных с сайта РосСтат. Все Python сервисы на настоящий момент находятся в пределах одного Docker контейнера.
 
# Файловая структура проекта
- UG.Configuration - .NET Standard 2.1. библиотека: абстракции для работы с конфигурацией
- UG.DBScripts - скрипты создания, обновления схемы БД и базового контентного наполнения
- UG.Model - .NET Standard 2.1. библиотека: описание модели предметной области
- UG.ORM - .NET Standard 2.1. библиотека: классы реляционных проекций
- UG.UserManagement.Cli - .NET Core 3.1. консольное приложение. Используется для выполнения некоторых рутинных операций (создание учетных записей пользователя, сервисы импорта данных), реализация полноценного решения которых через вэб-инетерфейс была бы слишком ресурсозатратна для проекта уровня прототипа
- UG.WebApi - ASP.NET Core 3.1. self-hosted приложение. Реализует логику REST API для клиентского приложения
- UG.WebFront - Клиентское вэб-приложение на основе шаблона ngx-Admin (на основе Angular 9 и Nebular).
- UG.PyServices - микросервисы на питоне:
--- assessor - сервис, выполняющий оценку тональности артефактов соц сетей и поиск ключевых слов. Рантайм на базе Python3.
--- rosstat_scrapper - Сервис сбора данных с сайта РосСтат. Рантайм на базе Python3.
--- vk_scraper - Сервис сбора данных с соц сети ВКонтакте. Рантайм на базе Python3.
