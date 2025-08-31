# Инструкция по запуску сервиса
Данный репозиторий содержит API для работы с рекламными площадками. Для запуска сервиса выполните следующие шаги:

## Предварительные требования
1. **.NET 9 SDK with ASP.net Core** 
2. **Xunit**
3. **FluentAssertions**
4. **Swagger**

### Шаги по установке и запуску
1. **Клонирование репозитория**

    *git clone [https://github.com/MaximKortnev/ApplicationAPI.git](https://github.com/MaximKortnev/AdvertPlatforms.git)*
   или 

<img width="977" height="242" alt="image" src="https://github.com/user-attachments/assets/2ba99be5-52c2-4d27-af35-985d42fd0ed2" />

3. **Переход в директорию проекта**
   
    *cd your-repository*
> Замените your-repository на название вашего репозитория.

4. **Запуск проекта**

*dotnet run*

или
<img width="1004" height="285" alt="image" src="https://github.com/user-attachments/assets/e532bd0e-e6e5-4269-92b8-0390eb0ae7ba" />

> После этого API будет доступен по адресу > После этого API будет доступен по адресу [https://localhost:7287/swagger/index.html](https://localhost:5070/swagger/index.html).

В итоге вы должны увидеть следующее:
<img width="1513" height="716" alt="image" src="https://github.com/user-attachments/assets/803dc793-00f6-4a81-bd2f-7f8362819a35" />

### Загрузка файла
<img width="1479" height="1056" alt="image" src="https://github.com/user-attachments/assets/5e5dc264-4f64-48da-beb3-7a32eb1eabee" />

### Поиск рекламной площадки
<img width="1439" height="885" alt="image" src="https://github.com/user-attachments/assets/464d4192-3957-4e7d-a0b4-f3c12a4ec7ea" />
<img width="1435" height="881" alt="image" src="https://github.com/user-attachments/assets/d24a96ae-4101-4d9a-b3ec-8a1e6cd54c25" />

# API Endpoints
- Загрузка файла с данными: POST /api/Platforms/upload
- Поиск площадки в регионе: GET /api/Platform/search

