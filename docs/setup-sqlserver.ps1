# Script para configurar SQL Server no Docker

Write-Host "Parando container existente (se houver)..." -ForegroundColor Yellow
docker stop sqlserver 2>$null
docker rm sqlserver 2>$null

Write-Host "Criando container SQL Server 2022..." -ForegroundColor Green
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=*automa1" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest

Write-Host "Aguardando SQL Server inicializar (30 segundos)..." -ForegroundColor Cyan
Start-Sleep -Seconds 30

Write-Host "Verificando status do container..." -ForegroundColor Yellow
docker ps -a | Select-String "sqlserver"

Write-Host "`nTestando conexão..." -ForegroundColor Cyan
docker exec sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "*automa1" -C -Q "SELECT 'SQL Server está funcionando!' AS Status"

Write-Host "`nAplicando migrations..." -ForegroundColor Green
Set-Location "c:\Users\gabriel\Documents\projetos\SimplePDV\src\Backend\SimplePDV.API"
dotnet ef database update --project ..\SimplePDV.Infrastructure

Write-Host "`nSQL Server configurado com sucesso!" -ForegroundColor Green
Write-Host "Connection String: Server=localhost,1433;Database=SimplePDV;User Id=sa;Password=*automa1" -ForegroundColor White
