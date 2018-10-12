FROM node:10.11.0-stretch AS node
WORKDIR /src
COPY Source/Ummati/Client/package.json .
RUN npm install
COPY Source/Ummati/Client/ .
RUN npm run build

FROM microsoft/dotnet:2.1.403-sdk AS dotnet
WORKDIR /src
COPY *.sln ./
COPY MinimumRecommendedRulesWithStyleCop.ruleset ./
COPY Source/Ummati/Ummati.csproj Source/Ummati/
WORKDIR /src/Source/Ummati/
RUN dotnet restore
COPY Source/Ummati/ .
RUN dotnet publish -c Release -o /app
WORKDIR /app
COPY --from=node /src/dist Client/dist/

FROM microsoft/dotnet:2.1.5-aspnetcore-runtime AS final
WORKDIR /app
COPY --from=dotnet /app .
ENTRYPOINT ["dotnet", "Ummati.dll"]
