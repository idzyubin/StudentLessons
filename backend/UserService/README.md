# Руководство по запуску приложения

Чтобы запустить приложение, требуется:
1. Установить WSL (Windows Subsytem for Linux): https://learn.microsoft.com/en-us/windows/wsl/install
2. Установить Docker Desktop: https://www.docker.com/products/docker-desktop/
3. Перейти в приложение Terminal (если его еще нет: https://learn.microsoft.com/ru-ru/windows/terminal/install)
4. Выполнить следующую команду для создания БД PostgreSQL в Docker контейнере: docker run --name user_db -p 5432:5432 -e POSTGRES_PASSWORD=password -d postgres
5. Выполнить сборку приложения
6. Запустить