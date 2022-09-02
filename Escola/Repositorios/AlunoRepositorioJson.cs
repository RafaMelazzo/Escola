using Escola.Entidades;
using Escola.Interfaces;
using Newtonsoft.Json;
using System.Configuration;

namespace Escola.Repositorios
{
    public class AlunoRepositorioJson : IRepositorio
    {
        private string caminhoJson()
        {
            return ConfigurationManager.AppSettings["caminho_json"];
        }

        public int Quantidade()
        {
            return this.Todos().Count();
        }

        public List<Aluno> Todos()
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
            var alunos = Todos();
            alunos.Add(aluno);
            File.WriteAllText(caminhoJson(), JsonConvert.SerializeObject(alunos));
        }
    }
}
