
#import the dotnet SDK from microsoft
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

#copy the dependency file and restore them (by Nuget)
COPY *.csproj ./
RUN dotnet restore 


COPY . ./   
RUN dotnet publish  -c Release -o out


#generate runtime image in this way we are not using the all sdk 
FROM mcr.microsoft.com/dotnet/aspnet:5.0 
WORKDIR /app
#for http
EXPOSE 80
#for https
EXPOSE 443 
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "CatalogServer.dll"]
