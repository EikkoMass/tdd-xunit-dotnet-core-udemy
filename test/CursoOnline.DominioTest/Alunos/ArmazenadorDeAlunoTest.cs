using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Alunos;

public class ArmazenadorDeAlunoTest
{
    private readonly AlunoDto _alunoDto;
    private readonly Mock<IAlunoRepositorio> _alunoRepositorioMock;
    private readonly ArmazenadorDeAluno _armazenador;
    
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
        _armazenador = new ArmazenadorDeAluno(_alunoRepositorioMock.Object);
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
    public void NaoDeveInformarPublicoAlvoInvalido()
    {
        _alunoDto.PublicoAlvo = "Medico";
        
        Assert.Throws<ExcecaoDeDominio>(() => _armazenador.Armazenar(_alunoDto))
            .ComMensagem(Resource.PublicoAlvoInvalido);
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

}