using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Linq;

public class DiaValor
{
    [JsonPropertyName("dia")]
    public int Dia { get; set; }

    [JsonPropertyName("valor")]
    public double Valor { get; set; }
}

public class Program
{
    static void Main()
    {
        string caminhoArquivo = @"C:\Users\55229\source\repos\Desafio3\Desafio3\Data\dados.json";

        if (!File.Exists(caminhoArquivo)) //teste para ver se o arquivo realmente existe, fiz isso pois no arquivo anterior que tinha criado eu coloquei "Desafio 3" e esse espaço fez eu ter uma dor de cabeça até achar o erro kkk.
        {
            Console.WriteLine("Arquivo não encontrado");
            return;
        }

        
        string json = File.ReadAllText(caminhoArquivo); //realizar a leitura do arquivo
        Console.WriteLine("Arquivo JSON lido com sucesso.");

        
        List<DiaValor> dados = JsonSerializer.Deserialize<List<DiaValor>>(json); //fazer a disserialização de dias e valores.
        Console.WriteLine("JSON desserializado com sucesso.");

        
        foreach (var item in dados) //exibir os dias e valores.
        {
            Console.WriteLine($"Dia: {item.Dia}, Valor: R$ {item.Valor:F2}");
        }

        var mostrarMenorValor = DiaComMenorValor(dados);
        Console.WriteLine("Dia com menor valor");
        foreach (var dia in mostrarMenorValor)
        {
            Console.WriteLine($"Dia: {dia.Dia}, Valor: R$ {dia.Valor:F2}");
        }

        var mostrarDiaComMaiorValor = DiaComMaiorValor(dados);
        Console.WriteLine("Dia com maior valor");
        {
            Console.WriteLine($"Dia: {mostrarDiaComMaiorValor.Dia}, Valor: R$ {mostrarDiaComMaiorValor.Valor:F2}");
        }

        var mostrarMediaDeValores = MediaDosValores(dados);
        Console.WriteLine("Média dos valores");
        {
            Console.WriteLine($"A média total dos valores é: R$ {mostrarMediaDeValores:F2}");
        }
        var mostrarDiasMaioresQueMedia = DiasComValoresSuperioresAMedia(dados, mostrarMediaDeValores);
        Console.WriteLine("Dias em que os valores superaram as médias");
        {
            Console.WriteLine($"Dias com valores acima da media: {string.Join(",", mostrarDiasMaioresQueMedia)}");
        }

       /* var mediaComZ = MediaComZeros(dados);
        {
            Console.WriteLine(mediaComZ.ToString()); Teste para saber se o método sem os dias com valores iguais a 0 estava funcionando.
        }
       */



        //métodos para descobrir os dias com maiores e menores faturamentos, média e os dias em que o faturamento superou a media.

        static List<DiaValor> DiaComMenorValor(List<DiaValor> dados) 
        {
            var diasComMenoresValores = dados.Where(d => d.Valor > 0).ToList(); //metodo para ignorar os dias com valor igual a 0.
            if (!diasComMenoresValores.Any()) //verifica se existem dias com valor miaor que 0
            {

                return new List<DiaValor>(); //retorna uma lista vazia caso nao exista numero maior que 0
            }
            double menorValor = diasComMenoresValores.Min(d => d.Valor);
            return diasComMenoresValores.Where(d => d.Valor == menorValor).ToList(); //retorna o menir valor.
        }

        static DiaValor DiaComMaiorValor(List<DiaValor> dados)
        {
            return dados.OrderByDescending(d => d.Valor).FirstOrDefault();
        }
        static double MediaDosValores(List<DiaValor> dados)
        {
            var valoresMaioresQueZero = dados.Where(d => d.Valor > 0).ToList();
            if (!valoresMaioresQueZero.Any())
            {
              return 0;
            }
          
           return valoresMaioresQueZero.Average(d => d.Valor);
        }
        static List<int> DiasComValoresSuperioresAMedia(List<DiaValor> dados, double media)
        {
            return dados.Where(d => d.Valor > media).Select(d => d.Dia).ToList();
        }

        /*static double MediaComZeros(List<DiaValor> dados)
        {
            return dados.Average(d => d.Valor);      Teste como ja falei logo acima.
        }
        */
    }
}
