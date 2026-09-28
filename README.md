# Video Game Catalogue

A small full-stack app for browsing and editing a list of video games. It has two pages: a **Browse** page where you can search, sort and page through the catalogue, and an **Edit** page with a validated form for updating a game.

The back end is an ASP.NET Core Web API backed by SQL Server (via EF Core), and the front end is an Angular single-page app styled with Bootstrap.

This was built as an assignment, so the focus is on writing clean, well-structured code and showing good use of the frameworks, rather than on visual polish.

## What's in here

```
VideoGameCatalogue/
├── src/
│   ├── VideoGames.Api/        # ASP.NET Core Web API
│   └── video-games-web/       # Angular app
└── tests/
    └── VideoGames.Api.Tests/  # xUnit tests for the API
```

A full write-up of the design and the reasoning behind it lives in **video-game-catalogue.md** at the root of the repo. `design-prompt.md` is a prompt you can hand to a design tool (Figma AI, v0, etc.) to generate mockups that match this build.

## What it does

- **Browse page** — search by title, platform or genre; sort by any column; server-side paging.
- **Edit page** — a reactive form with validation, save/cancel, and a "you have unsaved changes" pop-up if you try to navigate away with edits pending.
- **Optimistic concurrency** — if two people edit the same game at once, the second save gets a clear "someone else changed this" message instead of silently overwriting the first person's changes.
- **Error handling and logging** — unhandled errors are caught centrally and logged, and every request gets a one-line log entry (method, path, status, how long it took).

## Before you start

You'll need:

- **.NET 10 SDK**
- **Node.js** (v20.19+ or v22.12+) and **npm**
- **Angular CLI** — `npm i -g @angular/cli`
- A **SQL Server** instance to point the API at. Any of these work:
  - SQL Server Express
  - SQL Server LocalDB (comes with Visual Studio, usually already installed on a dev machine)
  - A full SQL Server instance, local or remote

## Setting up the database

1. Open `src/VideoGames.Api/appsettings.json` and check the `Catalogue` connection string points at a SQL Server instance you actually have running. Out of the box it's set up for LocalDB:

   ```json
   "Catalogue": "Server=(localdb)\\MSSQLLocalDB;Database=VideoGameCatalogue;Trusted_Connection=True;TrustServerCertificate=True"
   ```

   If you're using SQL Server Express instead, change it to something like `Server=.\SQLEXPRESS;...`.

2. From `src/VideoGames.Api`, apply the migrations:

   ```bash
   cd src/VideoGames.Api
   dotnet ef database update
   ```

   This creates the database and seeds it with 25 sample games. (If `dotnet ef` isn't installed, run `dotnet tool install --global dotnet-ef` first.)

The API also applies pending migrations automatically on startup when running in Development, so step 2 is mostly a formality — but it's worth running once so you're not surprised the first time you hit `dotnet run`.

## Running the API

```bash
cd src/VideoGames.Api
dotnet run
```

By default this listens on `http://localhost:5050`. You can check it's working by opening `http://localhost:5050/api/video-games` in a browser or with curl — you should get back a page of JSON games.

## Running the front end

```bash
cd src/video-games-web
npm install
ng serve
```

Then open `http://localhost:4200`. The app is already pointed at `http://localhost:5050/api` in `src/environments/environment.ts` — if you changed the API's port, update that file too.

## Running the tests

**Back end** (from the repo root):

```bash
dotnet test
```

**Front end** (from `src/video-games-web`):

```bash
ng test
```

## Trying out the concurrency conflict

This is a nice thing to demo: open the same game in two browser tabs, save your change in the first tab, then save a different change in the second tab. The second save will fail with a 409 and a message telling you someone else already changed that game.

## A couple of notes

- SSL and login/authentication are deliberately left out — they weren't part of the brief.
- If `dotnet ef database update` can't reach your SQL Server instance, that's almost always the connection string in `appsettings.json`, not the code — double check the server name matches an instance you actually have running (`sqllocaldb info` will list your LocalDB instances if you're not sure).
