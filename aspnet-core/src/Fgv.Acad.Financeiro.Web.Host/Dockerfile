#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:2.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:2.1 AS build
WORKDIR /src
COPY ["src/Fgv.Acad.Financeiro.Web.Host/Fgv.Acad.Financeiro.Web.Host.csproj", "src/Fgv.Acad.Financeiro.Web.Host/"]
COPY ["src/Fgv.Acad.Financeiro.Web.Core/Fgv.Acad.Financeiro.Web.Core.csproj", "src/Fgv.Acad.Financeiro.Web.Core/"]
COPY ["src/Fgv.Acad.Financeiro.EntityFrameworkCore/Fgv.Acad.Financeiro.EntityFrameworkCore.csproj", "src/Fgv.Acad.Financeiro.EntityFrameworkCore/"]
COPY ["src/Fgv.Acad.Financeiro.Core/Fgv.Acad.Financeiro.Core.csproj", "src/Fgv.Acad.Financeiro.Core/"]
COPY ["src/Fgv.Acad.Financeiro.Core.Shared/Fgv.Acad.Financeiro.Core.Shared.csproj", "src/Fgv.Acad.Financeiro.Core.Shared/"]
COPY ["src/Fgv.Acad.Financeiro.Application/Fgv.Acad.Financeiro.Application.csproj", "src/Fgv.Acad.Financeiro.Application/"]
COPY ["src/Fgv.Acad.Financeiro.Application.Shared/Fgv.Acad.Financeiro.Application.Shared.csproj", "src/Fgv.Acad.Financeiro.Application.Shared/"]
RUN dotnet restore "src/Fgv.Acad.Financeiro.Web.Host/Fgv.Acad.Financeiro.Web.Host.csproj"
COPY . .
WORKDIR "/src/src/Fgv.Acad.Financeiro.Web.Host"
RUN dotnet build "Fgv.Acad.Financeiro.Web.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fgv.Acad.Financeiro.Web.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fgv.Acad.Financeiro.Web.Host.dll"]