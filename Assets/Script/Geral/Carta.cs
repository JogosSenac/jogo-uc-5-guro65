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

    private void Awake() 
    {
        ativa = true;
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        scalaInicial = transform.localScale;
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
            transform.localScale = new Vector3(0.15f, 0.15f, 1);
            sprite.sortingOrder = 2; // Mantém a carta na frente
            statusDefesa.text = "Defesa: " + defesa;
            statusDano.text = "Dano: " + dano;
        }
    }

    private void OnMouseExit() 
    {
        if (ativa && !clicada) 
        {
            transform.localScale = scalaInicial;
            sprite.sortingOrder = 2; // Ordenação padrão ao sair
            statusDefesa.text = "";
            statusDano.text = "";
        }
    }

        private void OnMouseDown()
    {
        if (ativa)
        {
            clicada = !clicada;
            transform.localScale = clicada ? new Vector3(0.15f, 0.15f, 1) : scalaInicial;
            
            // Aqui você deve notificar o Token se foi clicado
            Token token = FindObjectOfType<Token>();
            if (clicada && token != null)
            {
                token.SetCartaSelecionada(this);
            }
            else if (!clicada && token != null)
            {
                token.ResetToken();
            }
        }
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
            Destroy(gameObject);
        }
        else
        {
            sprite.sortingOrder = 2; // Garante que a carta continue na frente após dano
        }
    }

    public string NomeCarta()
    {
        return carta; // Adicionando método para obter o nome da carta
    }
}
