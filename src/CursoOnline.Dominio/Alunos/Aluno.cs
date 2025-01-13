using System.Text.RegularExpressions;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Enums;

namespace CursoOnline.Dominio.Alunos;

public class Aluno : Entidade
{
    
    private readonly Regex _cpfRegex = new Regex(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$");
    private readonly Regex _emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
    public string Nome { get; private set; } 
    public string Cpf { get; private set; } 
    public string Email { get; private set; } 
    public PublicoAlvo PublicoAlvo { get; private set; }
    
    private Aluno() {}

    public Aluno(string nome, string email, string cpf, PublicoAlvo publicoAlvo)
    {
        ValidadorDeRegra.Novo()
            .Quando(string.IsNullOrEmpty(nome), Resource.NomeInvalido)
            .Quando(string.IsNullOrEmpty(cpf) || !_cpfRegex.IsMatch(cpf), Resource.CpfInvalido)
            .Quando(string.IsNullOrEmpty(email) || !_emailRegex.IsMatch(email), Resource.EmailInvalido)
            .DispararExcecaoSeExistir();
        
        Nome = nome;
        Email = email;
        Cpf = cpf;
        PublicoAlvo = publicoAlvo;
    }

    public Aluno AlterarNome(string nome)
    {
        ValidadorDeRegra.Novo()
            .Quando(string.IsNullOrEmpty(nome), Resource.NomeInvalido)
            .DispararExcecaoSeExistir();
        
        Nome = nome;
        return this;
    }
    
}