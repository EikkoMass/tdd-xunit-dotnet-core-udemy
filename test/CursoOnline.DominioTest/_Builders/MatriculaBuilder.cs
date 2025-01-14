using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.DominioTest.Matriculas;

namespace CursoOnline.DominioTest._Builders;

public class MatriculaBuilder
{
    protected Aluno _aluno;
    protected Curso _curso;
    protected double _valorPago;
    
    public static MatriculaBuilder Novo()
    {
        var curso = CursoBuilder.Novo().Build();
        
        return new MatriculaBuilder
        {
            _aluno = AlunoBuilder.Novo().Build(),
            _curso = curso,
            _valorPago = curso.Valor
        };
    }

    public MatriculaBuilder ComAluno(Aluno aluno)
    {
        _aluno = aluno;
        return this;
    }
    
    public MatriculaBuilder ComCurso(Curso curso)
    {
        _curso = curso;
        return this;
    }
    
    public MatriculaBuilder ComValorPago(double valorPago)
    {
        _valorPago = valorPago;
        return this;
    }
    
    public Matricula Build()
    {
      var matricula = new Matricula(_aluno, _curso, _valorPago);
      return matricula;
    }
}