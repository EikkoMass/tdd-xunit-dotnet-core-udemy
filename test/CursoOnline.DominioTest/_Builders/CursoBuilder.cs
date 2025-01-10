using CursoOnline.Dominio.Cursos;

namespace CursoOnline.DominioTest._Builders;

public class CursoBuilder
{
    private string _nome = "Informatica Basica";
    private string _descricao = "Uma descricao";
    private double _cargaHoraria = 80;
    private double _valor = 950;
    private PublicoAlvo _publicoAlvo = PublicoAlvo.Estudante;
    
    public static CursoBuilder Novo()
    {
        return new CursoBuilder();
    }

    public CursoBuilder ComNome(string nome)
    {
        _nome = nome;
        return this;
    }
    
    public CursoBuilder ComDescricao(string descricao)
    {
        _descricao = descricao;
        return this;
    }
    
    public CursoBuilder ComCargaHoraria(double cargaHoraria)
    {
        _cargaHoraria = cargaHoraria;
        return this;
    }
    
    public CursoBuilder ComValor(double valor)
    {
        _valor = valor;
        return this;
    }
    
    public CursoBuilder ComPublicoAlvo(PublicoAlvo publicoAlvo)
    {
        _publicoAlvo = publicoAlvo;
        return this;
    }
    
    public Curso Build()
    {
        return new Curso(_nome, _descricao, _cargaHoraria, _publicoAlvo, _valor);
    }
}