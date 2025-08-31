# 🚀 API для задачи "Рекламные площадки"

Этот проект представляет собой **ASP.NET Core Web API** для загрузки и поиска рекламных площадок.  
API снабжён удобным интерфейсом **Swagger UI** для тестирования эндпоинтов.

---

## ✅ Предварительные требования

Перед запуском убедитесь, что у вас установлено:

- [.NET 9 SDK (с поддержкой ASP.NET Core)](https://dotnet.microsoft.com/download/dotnet/9.0)  
- [xUnit](https://xunit.net/) — для модульного тестирования  
- [FluentAssertions](https://fluentassertions.com/) — для удобных проверок в тестах  
- [Swashbuckle.AspNetCore (Swagger)](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)  

---

## 🔧 Установка и запуск

### 1. Клонируйте репозиторий
```bash
git clone https://github.com/MaximKortnev/AdvertPlatforms.git
```

### 2. Перейдите в директорию проекта
```bash
cd AdvertPlatforms
```

### 3. Запустите проект
```bash
dotnet run
```

После успешного запуска API будет доступно по адресу:  
👉 [https://localhost:5070/swagger/index.html](https://localhost:5070/swagger/index.html)

---

## 📖 Использование API

### 1. Swagger UI
Откройте в браузере:  
[https://localhost:5070/swagger/index.html](https://localhost:5070/swagger/index.html)

В интерфейсе можно протестировать все доступные эндпоинты.

---

### 2. Эндпоинты

#### 📂 Загрузка файла с данными
```
POST /api/Platforms/upload
```
Позволяет загрузить текстовый файл с описанием рекламных площадок.  
Формат строк в файле:
```
Название: /loc1, /loc2, ...
```

Пример запроса в Swagger UI:
- В поле **file** выберите `.txt` файл  
- Нажмите **Execute**  

---

#### 🔍 Поиск рекламной площадки
```
GET /api/Platforms/search?location=/loc1
```

- **location** — путь, начинающийся с `/`  
- В ответе возвращается список найденных площадок  

Пример:
```
GET /api/Platforms/search?location=/moscow
```

---

## 🖼 Скриншоты

### Swagger UI (главная страница)
![Swagger UI](https://github.com/user-attachments/assets/803dc793-00f6-4a81-bd2f-7f8362819a35)

### Загрузка файла
![Загрузка файла](https://github.com/user-attachments/assets/5e5dc264-4f64-48da-beb3-7a32eb1eabee)

### Поиск площадки
![Поиск 1](https://github.com/user-attachments/assets/464d4192-3957-4e7d-a0b4-f3c12a4ec7ea)  
![Поиск 2](https://github.com/user-attachments/assets/d24a96ae-4101-4d9a-b3ec-8a1e6cd54c25)

---

## 📌 Эндпоинты

- `POST /api/Platforms/upload` — загрузка файла с данными  
- `GET /api/Platforms/search` — поиск площадки в регионе  

---
