﻿using Bogus;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using ExpectedObjects;
using Xunit;

namespace CursoOnline.DominioTest.Matriculas;

public class MatriculaTest
{
    [Fact]
    public void DeveCriarMatricula()
    {
        var aluno = AlunoBuilder.Novo().Build();
        var curso = CursoBuilder.Novo().Build();

        var matriculaEsperada = new
        {
            Aluno = aluno,
            Curso = curso,
            ValorPago = curso.Valor
        };
        
        var matricula = new Matricula(matriculaEsperada.Aluno, matriculaEsperada.Curso, matriculaEsperada.ValorPago);
        
        matriculaEsperada.ToExpectedObject().ShouldMatch(matricula);
    }

    [Fact]
    public void NaoDeveCriarMatriculaSemAluno()
    {
        Aluno alunoInvalido = null;
        
        Assert.Throws<ExcecaoDeDominio>(() => 
            MatriculaBuilder.Novo().ComAluno(alunoInvalido).Build()
            ).ComMensagem(Resource.AlunoInvalido);
    }
    
    [Fact]
    public void NaoDeveCriarMatriculaSemCurso()
    {
        Curso cursoInvalido = null;
        
        Assert.Throws<ExcecaoDeDominio>(() => 
            MatriculaBuilder.Novo().ComCurso(cursoInvalido).Build()
        ).ComMensagem(Resource.CursoInvalido);
    }
    
    [Fact]
    public void NaoDeveCriarMatriculaComValorPagoInvalido()
    {
        const double valorPagoInvalido = 0;
        
        Assert.Throws<ExcecaoDeDominio>(() => 
            MatriculaBuilder.Novo().ComValorPago(valorPagoInvalido).Build()
        ).ComMensagem(Resource.ValorPagoInvalido);
    }
    
    [Fact]
    public void NaoDeveCriarMatriculaComValorMaiorQueValorDoCurso()
    {
        Curso curso = CursoBuilder.Novo().ComValor(100).Build();
        double valorPagoMaiorQueCurso = curso.Valor + 1;
        
        Assert.Throws<ExcecaoDeDominio>(() => 
            MatriculaBuilder.Novo().ComCurso(curso).ComValorPago(valorPagoMaiorQueCurso).Build()
        ).ComMensagem(Resource.ValorPagoMaiorQueValorDoCurso);
    }

    [Fact]
    public void DeveIndicarQuehHouveDescontoNaMatricula()
    {
        Curso curso = CursoBuilder.Novo().ComValor(100).Build();
        double valorPagoComDesconto = curso.Valor - 1;

        var matricula = MatriculaBuilder.Novo().ComValorPago(valorPagoComDesconto).Build();
        
        Assert.True(matricula.TemDesconto);
    }
    
    [Fact]
    public void NaoDevePublicoAlvoDeAlunoECursoSeremDiferentes()
    {
        var curso = CursoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Empregado).Build();
        var aluno = AlunoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Universitario).Build();
        
        Assert.Throws<ExcecaoDeDominio>(() => MatriculaBuilder.Novo().ComAluno(aluno).ComCurso(curso).Build())
            .ComMensagem(Resource.PublicosAlvoDiferentes);
    }

    [Fact]
    public void DeveInformarNotaDoAlunoParMatricula()
    {
        const double notaDoAlunoEsperada = 9.5;
        var matricula = MatriculaBuilder.Novo().Build();
        
        matricula.InformarNota(notaDoAlunoEsperada);

        Assert.Equal(notaDoAlunoEsperada, matricula.NotaDoAluno);
    }
    
    [Fact]
    public void DeveIndicarQueCursoFoiConcluido()
    {
        const double notaDoAlunoEsperada = 9.5;
        var matricula = MatriculaBuilder.Novo().Build();
        
        matricula.InformarNota(notaDoAlunoEsperada);

        Assert.True(matricula.CursoConcluido);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(11)]
    public void NaoDeveInformarComNotaInvalida(double notaDoAlunoInvalida)
    {
        var matricula = MatriculaBuilder.Novo().Build();
        
        Assert.Throws<ExcecaoDeDominio>(() => matricula.InformarNota(notaDoAlunoInvalida))
            .ComMensagem(Resource.NotaDoAlunoInvalida);
    }

    [Fact]
    public void DeveCancelarMatricula()
    {
        var matricula = MatriculaBuilder.Novo().Build();
        matricula.Cancelar();
        
        Assert.True(matricula.Cancelada);
    }

    [Fact]
    public void NaoDeveInformarNotaQuandoMatriculaEstiverCancelada()
    {
        double notaDoAluno = 3;
        var matricula = MatriculaBuilder.Novo().ComCancelada(true).Build();
        
        Assert.Throws<ExcecaoDeDominio>(() => matricula.InformarNota(notaDoAluno))
            .ComMensagem(Resource.MatriculaCancelada);
    }
    
    [Fact]
    public void NaoDeveCancelarQuandoMatriculaEstiverConcluida()
    {
        var matricula = MatriculaBuilder.Novo().ComConcluido(true).Build();
        
        Assert.Throws<ExcecaoDeDominio>(() => matricula.Cancelar())
            .ComMensagem(Resource.MatriculaConcluida);
    }
}