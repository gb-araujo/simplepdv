using SimplePDV.Domain.Entities;

namespace SimplePDV.WPF.Services;

public class SincronizacaoService
{
    private readonly ApiService _apiService;
    private readonly ProdutoLocalService _produtoLocalService;
    private readonly VendaLocalService _vendaLocalService;
    private readonly UsuarioLocalService _usuarioLocalService;

    public SincronizacaoService(
        ApiService apiService,
        ProdutoLocalService produtoLocalService,
        VendaLocalService vendaLocalService,
        UsuarioLocalService usuarioLocalService)
    {
        _apiService = apiService;
        _produtoLocalService = produtoLocalService;
        _vendaLocalService = vendaLocalService;
        _usuarioLocalService = usuarioLocalService;
    }

    public async Task<bool> SincronizarAsync()
    {
        if (!await _apiService.IsOnlineAsync())
            return false;

        try
        {
            // Baixar produtos do servidor
            var produtos = await _apiService.GetAsync<List<Produto>>("produtos");
            if (produtos != null)
            {
                await _produtoLocalService.SincronizarProdutosAsync(produtos);
            }

            // Enviar vendas não sincronizadas
            var vendasNaoSync = await _vendaLocalService.GetNaoSincronizadasAsync();
            foreach (var venda in vendasNaoSync)
            {
                var sucesso = await EnviarVendaAsync(venda);
                if (sucesso)
                {
                    await _vendaLocalService.MarcarComoSincronizadaAsync(venda.Id);
                }
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    private async Task<bool> EnviarVendaAsync(Venda venda)
    {
        var vendaDto = new
        {
            UsuarioId = venda.UsuarioId,
            FormaPagamento = venda.FormaPagamento,
            Itens = venda.Itens.Select(i => new
            {
                ProdutoId = i.ProdutoId,
                Quantidade = i.Quantidade
            }).ToList()
        };

        var resultado = await _apiService.PostAsync<object, object>("/vendas", vendaDto);
        return resultado != null;
    }
}
