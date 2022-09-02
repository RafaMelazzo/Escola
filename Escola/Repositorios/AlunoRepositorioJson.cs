using Escola.Entidades;
using Newtonsoft.Json;
using System.Configuration;

namespace Escola.Repositorios
{
    public class AlunoRepositorioJson
    {
        private string caminhoJson()
        {
            return ConfigurationManager.AppSettings["caminho_json"];
        }

        public List<Aluno> TodosJson()
        {
            var alunos = new List<Aluno>();
            if (File.Exists(caminhoJson()))
            {
                var conteudo = File.ReadAllText(caminhoJson());
                alunos = JsonConvert.DeserializeObject<List<Aluno>>(conteudo);
            }
            return alunos;
        }

        public void Salvar(Aluno aluno)
        {
            var alunos = TodosJson();
            alunos.Add(aluno);
            File.WriteAllText(caminhoJson(), JsonConvert.SerializeObject(alunos));
        }
    }
}
