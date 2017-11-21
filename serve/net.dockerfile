FROM  microsoft/dotnet
COPY ./app /app
WORKDIR /app
RUN dotnet restore
ENTRYPOINT ["dotnet","run"]