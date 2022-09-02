// See https://aka.ms/new-console-template for more information

using System.Configuration;
using System.Data.SqlClient;
using Newtonsoft.Json;

public class Aluno
{
    public int Id { get; set; }
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

    public static List<Aluno> TodosJson()
    {
        if (File.Exists(Aluno.caminhoJson()))
        {
            var conteudo = File.ReadAllText(Aluno.caminhoJson());
            Aluno.alunos = JsonConvert.DeserializeObject<List<Aluno>>(conteudo);
        }
        return Aluno.alunos;
    }

    public static List<Aluno> TodosSql()
    {
        Aluno.alunos = new List<Aluno>();
        using (var cnn = new SqlConnection(Aluno.stringConexaoSql()))
        {
            cnn.Open();
            using (var cmd = new SqlCommand("select * from alunos", cnn))
            {
                using(SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var aluno = new Aluno();
                        aluno.Id = Convert.ToInt32(dr["id"]);
                        aluno.Nome = dr["nome"].ToString();
                        aluno.Matricula = dr["matricula"].ToString();
                        
                        Aluno.alunos.Add(aluno);

                        
                    }
                }
                foreach (var aluno in Aluno.alunos)
                {
                using (var cmdNotas = new SqlCommand("select * from notas where aluno_id = " + aluno.Id, cnn))
                {
                    using (SqlDataReader drNotas = cmdNotas.ExecuteReader())
                    {
                        aluno.Notas = new List<double>();
                        while (drNotas.Read())
                        {
                            aluno.Notas.Add(Convert.ToDouble(drNotas["nota"]));
                        }
                    }
                }
                }
            }
            cnn.Close();
        }

        return Aluno.alunos;
    }

    private static string stringConexaoSql()
    {
        return ConfigurationManager.AppSettings["conexao_sql"];
    }

    private static string caminhoJson()
    {
        return ConfigurationManager.AppSettings["caminho_json"];
    }

    public static void AdicionarJson(Aluno aluno)
    {
        Aluno.alunos = Aluno.TodosJson();
        Aluno.alunos.Add(aluno);
        File.WriteAllText(Aluno.caminhoJson(), JsonConvert.SerializeObject(Aluno.alunos));
    }

    public static void AdicionarSql(Aluno aluno)
    {
        using (var cnn = new SqlConnection(Aluno.stringConexaoSql()))
        {
            cnn.Open();
            var cmd = new SqlCommand("insert into alunos(nome, matricula) values(@nome, @matricula); select @@identity", cnn);
            cmd.Parameters.AddWithValue("@nome", aluno.Nome);
            cmd.Parameters.AddWithValue("@matricula", aluno.Matricula);
            int aluno_id = Convert.ToInt32(cmd.ExecuteScalar());

            foreach (var nota in aluno.Notas)
            {
                var cmdNota = new SqlCommand("insert into notas(aluno_id, nota) values(@aluno_id, @nota)", cnn);
                cmdNota.Parameters.AddWithValue("@aluno_id", aluno_id);
                cmdNota.Parameters.AddWithValue("@nota", nota);
                cmdNota.ExecuteNonQuery();
            }
            cnn.Close();
        }
    }
}