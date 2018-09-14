docker build -t mystore.web .
docker tag mystore.web spetz/mystore.web
docker login -u $(docker.username) -p $(docker.password)
docker push spetz/mystore.web