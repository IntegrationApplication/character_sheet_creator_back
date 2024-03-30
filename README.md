# character_sheet_creator_back

This is the backend of the character sheet creator

## Docker

```bash
dotnet ef migrations add InitialCreate
docker compose up
```

**NOTE**: if the api starts before the db initialization is done, run `docker
compose restart api`.

We can use the `test.sh` script to insert an element in the database.
