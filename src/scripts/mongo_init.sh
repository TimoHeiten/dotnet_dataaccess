#! /bin/bash

docker container run -d --name mgdb -p 27017:27017 mongo:4.0.10-xenial
