# Deploy to Heroku
```
heroku apps:create heroku-dotnet-mvc
heroku create --buildpack http://github.com/heroku/dotnet-buildpack.git
heroku buildpacks:set heroku/dotnet
```

## Aspsnippets
- [ASP.Net Core Razor Pages: Create Word Document using OpenXml](https://www.aspsnippets.com/Articles/5142/ASPNet-Core-Razor-Pages-Create-Word-Document-using-OpenXml/)