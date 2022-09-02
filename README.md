### [Middleware](https://docs.microsoft.com/ru-ru/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0)

### Краткое описание

#### Authentication

Обеспечивает поддержку аутентификации. 
Прежде HttpContext.User нужно. Терминал для обратных вызовов OAuth.

[Introduction to Identity on ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-7.0&tabs=visual-studio) 

#### Authorization 

Обеспечивает поддержку авторизации.  
Сразу после ПО промежуточного слоя аутентификации.

[Introduction to authorization in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/authorization/introduction?view=aspnetcore-7.0) 

#### Cookie Policy

Отслеживает согласие пользователей на хранение личной информации и обеспечивает соблюдение минимальных стандартов для полей cookie, таких как secure и SameSite.
Перед промежуточным программным обеспечением, которое выдает файлы cookie. Примеры: Аутентификация, сеанс, MVC (TempData).

#### CORS

Настраивает Совместное использование Ресурсов Из Разных Источников.
Перед компонентами, использующими CORS. UseCors в настоящее время должны идти до UseResponseCaching из-за этой ошибки.

#### DeveloperExceptionPage

Создает страницу с информацией об ошибке, которая предназначена для использования только в среде разработки. Перед компонентами, которые генерируют ошибки.
Шаблоны проекта автоматически регистрируют это промежуточное программное обеспечение в качестве первого промежуточного программного обеспечения в конвейере, когда среда находится в стадии разработки.