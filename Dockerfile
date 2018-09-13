FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY ./src/MyStore.Web/bin/docker .
ENV ASPNETCORE_ENVIRONMENT docker
ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000
ENTRYPOINT dotnet MyStore.Web.dll