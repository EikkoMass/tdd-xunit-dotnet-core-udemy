using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Enums;

namespace CursoOnline.Dominio.Alunos;

public class ArmazenadorDeAluno
{
    private readonly IAlunoRepositorio _alunoRepositorio;
    
    public ArmazenadorDeAluno(IAlunoRepositorio alunoRepositorio)
    {
        _alunoRepositorio = alunoRepositorio;
    }
    
    public void Armazenar(AlunoDto alunoDto)
    {
        var mesmoAluno = _alunoRepositorio.ObterPorCpf(alunoDto.Cpf);
        
        ValidadorDeRegra.Novo()
            .Quando(mesmoAluno != null && alunoDto.Id != mesmoAluno.Id, Resource.CpfJaCadastrado)
            .Quando(!Enum.TryParse<PublicoAlvo>(alunoDto.PublicoAlvo, out var publicoAlvo), Resource.PublicoAlvoInvalido)
            .DispararExcecaoSeExistir();

        var aluno = new Aluno(alunoDto.Nome, alunoDto.Email, alunoDto.Cpf, (PublicoAlvo) publicoAlvo);

        if (alunoDto.Id > 0)
        {
            aluno = _alunoRepositorio.ObterPorId(alunoDto.Id);
            aluno.AlterarNome(alunoDto.Nome);
        }

        if (alunoDto.Id == 0)
        {
            _alunoRepositorio.Adicionar(aluno);
        }
    }
}