FROM microsoft/aspnetcore:1.1
ARG source
WORKDIR /app
ENV REDIS_URL redis:6379
EXPOSE 80
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "WebApplication1.dll"]