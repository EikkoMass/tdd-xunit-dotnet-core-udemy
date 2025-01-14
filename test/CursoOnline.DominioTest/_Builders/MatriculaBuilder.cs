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
    protected bool _cancelada;
    protected bool _concluido;
    
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
    
    public MatriculaBuilder ComCancelada(bool cancelada)
    {
        _cancelada = cancelada;
        return this;
    }
    
    public MatriculaBuilder ComConcluido(bool concluido)
    {
        _concluido = concluido;
        return this;
    }
    
    public Matricula Build()
    {
      var matricula = new Matricula(_aluno, _curso, _valorPago);

      if (_cancelada)
      {
          matricula.Cancelar();
      }
      
      if (_concluido)
      {
          double nota = 7;
          matricula.InformarNota(nota);
      }
      
      return matricula;
    }
}