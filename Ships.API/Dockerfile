#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ships.API/Ships.API.csproj", "Ships.API/"]
COPY ["Ships.Application/Ships.Application.csproj", "Ships.Application/"]
COPY ["Ships.DTO/Ships.DTO.csproj", "Ships.DTO/"]
COPY ["Ships.Domain/Ships.Domain.csproj", "Ships.Domain/"]
COPY ["Ships.Common/Ships.Common.csproj", "Ships.Common/"]
RUN dotnet restore "Ships.API/Ships.API.csproj"
COPY . .
WORKDIR "/src/Ships.API"
RUN dotnet build "Ships.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ships.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ships.API.dll"]