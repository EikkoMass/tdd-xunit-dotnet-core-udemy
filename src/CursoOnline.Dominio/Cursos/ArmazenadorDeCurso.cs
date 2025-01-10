namespace CursoOnline.Dominio.Cursos;

public class ArmazenadorDeCurso
{
    private readonly ICursoRepositorio _cursoRepositorio;
    
    public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorio)
    {
        _cursoRepositorio = cursoRepositorio;
    }
    
    public void Armazenar(CursoDto cursoDto)
    {
        var cursoJaSalvo = _cursoRepositorio.ObterPeloNome(cursoDto.Nome);

        if (cursoJaSalvo != null)
        {
            throw new ArgumentException("Nome do curso ja consta no banco de dados");
        }

        if (!Enum.TryParse<PublicoAlvo>(cursoDto.PublicoAlvo, out var publicoAlvo))
        {
            throw new ArgumentException("Publico Alvo invalido");
        }
        
        var curso = new Curso(cursoDto.Nome, cursoDto.Descricao, cursoDto.CargaHoraria, (PublicoAlvo) publicoAlvo, cursoDto.Valor);
        
        _cursoRepositorio.Adicionar(curso);
    }
}