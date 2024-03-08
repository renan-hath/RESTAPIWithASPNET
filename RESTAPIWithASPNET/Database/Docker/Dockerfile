FROM mysql:5.7.22
EXPOSE 3306
COPY ./RESTAPIWithASPNET/Database/Migrations/ /home/database/
COPY ./RESTAPIWithASPNET/Database/Datasets/ /home/database/
COPY ./RESTAPIWithASPNET/Database/Docker/init_database.sh/ /docker-entrypoint-initdb.d/init_database.sh