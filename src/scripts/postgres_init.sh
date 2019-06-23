docker container run -d --name postgres -p 5432:5432 -e POSTGRES_DB=udemy -e POSTGRES_USER=udemy postgres:10-alpine

sleep 10

docker container exec -i postgres mkdir -p /backup/
docker cp ./../data/psql/udemy.backup postgres:/backup

docker container exec -i postgres pg_restore -d udemy -U udemy -h localhost /backup/udemy.backup