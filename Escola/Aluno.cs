// See https://aka.ms/new-console-template for more information
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
}