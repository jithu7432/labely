all: build

r: run

.PHONY: build
build:
	dotnet build --no-restore

.PHONY: run
run:
	dotnet run --no-restore --project src/Labely.Console/Labely.Console.csproj


