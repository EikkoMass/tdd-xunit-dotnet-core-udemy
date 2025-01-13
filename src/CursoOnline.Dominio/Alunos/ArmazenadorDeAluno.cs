using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Enums;
using CursoOnline.DominioTest.PublicosAlvo;

namespace CursoOnline.Dominio.Alunos;

public class ArmazenadorDeAluno
{
    private readonly IAlunoRepositorio _alunoRepositorio;
    private readonly IConversorDePublicoAlvo _conversorDePublicoAlvo;
    
    public ArmazenadorDeAluno(IAlunoRepositorio alunoRepositorio, IConversorDePublicoAlvo conversorDePublicoAlvo)
    {
        _alunoRepositorio = alunoRepositorio;
        _conversorDePublicoAlvo = conversorDePublicoAlvo;
    }
    
    public void Armazenar(AlunoDto alunoDto)
    {
        var mesmoAluno = _alunoRepositorio.ObterPorCpf(alunoDto.Cpf);
        
        ValidadorDeRegra.Novo()
            .Quando(mesmoAluno != null && alunoDto.Id != mesmoAluno.Id, Resource.CpfJaCadastrado)
            .DispararExcecaoSeExistir();

        var publicoAlvo = _conversorDePublicoAlvo.Converter(alunoDto.PublicoAlvo);
        
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