FROM debian:bullseye-slim AS build1
WORKDIR /root

RUN apt-get update && apt-get install -y binutils gcc

ADD wrapper.c /root/
RUN gcc -shared  -ldl -fPIC -o wrapper.so wrapper.c


FROM mcr.microsoft.com/mssql/server:2017-latest-ubuntu
COPY --from=build1 /root/wrapper.so /root/


CMD LD_PRELOAD=/root/wrapper.so /opt/mssql/bin/sqlservr