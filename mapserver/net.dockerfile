FROM  microsoft/dotnet
RUN apt-get update;
RUN apt-get install gcc -y;
COPY ./ /app
WORKDIR /app/C
RUN gcc -o imagemagic.exe imagemagic.c
WORKDIR /app
RUN dotnet restore
ENTRYPOINT ["dotnet","run"]