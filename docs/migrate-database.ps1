# Script para criar as migrations e atualizar o banco de dados

Write-Host "==================================" -ForegroundColor Cyan
Write-Host "SimplePDV - Migration Database" -ForegroundColor Cyan
Write-Host "==================================" -ForegroundColor Cyan
Write-Host ""

$apiPath = ".\src\Backend\SimplePDV.API"
$infraPath = ".\src\Backend\SimplePDV.Infrastructure"

# Verificar se está no diretório raiz
if (-not (Test-Path "SimplePDV.sln")) {
    Write-Host "ERRO: Execute este script no diretório raiz do projeto (onde está SimplePDV.sln)" -ForegroundColor Red
    exit 1
}

Write-Host "1. Criando migration inicial..." -ForegroundColor Yellow
Set-Location $infraPath
dotnet ef migrations add InitialCreate --startup-project ../SimplePDV.API

if ($LASTEXITCODE -ne 0) {
    Write-Host "ERRO ao criar migration" -ForegroundColor Red
    Set-Location ../../../
    exit 1
}

Write-Host "2. Aplicando migration ao banco de dados..." -ForegroundColor Yellow
dotnet ef database update --startup-project ../SimplePDV.API

if ($LASTEXITCODE -ne 0) {
    Write-Host "ERRO ao aplicar migration" -ForegroundColor Red
    Set-Location ../../../
    exit 1
}

Set-Location ../../../

Write-Host ""
Write-Host "==================================" -ForegroundColor Green
Write-Host "Banco de dados criado com sucesso!" -ForegroundColor Green
Write-Host "==================================" -ForegroundColor Green
Write-Host ""
Write-Host "Usuario padrao criado:" -ForegroundColor Cyan
Write-Host "  Login: admin" -ForegroundColor White
Write-Host "  Senha: admin123" -ForegroundColor White
