using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baralho : MonoBehaviour
{
    public List<GameObject> cartas = new List<GameObject>();
    public List<GameObject> deckPlayer = new List<GameObject>();
    public List<GameObject> deckOponente = new List<GameObject>();
    public Player player;
    public Player oponente;
    public float offsetX;
    public int tempo;
    public GameObject cartaSorteada;
    private Vector3 posCarta;
    private Vector3 offset;
    public int limitePlayer = 5; // Limite de cartas do player
    public int limiteOponente = 5; // Limite de cartas do oponente

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        oponente = GameObject.FindWithTag("Oponente").GetComponent<Player>();
        DeckInicialOponente();
        DeckInicialPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<GameObject> DeckInicial(int limite, string tipo)
    {
        if (tipo == "Player")
        {
            return deckPlayer;
        }
        else if (tipo == "Oponente")
        {
            return deckOponente;
        }
        
        return null;
    }

    public void DeckInicialPlayer()
    {
        Vector3 posCarta = player.LocalDeck();
        Vector3 offset = new Vector3(offsetX, 0, 0);
        for (int i = 0; i < limitePlayer; i++)
        {
            cartaSorteada = cartas[Random.Range(0, cartas.Count)];
            cartaSorteada.gameObject.tag = "Carta Player";
            Instantiate(cartaSorteada, posCarta += offset, Quaternion.identity);
        }
    }

    public void DeckInicialOponente()
    {
        Vector3 posCarta = oponente.LocalDeck();
        Vector3 offset = new Vector3(offsetX, 0, 0);
        for (int i = 0; i < limiteOponente; i++)
        {
            cartaSorteada = cartas[Random.Range(0, cartas.Count)];
            cartaSorteada.gameObject.tag = "Carta Oponente";
            Instantiate(cartaSorteada, posCarta += offset, Quaternion.identity);
        }
    }
}
