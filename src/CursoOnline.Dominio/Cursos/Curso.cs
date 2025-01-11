using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.Cursos;

public class Curso : Entidade
{
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public double CargaHoraria { get; private set; }
    public PublicoAlvo PublicoAlvo { get; private set; }
    public double Valor { get; private set; }

    public Curso(string nome, string descricao, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
    {
        if (string.IsNullOrEmpty(nome))
        {
            throw new ArgumentException("Nome Invalido");
        }
        
        if (cargaHoraria < 1)
        {
            throw new ArgumentException("Carga Horaria Invalida");
        }
        
        if (valor < 1)
        {
            throw new ArgumentException("Valor Invalido");
        }
        
        Nome = nome;
        Descricao = descricao;
        CargaHoraria = cargaHoraria;
        PublicoAlvo = publicoAlvo;
        Valor = valor;
    }
}