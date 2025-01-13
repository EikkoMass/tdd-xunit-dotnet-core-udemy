using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using CursoOnline.DominioTest.PublicosAlvo;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Alunos;

public class ArmazenadorDeAlunoTest
{
    private readonly AlunoDto _alunoDto;
    private readonly Mock<IAlunoRepositorio> _alunoRepositorioMock;
    private readonly ArmazenadorDeAluno _armazenador;
    private readonly Mock<IConversorDePublicoAlvo> _conversorDePublicoAlvo;
    
    public ArmazenadorDeAlunoTest()
    {
        var faker = new Faker();
        _alunoDto = new AlunoDto()
        {
            Nome = faker.Person.FirstName,
            Cpf = faker.Person.Cpf(),
            Email = faker.Person.Email,
            PublicoAlvo = "Estudante",
        };
        
        _alunoRepositorioMock = new Mock<IAlunoRepositorio>();
        _conversorDePublicoAlvo = new Mock<IConversorDePublicoAlvo>();
        _armazenador = new ArmazenadorDeAluno(_alunoRepositorioMock.Object, _conversorDePublicoAlvo.Object);
    }
    
    [Fact]
    public void DeveAdicionarAluno()
    {
        _armazenador.Armazenar(_alunoDto);
        
        _alunoRepositorioMock.Verify(r => 
            r.Adicionar(
                It.Is<Aluno>(c=> 
                    c.Nome == _alunoDto.Nome 
                        && c.Cpf == _alunoDto.Cpf
                )
            ),
            Times.AtLeastOnce
        );
    }

    [Fact]
    public void DeveAlterarDadosDoAluno()
    {
        _alunoDto.Id = 323;
        var aluno = AlunoBuilder.Novo().Build();

        _alunoRepositorioMock.Setup(r => r.ObterPorId(_alunoDto.Id)).Returns(aluno);
        
        _armazenador.Armazenar(_alunoDto);
        
        Assert.Equal(_alunoDto.Nome, aluno.Nome);
    }

    [Fact]
    public void NaoDeveAdicionarNoRepositorioQuandoAlunoJaExiste()
    {
        _alunoDto.Id = 323;
        var aluno = AlunoBuilder.Novo().Build();

        _alunoRepositorioMock.Setup(r => r.ObterPorId(_alunoDto.Id)).Returns(aluno);
        
        _armazenador.Armazenar(_alunoDto);
        
        _alunoRepositorioMock.Verify(r => r.Adicionar(It.IsAny<Aluno>()), Times.Never());
    }

    [Fact]
    public void NaoDeveAdicionarAlunoComMesmoCpfDeOutroJaSalvo()
    {
        var alunoJaSalvo = AlunoBuilder.Novo().ComId(432).ComCpf(_alunoDto.Cpf).Build();

        _alunoRepositorioMock.Setup(r => r.ObterPorCpf(_alunoDto.Cpf)).Returns(alunoJaSalvo);
        
        Assert.Throws<ExcecaoDeDominio>(() => _armazenador.Armazenar(_alunoDto))
            .ComMensagem(Resource.CpfJaCadastrado);
    }
}