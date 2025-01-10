using Bogus;
using CursoOnline.Dominio.Cursos;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Cursos;

public class ArmazenadorDeCursoTest
{
    private readonly CursoDTO _cursoDto;
    private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;
    private readonly ArmazenadorDeCurso _armazenador;
    
    public ArmazenadorDeCursoTest()
    {
        var faker = new Faker();
        _cursoDto = new CursoDTO
        {
            Nome = faker.Random.Words(),
            Descricao = faker.Lorem.Paragraphs(),
            CargaHoraria = faker.Random.Double(50, 1000),
            PublicoAlvoId = 1,
            Valor = faker.Random.Double(1000, 2000),
        };
        
        _cursoRepositorioMock = new Mock<ICursoRepositorio>();
        _armazenador = new ArmazenadorDeCurso(_cursoRepositorioMock.Object);
    }
    
    [Fact]
    public void DeveAdicionarCurso()
    {
        _armazenador.Armazenar(_cursoDto);
        
        _cursoRepositorioMock.Verify(r => 
            r.Adicionar(
                It.Is<Curso>(c=> 
                    c.Nome == _cursoDto.Nome 
                        && c.Descricao == _cursoDto.Descricao
                )
            ),
            Times.AtLeastOnce
        );
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
    public double CargaHoraria { get; set; }
    public int PublicoAlvoId { get; set; }
    public double Valor { get; set; }
}