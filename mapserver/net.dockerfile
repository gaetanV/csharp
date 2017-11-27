FROM  microsoft/dotnet
RUN apt-get update;
RUN apt-get install gcc -y;
COPY ./ /app
WORKDIR /app/C
RUN gcc -c imagemagic.c
RUN gcc -shared -o imagemagic.dll imagemagic.o

WORKDIR /app
RUN dotnet restore
ENTRYPOINT ["dotnet","run"]