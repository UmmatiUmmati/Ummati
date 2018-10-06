FROM node:10.11.0-stretch AS node
WORKDIR /src/Source/Ummati/Client
COPY Source/Ummati/Client/package.json .
RUN npm install
COPY Source/Ummati/Client/ .
RUN npm run build

FROM microsoft/dotnet:2.1.403-sdk AS dotnet
WORKDIR /src
COPY *.sln ./
COPY MinimumRecommendedRulesWithStyleCop.ruleset ./
COPY Source/Ummati.csproj Source/
WORKDIR /src/Source/
RUN dotnet restore
COPY Source/ .
COPY --from=node /src/Source/Ummati/Client/dist Ummati/Client/dist/
RUN dotnet publish -c Release -o /app

FROM microsoft/dotnet:2.1.5-aspnetcore-runtime-alpine AS final
WORKDIR /app
COPY --from=dotnet /app .
ENTRYPOINT ["dotnet", "Ummati.dll"]