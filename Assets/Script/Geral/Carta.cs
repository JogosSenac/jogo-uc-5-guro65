using UnityEngine;
using TMPro;

public class Carta : MonoBehaviour
{
    public string carta; // Nome da carta
    public string nomeEvolucao; // Nome da carta após a evolução
    public GameObject cartaEvolutivaPrefab; // Prefab da carta evolutiva
    public int dano;
    public int defesa;
    public bool ativa;
    public Vector3 scalaInicial;
    public bool clicada = false;
    public SpriteRenderer sprite;
    [SerializeField] private TextMeshProUGUI statusDefesa;
    [SerializeField] private TextMeshProUGUI statusDano;

    private Token tokenEvolucao; // Token que pode evoluir esta carta

    private void Awake() 
    {
        ativa = true; // A carta está ativa por padrão
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        scalaInicial = transform.localScale; // Armazena a escala inicial
        statusDefesa = GameObject.FindWithTag("StatusDefesa").GetComponent<TextMeshProUGUI>();
        statusDano = GameObject.FindWithTag("StatusDano").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        // Implementar lógica adicional aqui, se necessário
    }

    public void AtivaCarta()
    {
        ativa = true;
    }

    public void DesativaCarta()
    {
        ativa = false;
    }

    private void OnMouseOver() 
    {
        if (ativa)
        {
            transform.localScale = new Vector3(0.15f, 0.15f, 1); // Aumenta a carta ao passar o mouse
            sprite.sortingOrder = 2; // Mantém a carta na frente
            statusDefesa.text = "Defesa: " + defesa; // Mostra status
            statusDano.text = "Dano: " + dano;
        }
    }

    private void OnMouseExit() 
    {
        if (ativa && !clicada) 
        {
            transform.localScale = scalaInicial; // Restaura escala ao sair
            sprite.sortingOrder = 2; // Ordenação padrão ao sair
            statusDefesa.text = ""; // Limpa status
            statusDano.text = "";
        }
    }

    private void OnMouseDown()
    {
        if (ativa)
        {
            clicada = !clicada;
            transform.localScale = clicada ? new Vector3(0.15f, 0.15f, 1) : scalaInicial;

            // Obtenha o token ativo (se houver)
            Token token = FindObjectOfType<Token>();
            if (clicada && token != null && token.ativo) // Verifica se o token está ativo
            {
                token.SetCartaSelecionada(this); // Se clicado, define esta carta como selecionada
                Debug.Log($"Carta {NomeCarta()} selecionada pelo token {token.NomeToken()}");
            }
            else if (!clicada && token != null)
            {
                token.ResetToken();
            }
        }
    }

    public void SetTokenEvolucao(Token token)
    {
        tokenEvolucao = token; // Define o token que pode evoluir esta carta
    }

    public Token GetTokenEvolucao()
    {
        return tokenEvolucao; // Retorna o token que pode evoluir esta carta
    }

    public bool CartaClicada()
    {
        return clicada;
    }

    public bool VerificaCartaAtiva()
    {
        return ativa;
    }

    public int DanoCarta()
    {
        return dano;
    }

    public void CalculaDano(int danoInimigo)
    {
        defesa -= danoInimigo;

        if(defesa <= 0)
        {
            Destroy(gameObject); // Destrói a carta se defesa for menor ou igual a zero
        }
        else
        {
            sprite.sortingOrder = 2; // Garante que a carta continue na frente após dano
        }
    }

    public string NomeCarta()
    {
        return carta; // Retorna o nome da carta
    }
}
