#! /bin/bash
docker image pull redis:5.0-alpine

docker container run -d --name redis-cache -p 6379:6379 redis:5.0-alpine

