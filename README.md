# BlazorShopServer - Table Dynamic

Repositório que contem a implementação de uma tabela genérica.

```C#
<DynamicTable ElementName="categorias"
              Data="@_categories"
              ItemsPerPage="@Take"
              CurrentPage="@CurrentPage"
              OnTakeChange="OnTakeChanged"
              OnPageChange="LoadPage" />
```

O componente possui os seguintes Parametros de funcionamento.
- ElementName: define o nome dos elementos listados em baixo da tabela. Por padrão possui o valor "elementos".
- Data: recebe uma classe genérica chamada TableResult que possui diversos atributos auxiliares.
- ItemsPerPage: seria o padrão Take, para a tabela definir o número de valores que devem ser inseridos na busca da tabela.
- CurrentPage: Padrão Skir, para a tabela fazer a paginação.
- OnTakeChange e OnPageChange: São dois parâmetros de Callback para fazer a parte de paginação da tabela.

## Exemplo de funcionamento

### Inicialização da Tabela
 ![image](https://github.com/user-attachments/assets/6c159de3-4c7b-4e2c-9007-7fd8aa031aca)

### Tabela após ter seu valor de filtro Take alterado
 ![image](https://github.com/user-attachments/assets/64257e45-d75e-4bf8-a33e-1c85c2dda778)

### Tabela executando paginação
![image](https://github.com/user-attachments/assets/4fe3947f-0080-4e60-bb35-300b8f5781b2)

## Construção da tabela - Exemplo de implementação

### Classe TableColumn
```C#
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

```
### Classe TableResult
```C#
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

      // Elementos de navegação interna não implementados até o momento atual
      #region Navigation

      /// <summary>
      /// Callback acionado ao mudar a página.
      /// </summary>
      public NavigationManager Navigation { get; set; }
  
      public string NavigateTo { get; set; }
  
      public void Navigate(string? destiny = "")
      {
          Navigation.NavigateTo($"{NavigateTo}/{destiny}");
      }

      #endregion
  }
```
### Criação dos elementos do Componente da tabela dinâmica
```C#
  <!-- Criação de um TableResult de Categorias -->
  private TableResult<Category> _categories = new();

  <!-- Criação dos elementos da tabela de Categorias -->
  private List<TableColumn<Category>> ProductColumns => new()
  {
      new TableColumn<Category> { Header = "Id", Render = p => (MarkupString)$"{p.Id}" },
      new TableColumn<Category> { Header = "Título", RenderFragment = p => @<a href="@($"/categories/{p.Id}")">@p.Title</a> },
      new TableColumn<Category>
      {
          Header = "Ações",
          RenderFragment = p => @<div>
              <a href="@($"categories/edit/{p.Id}")" class="btn btn-warning">
                  <i class="bi bi-pencil-square"></i> Alterar
              </a>
              &nbsp;
              <a href="@($"categories/delete/{p.Id}")" class="btn btn-outline-danger">
                  <i class='bi bi-trash'></i> Excluir
              </a>
          </div>
      }
  };

  private int CurrentPage { get; set; } = 1;
  private int Take { get; set; } = 10;
```

### Funções de chamada da tela
```C#
  private async Task OnTakeChanged(int newTakeValue)
  {
      Take = newTakeValue;
      await LoadPage();
  }

  private async Task LoadPage(int page = 1)
  {
      CurrentPage = page;
      _categories.TotalItems = await GetCategoriesTotal(); // Função para obter valor total de categorias cadastradas.
      PaginateElements(await GetCategories(), CurrentPage - 1, Take); //Função que faz a paginação de elementos e os envia para dentro do nosso TableResult
  }

  private void PaginateElements(List<Category> categories, int skip, int take)
  {
      if (categories.Any())
          _categories.Items = categories.Skip(skip * take).Take(take).ToList();
      else
          _categories.Items = null;

      _categories.Navigation = Navigation;
      _categories.NavigateTo = "products";
      _categories.Columns = ProductColumns;
  }
```
