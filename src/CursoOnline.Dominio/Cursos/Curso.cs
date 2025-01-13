using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.Cursos;

public class Curso : Entidade
{
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public double CargaHoraria { get; private set; }
    public PublicoAlvo PublicoAlvo { get; private set; }
    public double Valor { get; private set; }

    private Curso() { }
    
    public Curso(string nome, string descricao, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
    {
        ValidadorDeRegra.Novo()
            .Quando(string.IsNullOrEmpty(nome), "Nome Invalido")
            .Quando(cargaHoraria < 1, "Carga Horaria Invalida")
            .Quando(valor < 1, "Valor Invalido")
            .DispararExcecaoSeExistir();
        
        Nome = nome;
        Descricao = descricao;
        CargaHoraria = cargaHoraria;
        PublicoAlvo = publicoAlvo;
        Valor = valor;
    }

    public void AlterarNome(string nome)
    {
        ValidadorDeRegra.Novo()
            .Quando(string.IsNullOrEmpty(nome), "Nome Invalido")
            .DispararExcecaoSeExistir();
        
        Nome = nome;
    }

    public void AlterarCargaHoraria(double cargaHoraria)
    {
        ValidadorDeRegra.Novo()
            .Quando(cargaHoraria < 1, "Carga Horaria Invalida")
            .DispararExcecaoSeExistir();
        
        CargaHoraria = cargaHoraria;
    }

    public void AlterarValor(double valor)
    {
        ValidadorDeRegra.Novo()
            .Quando(valor < 1, "Valor Invalido")
            .DispararExcecaoSeExistir();
        
        Valor = valor;
    }
}