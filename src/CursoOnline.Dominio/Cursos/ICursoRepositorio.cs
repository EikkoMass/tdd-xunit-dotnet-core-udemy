namespace CursoOnline.Dominio.Cursos;

public interface ICursoRepositorio
{
    void Adicionar(Curso curso);
    Curso ObterPeloNome(string nome);
    Curso? ObterPorId(int id);

}