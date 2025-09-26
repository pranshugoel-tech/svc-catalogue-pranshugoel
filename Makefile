.PHONY: run build test lint fmt docker-build clean

docker:
	docker-compose up --build

build:
	dotnet build src/Service.Catalogue.Api/Service.Catalogue.Api.csproj -c Release

fmt:
	dotnet tool restore || true

dotnet-format:
	dotnet format

lint:
	dotnet build -warnaserror

docker-build:
	docker build -t service-catalog:local .

clean:
	rm -rf ./data ./coverage