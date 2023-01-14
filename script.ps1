docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=NotAll0wedForPublic" -p 7821:1433 -d --name KriniteAuthServer-ResourceDataAPI mcr.microsoft.com/mssql/server:latest
dotnet dev-certs https -ep %PATH%\dev-cert.pfx -p "DoNotTellAny0ne"
dotnet dev-certs https --trust
docker inspect db9c472734bfea92aa9d3d9880a990832a6e2fe0d5e52f52d38aadd2915fbdd4

<# Install IdentityServer UI #> 
$source = "https://github.com/IdentityServer/IdentityServer4.Quickstart.UI/archive/main.zip"
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
Invoke-WebRequest $source -OutFile ui.zip

Expand-Archive ui.zip

if (!(Test-Path -Path Quickstart))  { mkdir Quickstart }
if (!(Test-Path -Path Views))       { mkdir Views }
if (!(Test-Path -Path wwwroot))     { mkdir wwwroot }

copy .\ui\IdentityServer4.Quickstart.UI-main\Quickstart\* Quickstart -recurse -force
copy .\ui\IdentityServer4.Quickstart.UI-main\Views\* Views -recurse -force
copy .\ui\IdentityServer4.Quickstart.UI-main\wwwroot\* wwwroot -recurse -force

del ui.zip
del ui -recurse