# Randal's Video Store - Example API

## Usage

1. checkout the code
2. open the subfolder `RandalsVideoStore.Api` in VS Code. It should ask to add resources; press yes. 
3. open a terminal
4. `dotnet watch run`
5. <https://localhost:5000/swagger/index.html>.
6. Behold!

## Some general remarks

- Notice we have pretty generic error handling; normally you'd make a distinction between user errors (4xx) and server errors (5xx)
- This code is fully sync (not async); this is something we will fix in the future
- The organisation of the code is subjective; make sure you pick something you can work with and makes sense for your team.
- If you got something stable in your codebase: make sure you create a new feature branch if you start on the next thing.