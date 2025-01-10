using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Cursos;

public class ArmazenadorDeCursoTest
{
    private readonly CursoDto _cursoDto;
    private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;
    private readonly ArmazenadorDeCurso _armazenador;
    
    public ArmazenadorDeCursoTest()
    {
        var faker = new Faker();
        _cursoDto = new CursoDto()
        {
            Nome = faker.Random.Words(),
            Descricao = faker.Lorem.Paragraphs(),
            CargaHoraria = faker.Random.Double(50, 1000),
            PublicoAlvo = "Estudante",
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

    [Fact]
    public void NaoDeveInformarPublicoAlvoInvalido()
    {
        _cursoDto.PublicoAlvo = "Medico";
        
        Assert.Throws<ArgumentException>(() => _armazenador.Armazenar(_cursoDto))
            .ComMensagem("Publico Alvo invalido");
    }

    [Fact]
    public void NaoDeveAdicionarCursoComMesmoNomeDeOutroJaSalvo()
    {
        var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDto.Nome).Build();

        _cursoRepositorioMock.Setup(r => r.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);
        
        Assert.Throws<ArgumentException>(() => _armazenador.Armazenar(_cursoDto))
            .ComMensagem("Nome do curso ja consta no banco de dados");
    }
}