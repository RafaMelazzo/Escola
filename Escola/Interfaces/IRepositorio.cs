using Escola.Entidades;

namespace Escola.Interfaces
{
    public interface IRepositorio
    {
        int Quantidade();
        List<Aluno> Todos();
        void Salvar(Aluno aluno);
    }
}
