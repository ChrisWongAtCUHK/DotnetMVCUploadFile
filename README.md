# Deploy to Heroku
```
heroku apps:create heroku-dotnet-mvc
heroku create --buildpack http://github.com/heroku/dotnet-buildpack.git
heroku buildpacks:set heroku/dotnet
```