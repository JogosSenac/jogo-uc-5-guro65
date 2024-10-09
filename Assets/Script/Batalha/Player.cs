using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<GameObject> deck = new List<GameObject>(); // Deck do jogador
    public List<GameObject> deckNaTela = new List<GameObject>(); // Cartas na tela
    public List<Token> tokens = new List<Token>(); // Lista de tokens
    public Baralho baralho; // Referência ao baralho
    public GameObject localDeck; // Local onde as cartas serão instanciadas
    public bool minhaVez; // Indica se é a vez do jogador
    public float offsetX; // Offset para posicionar as cartas
    public float tempo; // Tempo entre a colocação das cartas
    public string tipo; // Tipo do jogador (Player ou Oponente)

    void Start()
    {
        baralho = GameObject.Find("Baralho").GetComponent<Baralho>();
        int limite = tipo == "Player" ? baralho.limitePlayer : baralho.limiteOponente; // Obtém o limite correto
        deck = baralho.DeckInicial(limite, tipo);
        StartCoroutine(ColocarCartasNaMesa()); // Inicia a corrotina
    }

    IEnumerator ColocarCartasNaMesa()
    {
        Vector3 posCarta = localDeck.transform.position;
        Vector3 offset = new Vector3(offsetX, 0, 0);
        foreach (GameObject carta in deck)
        {
            Instantiate(carta, posCarta, Quaternion.identity);
            posCarta += offset; // Atualiza a posição para a próxima carta
            yield return new WaitForSeconds(tempo);
        }

        yield return new WaitForSeconds(1.0f);
        PegaCartasNaTela();
    }

    private void PegaCartasNaTela()
    {
        // Buscar todas as cartas ativas no jogo com base no tipo (Player ou Oponente)
        GameObject[] cartasNoJogo = GameObject.FindGameObjectsWithTag(tipo == "Player" ? "Carta Player" : "Carta Oponente");
        foreach (GameObject carta in cartasNoJogo)
        {
            if (!deckNaTela.Contains(carta))  // Verifica se a carta já não está no deck na tela
            {
                deckNaTela.Add(carta);
            }
        }
    }

    // Método que atualiza o deck na tela quando a carta evolui
    public void AtualizarCartaEvoluida(Carta cartaAntiga, GameObject novaCartaPrefab)
    {
        // Encontra a posição da carta antiga no deckNaTela
        int indexCarta = deckNaTela.IndexOf(cartaAntiga.gameObject);
        
        if (indexCarta != -1) // Se a carta antiga está no deckNaTela
        {
            Vector3 posicaoAntiga = cartaAntiga.transform.position;
            Quaternion rotacaoAntiga = cartaAntiga.transform.rotation;

            // Remove a carta antiga da tela e destrói o objeto
            deckNaTela.RemoveAt(indexCarta);
            Destroy(cartaAntiga.gameObject);

            // Instancia o novo prefab da carta evoluída
            GameObject novaCarta = Instantiate(novaCartaPrefab, posicaoAntiga, rotacaoAntiga);
            novaCarta.tag = tipo == "Player" ? "Carta Player" : "Carta Oponente";  // Certifique-se de marcar a carta com a tag apropriada

            // Adiciona a nova carta no lugar da antiga
            deckNaTela.Insert(indexCarta, novaCarta);

            Debug.Log($"Carta evoluída: {novaCarta.GetComponent<Carta>().NomeCarta()} substituiu {cartaAntiga.NomeCarta()}.");
        }
        else
        {
            Debug.LogWarning("A carta antiga não foi encontrada no deck na tela.");
        }
    }

    public void MinhaVez(bool vez)
    {
        minhaVez = vez;
        if (vez) AtivaDeck();
        else DesativaDeck();
    }

    private void AtivaDeck()
    {
        foreach (GameObject carta in deckNaTela)
        {
            if (carta.TryGetComponent<Carta>(out Carta cartaComp))
            {
                cartaComp.AtivaCarta();
            }
        }
    }

    private void DesativaDeck()
    {
        foreach (GameObject carta in deckNaTela)
        {
            if (carta.TryGetComponent<Carta>(out Carta cartaComp))
            {
                cartaComp.DesativaCarta();
            }
        }
    }

    public void AddToken(Token token)
    {
        tokens.Add(token);
        Debug.Log($"Token adicionado: {token.NomeToken()}");
    }

    public void RemoveToken(Token token)
    {
        tokens.Remove(token);
        Debug.Log($"Token removido: {token.NomeToken()}");
    }

    public Vector3 LocalDeck()
    {
        return localDeck.transform.position; // Retorna a posição do local do deck
    }

    public List<GameObject> DeckNaTela()
    {
        return deckNaTela; // Retorna as cartas na tela
    }
}
