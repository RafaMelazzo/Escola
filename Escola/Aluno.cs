// See https://aka.ms/new-console-template for more information

using System.Configuration;
using Newtonsoft.Json;

public class Aluno
{
    public string Nome { get; set; }
    public string Matricula { get; set; }
    public List<double> Notas { get; set; }

    public double Media()
    {
        double mediaNotas = 0;
        foreach (var nota in this.Notas) mediaNotas += nota;
        mediaNotas = mediaNotas / this.Notas.Count;
        
        return mediaNotas;
    }

    public string Situacao()
    {
        return (this.Media() > 6 ? "Aprovado" : "Reprovado");
    }

    public string NotasFormatada()
    {
        return string.Join(",", this.Notas);
    }

    private static List<Aluno> alunos = new List<Aluno>();
    
    public static List<Aluno> Todos()
    {
        if (File.Exists(Aluno.caminhoJson()))
        {
            var conteudo = File.ReadAllText(Aluno.caminhoJson());
            Aluno.alunos = JsonConvert.DeserializeObject<List<Aluno>>(conteudo);
        }
        return Aluno.alunos;
    }

    private static string caminhoJson()
    {
        return ConfigurationManager.AppSettings["caminho_json"];
    }

    public static void Adicionar(Aluno aluno)
    {
        Aluno.alunos = Aluno.Todos();
        Aluno.alunos.Add(aluno);
        File.WriteAllText(Aluno.caminhoJson(), JsonConvert.SerializeObject(Aluno.alunos));
    }
}