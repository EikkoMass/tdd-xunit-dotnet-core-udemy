﻿using CursoOnline.Dados.Contextos;
using CursoOnline.Dominio.Alunos;

namespace CursoOnline.Dados.Repositorios;

public class AlunoRepositorio  : RepositorioBase<Aluno>, IAlunoRepositorio
{
    public AlunoRepositorio(ApplicationDbContext context) : base(context)
    {
    }
        
    public Aluno ObterPorId(int id)
    {
        var entidade = Context.Set<Aluno>().Where(c => c.Id == id);
        if (entidade.Any())
            return entidade.First();
        return null;
    }
    
    public Aluno ObterPorCpf(string cpf)
    {
        var entidade = Context.Set<Aluno>().Where(c => c.Cpf == cpf);
        if (entidade.Any())
            return entidade.First();
        return null;
    }
}