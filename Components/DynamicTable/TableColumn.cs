using Microsoft.AspNetCore.Components;

namespace BlazorShopServer.Components.DynamicTable;

public class TableColumn<T> where T : class
{
    /// <summary>
    /// Título da coluna.
    /// </summary>
    public string Header { get; set; }

    /// <summary>
    /// Função para renderizar o conteúdo da célula.
    /// </summary>
    public Func<T, MarkupString>? Render { get; set; }

    /// <summary>
    /// Função para renderizar o conteúdo da célula.
    /// </summary>
    public RenderFragment<T>? RenderFragment { get; set; }
}
