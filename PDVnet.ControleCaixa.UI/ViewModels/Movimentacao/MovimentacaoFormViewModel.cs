using PDVnet.ControleCaixa.UI.Resources;

namespace PDVnet.ControleCaixa.UI.ViewModels.Movimentacao;

public class MovimentacaoFormViewModel : BaseViewModel
{
    public event Action<bool>? FecharJanela;

    public int Id { get; set; }

    private string _descricao = "";
    public string Descricao
    {
        get => _descricao;
        set
        {
            var texto = new string(value
                .Where(c =>
                    char.IsLetterOrDigit(c) ||
                    char.IsWhiteSpace(c) ||
                    char.IsPunctuation(c))
                .ToArray());

            if (_descricao != texto)
            {
                _descricao = texto;
                OnPropertyChanged();
            }
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

    protected void SolicitarFechamento(bool resultado = true)
    {
        FecharJanela?.Invoke(resultado);
    }

    protected void FecharComResultado(bool resultado)
    {
        FecharJanela?.Invoke(resultado);
    }
}