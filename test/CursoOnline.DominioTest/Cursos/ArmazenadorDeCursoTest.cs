using CursoOnline.Dominio.Cursos;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Cursos;

public class ArmazenadorDeCursoTest
{
    [Fact]
    public void DeveAdicionarCurso()
    {
        var cursoDto = new CursoDTO
        {
            Nome = "Curso A",
            Descricao = "Descricao",
            CargaHoraria = 80,
            PublicoAlvoId = 1,
            Valor = 850.00,
        };

        var cursoRepositorioMock = new Mock<ICursoRepositorio>();

        var armazenador = new ArmazenadorDeCurso(cursoRepositorioMock.Object);

        armazenador.Armazenar(cursoDto);
        
        cursoRepositorioMock.Verify(r => r.Adicionar(It.IsAny<Curso>()));
    }
}

public class ArmazenadorDeCurso
{
    private readonly ICursoRepositorio _cursoRepositorio;
    
    public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorio)
    {
        _cursoRepositorio = cursoRepositorio;
    }
    
    public void Armazenar(CursoDTO cursoDto)
    {
        var curso = new Curso(cursoDto.Nome, cursoDto.Descricao, cursoDto.CargaHoraria, PublicoAlvo.Estudante, cursoDto.Valor);
        
        _cursoRepositorio.Adicionar(curso);
    }
}

public interface ICursoRepositorio
{
    void Adicionar(Curso curso);
}

public class CursoDTO
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public int CargaHoraria { get; set; }
    public int PublicoAlvoId { get; set; }
    public double Valor { get; set; }
}