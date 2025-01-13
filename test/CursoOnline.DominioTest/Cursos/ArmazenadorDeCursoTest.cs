using Bogus;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Cursos;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using CursoOnline.DominioTest.PublicosAlvo;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Cursos;

public class ArmazenadorDeCursoTest
{
    private readonly CursoDto _cursoDto;
    private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;
    private readonly Mock<IConversorDePublicoAlvo> _conversorDePublicoAlvo;
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
        _conversorDePublicoAlvo = new Mock<IConversorDePublicoAlvo>();
        _armazenador = new ArmazenadorDeCurso(_cursoRepositorioMock.Object, _conversorDePublicoAlvo.Object);
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
    public void NaoDeveAdicionarCursoComMesmoNomeDeOutroJaSalvo()
    {
        var cursoJaSalvo = CursoBuilder.Novo().ComId(432).ComNome(_cursoDto.Nome).Build();

        _cursoRepositorioMock.Setup(r => r.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);
        
        Assert.Throws<ExcecaoDeDominio>(() => _armazenador.Armazenar(_cursoDto))
            .ComMensagem(Resource.NomeDoCursoJaExiste);
    }

    [Fact]
    public void DeveAlterarDadosDoCurso()
    {
        _cursoDto.Id = 323;
        var curso = CursoBuilder.Novo().Build();

        _cursoRepositorioMock.Setup(r => r.ObterPorId(_cursoDto.Id)).Returns(curso);
        
        _armazenador.Armazenar(_cursoDto);
        
        Assert.Equal(_cursoDto.Nome, curso.Nome);
        Assert.Equal(_cursoDto.Valor, curso.Valor);
        Assert.Equal(_cursoDto.CargaHoraria, curso.CargaHoraria);
    }

    [Fact]
    public void NaoDeveAdicionarNoRepositorioQuandoCursoJaExiste()
    {
        _cursoDto.Id = 323;
        var curso = CursoBuilder.Novo().Build();

        _cursoRepositorioMock.Setup(r => r.ObterPorId(_cursoDto.Id)).Returns(curso);
        
        _armazenador.Armazenar(_cursoDto);
        
        _cursoRepositorioMock.Verify(r => r.Adicionar(It.IsAny<Curso>()), Times.Never());
    }
}