# Script para executar a API

Write-Host "==================================" -ForegroundColor Cyan
Write-Host "SimplePDV - Iniciando API" -ForegroundColor Cyan
Write-Host "==================================" -ForegroundColor Cyan
Write-Host ""

$apiPath = ".\src\Backend\SimplePDV.API"

if (-not (Test-Path "SimplePDV.sln")) {
    Write-Host "ERRO: Execute este script no diretório raiz do projeto" -ForegroundColor Red
    exit 1
}

Set-Location $apiPath

Write-Host "Compilando projeto..." -ForegroundColor Yellow
dotnet build

if ($LASTEXITCODE -ne 0) {
    Write-Host "ERRO na compilacao" -ForegroundColor Red
    Set-Location ../../../
    exit 1
}

Write-Host ""
Write-Host "Iniciando API..." -ForegroundColor Green
Write-Host "URL: https://localhost:7000" -ForegroundColor Cyan
Write-Host "Swagger: https://localhost:7000/swagger" -ForegroundColor Cyan
Write-Host ""

dotnet run
