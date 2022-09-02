using Escola.Entidades;
using Escola.Enum;

namespace Escola.Repositorios
{
    public class AlunoRepositorio
    {
        public void Salvar(Aluno aluno)
        {
            if (aluno.OndeSalvar == OndeSalvar.Arquivo)
            {
                new AlunoRepositorioJson().Salvar(aluno);
            }
            else //if (aluno.OndeSalvar == OndeSalvar.Sql)
            {
                new AlunoRepositorioSql().Salvar(aluno);
            }
            //else
            //{
            //    throw new Exception("Tipo inexistente!");
            //}
        }
    }
}
