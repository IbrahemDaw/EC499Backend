# EC499

## Sections

- [General Instructions](#general-instructions)
- [Local Setup Prerequisites](#local-setup-prerequisites)
- [Running SQL Server using docker(optional)](#running-sql-server-using-dockeroptional)
- [Usefull Commands](#usefull-commands)

### General Instructions

- Clone the repo on your machine.
- Ensure you have the latest `develop` branch.
- Create a branch with your changes.
- Use clear commit messages.
- Open a Pull Request

### Local Setup Prerequisites

- Download .NET8 [installation](https://dotnet.microsoft.com/en-us/download)
- Install dotnet ef tools
```sh
dotnet tool install --global dotnet-ef
```

### Running SQL Server using docker(optional)

- Install docker & docker compose
- Run required docker containers
```sh
docker-compose up -d
```
- Update database with latest migration

```sh
dotnet ef database update  -p Infrastructure -s ManagementApi
```

### Usefull commands

- Create migration
```sh
dotnet ef migrations  -p Infrastructure -s ManagementApi add "MIGRATION_NAME"
```

- Update database
```sh
dotnet ef database update  -p Infrastructure -s ManagementApi
```
