
#CE IMAGINE SE FOLOSESTE :
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

#UNDE SE RULEAZA COMENZILE
WORKDIR /app
#PRIMUL PUNCT: DE UNDE SE IA CODUL SURSA, AL DOILEA, UNDE SPUNE(/APP)
COPY . .

RUN dotnet tool install --global dotnet-ef --version 6.0
RUN dotnet restore 

RUN dotnet build --configuration release

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

#comenzile se ruleaza doar cand containerul ruleaza
ENTRYPOINT ["dotnet","MoviesAPI.dll"]