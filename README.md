# SimpleBlog

SimpleBlog is an MVC application written in ASP.NET MVC5. The application allows you to manage a blog, add and modify content 
and users etc. Application allows comment on indvidual post. User can register using standard registration or registration via 
facebook. The system has three types of users: administrator, moderator and user-reader. Depending on the role of the user, different 
options are available.

## Getting Started

1. Clone the repository.
2. Restore NuGet package.
3. Add initial migration and update database to seed.
```
Add-Migration 'init'
```
then
```
Update-Database
```
4. Change the AppId and AppSecret in Startup.Auth to allow registration and login using by Facebook app.
5. Run project. 

## Technologies used
- ASP NET MVC5
- Entity Framework 6
- MSSQL database
- JQuery
- HTML, CSS, Bootstrap
- AutoMapper 6

## Application functions
- Add/edit/remove posts
- Add/edit/remove comments and reply
- Add/edit/remove categories
- Register and login with standard way and via Facebook
- Manage users: block, delete, change role
- Search posts by phrase, by category
- Statistics display

## Test login data
- Administrator- login: administrator@gmail.com, password: 1qaz@WSX
- Moderator- login: moderator@gmail.com, password: 1qaz@WSX
- Users- login: user@gmail.com, password: 1qaz@WSX
## Authors

* **Kamil Urbański**
