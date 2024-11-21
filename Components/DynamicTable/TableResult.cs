using Microsoft.AspNetCore.Components;

namespace BlazorShopServer.Components.DynamicTable;

public class TableResult<T> where T : class
{
    /// <summary>
    /// Lista de colunas a serem exibidas.
    /// </summary>
    public List<TableColumn<T>> Columns { get; set; } = new();

    /// <summary>
    /// Lista de itens exibidos na tabela.
    /// </summary>
    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();

    /// <summary>
    /// Total de itens disponíveis.
    /// </summary>
    public int TotalItems { get; set; }

    /// <summary>
    /// Callback acionado ao mudar a página.
    /// </summary>
    public NavigationManager Navigation { get; set; }

    public string NavigateTo { get; set; }

    public void Navigate(string? destiny = "")
    {
        Navigation.NavigateTo($"{NavigateTo}/{destiny}");
    }
}