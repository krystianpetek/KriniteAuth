docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=NotAll0wedForPublic" -p 7821:1433 -d --name KriniteAuthServer-ResourceDataAPI mcr.microsoft.com/mssql/server:latest
dotnet dev-certs https -ep %PATH%\dev-cert.pfx -p "DoNotTellAny0ne"
dotnet dev-certs https --trust
docker inspect db9c472734bfea92aa9d3d9880a990832a6e2fe0d5e52f52d38aadd2915fbdd4
