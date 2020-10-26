#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Web.Api.CodeChallengeJSM/Web.Api.CodeChallengeJSM.csproj", "Web.Api.CodeChallengeJSM/"]
COPY ["Structure.CodeChallengeJSM/Structure.CodeChallengeJSM.csproj", "Structure.CodeChallengeJSM/"]
COPY ["Libary.CodeChallengeJSM/Libary.CodeChallengeJSM.csproj", "Libary.CodeChallengeJSM/"]
RUN dotnet restore "Web.Api.CodeChallengeJSM/Web.Api.CodeChallengeJSM.csproj"
COPY . .
WORKDIR "/src/Web.Api.CodeChallengeJSM"
RUN dotnet build "Web.Api.CodeChallengeJSM.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.Api.CodeChallengeJSM.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.Api.CodeChallengeJSM.dll"]