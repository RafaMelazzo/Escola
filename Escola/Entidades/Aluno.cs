using Escola.Enum;

namespace Escola.Entidades
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public List<double> Notas { get; set; }
        public OndeSalvar OndeSalvar { get; internal set; }

        public double Media()
        {
            double mediaNotas = 0;
            foreach (var nota in Notas) mediaNotas += nota;
            mediaNotas = mediaNotas / Notas.Count;

            return mediaNotas;
        }

        public string Situacao()
        {
            return Media() > 6 ? "Aprovado" : "Reprovado";
        }

        public string NotasFormatada()
        {
            return string.Join(",", Notas);
        }
    }
}