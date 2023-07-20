@echo off

docker-compose -f .\docker-compose.yml -f .\docker-compose.api.yml up --build --detach
REM docker-compose -f .\docker-compose.yml up --build --detach

echo "Local environment has started."
pause
docker-compose -f .\docker-compose.yml -f .\docker-compose.api.yml down
REM docker-compose -f .\docker-compose.yml down