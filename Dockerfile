from mcr.microsoft.com/dotnet/sdk:10.0.103-aot as build
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /app --no-self-contained

FROM base as final
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ComprasVentas.dll"]