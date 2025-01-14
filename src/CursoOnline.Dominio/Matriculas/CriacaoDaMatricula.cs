using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;

namespace CursoOnline.Dominio.Matriculas;

public class CriacaoDaMatricula
{
    private readonly ICursoRepositorio _cursoRepositorio;
    private readonly IAlunoRepositorio _alunoRepositorio;
    private readonly IMatriculaRepositorio _matriculaRepositorio;
    
    public CriacaoDaMatricula(IAlunoRepositorio alunoRepositorio, ICursoRepositorio cursoRepositorio, IMatriculaRepositorio matriculaRepositorio)
    {
        _cursoRepositorio = cursoRepositorio;
        _alunoRepositorio = alunoRepositorio;
        _matriculaRepositorio = matriculaRepositorio;
    }

    public void Criar(MatriculaDto matriculaDto)
    {
        var curso = _cursoRepositorio.ObterPorId(matriculaDto.CursoId);
        var aluno = _alunoRepositorio.ObterPorId(matriculaDto.AlunoId);

        ValidadorDeRegra.Novo()
            .Quando(curso == null, Resource.CursoNaoEncontrado)
            .Quando(aluno == null, Resource.AlunoNaoEncontrado)
            .DispararExcecaoSeExistir();

        var matricula = new Matricula(aluno, curso, matriculaDto.ValorPago);
        
        _matriculaRepositorio.Adicionar(matricula);
    }
}