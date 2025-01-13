namespace CursoOnline.Dominio._Base;

public class ValidadorDeRegra
{
    private List<string> _mensagensDeErro;
    private ValidadorDeRegra()
    {
        _mensagensDeErro = new List<string>();
    }

    public static ValidadorDeRegra Novo()
    {
        return new ValidadorDeRegra();
    }

    public ValidadorDeRegra Quando(bool temErro, string mensagem)
    {
        if (temErro)
        {
            _mensagensDeErro.Add(mensagem);
        }
        
        return this;
    }

    public void DispararExcecaoSeExistir()
    {
        if (_mensagensDeErro.Any())
        {
            throw new ExcecaoDeDominio(_mensagensDeErro);
        }
    }
}

public class ExcecaoDeDominio : ArgumentException
{
    public List<string> MensagensDeErro { get; set; }

    public ExcecaoDeDominio(List<string> mensagens)
    {
        MensagensDeErro = mensagens;
    }
}