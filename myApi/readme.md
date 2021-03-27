# Useful docker commands

- docker build -t myapi .

- docker volume create myvolume

- docker run -it --name myapiwithvolume -v myvolume:/app/Uploads -p 8000:80 myapi