## About
This is a URL shortener webpage that supports bulk-shortening via CSV upload.

Query parameters are preserved.

## Tech
1. .NET Core 3.1 (MVC)
1. Entity Framework Core 3.1 (model-first)
1. .NET Standard 2
1. nUnit 3

## Running the URL shortener (MVC app)
1. Download & install the [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1) (or a more recent minor version)
1. Open `UrlShortener.sln` in Visual Studio 2019
1. Setup your connection string in `UrlShortener\appsettings.json`
1. In Visual Studio solution explorer, right-click "UrlShortener" & select "Set as Startup Project"
1. Open nuget package CLI
1. `Update-Database -Project UrlShortener.EntityFrameworkCore -StartupProject UrlShortener -Migration InitialMigration`
1. Select "IIS Express" & click the green play button (at the top)
1. Goto `https://localhost:44319/`. You will now see the URL shortener webpage running via localhost

## Running the test suite
1. In Visual Studio, click "Test" (at the top) > "Test Explorer"
1. Run the test suite by clicking the play button (the one with the 2 overlapping green play symbols)
