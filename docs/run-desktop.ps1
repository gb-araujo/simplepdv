# Script para executar o Cliente WPF

Write-Host "==================================" -ForegroundColor Cyan
Write-Host "SimplePDV - Iniciando Desktop" -ForegroundColor Cyan
Write-Host "==================================" -ForegroundColor Cyan
Write-Host ""

$wpfPath = ".\src\Client\SimplePDV.WPF"

if (-not (Test-Path "SimplePDV.sln")) {
    Write-Host "ERRO: Execute este script no diretório raiz do projeto" -ForegroundColor Red
    exit 1
}

Set-Location $wpfPath

Write-Host "Compilando projeto..." -ForegroundColor Yellow
dotnet build

if ($LASTEXITCODE -ne 0) {
    Write-Host "ERRO na compilacao" -ForegroundColor Red
    Set-Location ../../../
    exit 1
}

Write-Host ""
Write-Host "Iniciando aplicacao desktop..." -ForegroundColor Green
Write-Host ""

dotnet run
