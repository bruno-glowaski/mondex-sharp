# MonDex Sharp

Manage and host Pokédex entries online. Developed over ASP.NET Core and Nuxt 3.

## Features

- [x] Users can read a Pokémon entry;
- [x] Users can manage Pokémon entries;
- [x] Users can import types from PokeAPI;
- [ ] Users can import species from PokeAPI;
- [ ] Users can manage Pokémon abilities;
- [ ] Users can manage Pokémon moves;
- [ ] Users can create accounts;
- [ ] Users can be authenticated and authorized;
- [ ] Users can be promoted to admins;

## Running

### Using Docker Compose

1. First, set up the Hangfire database:

```sh
docker compose up db -d
docker exec -it mondex-sharp-db-1 /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 12345Abç -d Hangfire -Q "CREATE DATABASE Hangfire;"
```

2. Run the other services:

```sh
docker compose up
```
