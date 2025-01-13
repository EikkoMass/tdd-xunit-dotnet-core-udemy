namespace CursoOnline.Dominio.Alunos;

public interface IAlunoRepositorio
{
    void Adicionar(Aluno curso);
    Aluno ObterPorId(int id);
}