using MAU.HttpCliente.App.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using static System.Console;
using System.Collections.Generic;

namespace MAU.HttpCliente_v5.App
{
  internal class Program
  {
    static async Task Main(string[] args)
    {
      WriteLine("----------------------------");
      WriteLine("     HackerRank - Autores");
      WriteLine("-----------------------------");
      Write("Entre com o nome de um autor: ");
      var autor = ReadLine();

      // "https://jsonmock.hackerrank.com/api/article_users";
      string uri = "https://jsonmock.hackerrank.com/api/articles";
      var cliente = new HttpClient();

      var totalPages = 1;
      List<string> titles = new();

      for (int i = 1; i <= totalPages; i++)
      {
        JsonRoot resultado = await cliente.GetFromJsonAsync<JsonRoot>($"{uri}?author={autor}&page={i}");

        var paginas = resultado.TotalPages;

        WriteLine($"Página atual: {resultado.Page}");
        WriteLine($"Registros por página: {resultado.PerPage}");
        WriteLine($"Registros totais: {resultado.Total}");
        WriteLine($"Total de Páginas: {paginas}");

        foreach (var item in resultado.Data)
        {
          if (!string.IsNullOrEmpty(item.Title))
          {
            titles.Add(item.Title);
          }
          else if (!string.IsNullOrEmpty(item.StoryTitle))
          {
            titles.Add(item.StoryTitle);
          }
        }
        if (paginas > 1) totalPages = paginas;
      }

      WriteLine("");
      WriteLine("----------------------------");
      WriteLine("    Lista de títulos:");
      WriteLine("----------------------------");
      foreach (var item in titles)
      {
        WriteLine(item);
      }
      ReadKey();
    }
  }
}
