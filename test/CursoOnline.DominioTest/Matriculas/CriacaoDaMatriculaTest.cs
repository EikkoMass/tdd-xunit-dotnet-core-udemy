using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Matriculas;

public class CriacaoDaMatriculaTest
{
    private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;
    private readonly Mock<IAlunoRepositorio> _alunoRepositorioMock;
    private readonly Mock<IMatriculaRepositorio> _matriculaRepositorioMock;

    private readonly Aluno _aluno;
    private readonly Curso _curso;
    
    private readonly MatriculaDto _matriculaDto;
    private readonly CriacaoDaMatricula _criacaoDaMatricula;
    
    public CriacaoDaMatriculaTest()
    {
        _cursoRepositorioMock = new Mock<ICursoRepositorio>();
        _alunoRepositorioMock = new Mock<IAlunoRepositorio>();
        _matriculaRepositorioMock = new Mock<IMatriculaRepositorio>();
        
        _aluno = AlunoBuilder
            .Novo()
            .ComPublicoAlvo(PublicoAlvo.Universitario)
            .ComId(23)
            .Build();
        
        _alunoRepositorioMock.Setup(r => r.ObterPorId(_aluno.Id)).Returns(_aluno);
        
        _curso = CursoBuilder
            .Novo()
            .ComPublicoAlvo(PublicoAlvo.Universitario)
            .ComId(45)
            .Build();
        
        _cursoRepositorioMock.Setup(r => r.ObterPorId(_curso.Id)).Returns(_curso);
        
        _matriculaDto = new MatriculaDto { AlunoId = _aluno.Id, CursoId = _curso.Id, ValorPago = _curso.Valor };
        
        _criacaoDaMatricula = new CriacaoDaMatricula(_alunoRepositorioMock.Object, _cursoRepositorioMock.Object, _matriculaRepositorioMock.Object);
    }
    
    [Fact]
    public void DeveNotificarQuandoCursoNaoForEncontrado()
    {
        Curso cursoInvalido = null;
        _cursoRepositorioMock.Setup(r => r.ObterPorId(_matriculaDto.CursoId)).Returns(cursoInvalido);

        Assert.Throws<ExcecaoDeDominio>(() => _criacaoDaMatricula.Criar(_matriculaDto))
            .ComMensagem(Resource.CursoNaoEncontrado);
    }
    
    [Fact]
    public void DeveNotificarQuandoAlunoNaoForEncontrado()
    {
        Aluno alunoInvalido = null;
        _alunoRepositorioMock.Setup(r => r.ObterPorId(_matriculaDto.AlunoId)).Returns(alunoInvalido);

        Assert.Throws<ExcecaoDeDominio>(() => _criacaoDaMatricula.Criar(_matriculaDto))
            .ComMensagem(Resource.AlunoNaoEncontrado);
    }

    [Fact]
    public void DeveAdicionarMatricula()
    {
        _criacaoDaMatricula.Criar(_matriculaDto);
        
        _matriculaRepositorioMock.Verify(r => r.Adicionar(It.Is<Matricula>(m => m.Aluno == _aluno && m.Curso == _curso)));
    }
} 