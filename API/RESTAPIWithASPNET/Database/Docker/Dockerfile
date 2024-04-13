FROM mysql:5.7.22
EXPOSE 3306
COPY ./API/RESTAPIWithASPNET/Database/Migrations/ /home/database/
COPY ./API/RESTAPIWithASPNET/Database/Datasets/ /home/database/
COPY ./API/RESTAPIWithASPNET/Database/Docker/init_database.sh/ /docker-entrypoint-initdb.d/init_database.sh