using PDVnet.ControleCaixa.UI.Resources;

namespace PDVnet.ControleCaixa.UI.ViewModels.Movimentacao;

public class MovimentacaoBaseViewModel : BaseViewModel
{
    public event Action<bool>? FecharJanela;

    public int Id { get; set; }

    private string _descricao = "";
    public string Descricao
    {
        get => _descricao;
        set
        {
            _descricao = value;
            OnPropertyChanged();
        }
    }

    private string _categoria = "";
    public string Categoria
    {
        get => _categoria;
        set
        {
            _categoria = value;
            OnPropertyChanged();
        }
    }

    public List<string> Categorias => MovimentacaoOptions.Categorias;

    private decimal _valor;
    public decimal Valor
    {
        get => _valor;
        set
        {
            _valor = value;
            OnPropertyChanged();
        }
    }

    private string _tipo = "Entrada";
    public string Tipo
    {
        get => _tipo;
        set
        {
            _tipo = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsEntrada));
            OnPropertyChanged(nameof(IsSaida));
        }
    }

    public bool IsEntrada
    {
        get => Tipo == "Entrada";
        set
        {
            if (value)
                Tipo = "Entrada";
        }
    }

    public bool IsSaida
    {
        get => Tipo == "Saida";
        set
        {
            if (value)
                Tipo = "Saida";
        }
    }

    public List<string> Tipos => MovimentacaoOptions.Tipos;

    private bool _status = true;
    public bool Status
    {
        get => _status;
        set
        {
            _status = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsTrue));
            OnPropertyChanged(nameof(IsFalse));
        }
    }

    public bool IsTrue
    {
        get => Status;
        set
        {
            if (value)
                Status = true;
        }
    }

    public bool IsFalse
    {
        get => !Status;
        set
        {
            if (value)
                Status = false;
        }
    }

    protected void SolicitarFechamento(bool resultado = true)
    {
        FecharJanela?.Invoke(resultado);
    }

    protected void FecharComResultado(bool resultado)
    {
        FecharJanela?.Invoke(resultado);
    }
}