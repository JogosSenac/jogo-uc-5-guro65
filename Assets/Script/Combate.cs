using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Combate : MonoBehaviour
{
    public Player player;
    public Player oponente;
    public TextMeshProUGUI textoIndicador;
    public BoxCollider2D localCartas;
    public GameObject textoBotao;
    private GameObject cartaAtivaPlayer;
    private GameObject cartaAtivaOponente;
    private int vez;
    public bool aguardaVez = true;
    public float tempoTurno;
    private bool cartaOponenteSelecionada = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        oponente = GameObject.FindWithTag("Oponente").GetComponent<Player>();
        textoIndicador = GameObject.FindWithTag("Indicador").GetComponent<TextMeshProUGUI>();
        localCartas = GameObject.FindWithTag("Local").GetComponent<BoxCollider2D>();
        vez = 1;
        textoBotao.GetComponent<TextMeshProUGUI>().text = "Inicie o Turno";
    }

    // Update is called once per frame
    void Update()
    {
        if (aguardaVez)
        {
            if (vez == 1)
            {
                VezDoPlayer();
            }
            else if (vez == 2 && !cartaOponenteSelecionada)
            {
                textoIndicador.text = "Aguarde o oponente jogar";
                textoBotao.GetComponent<TextMeshProUGUI>().text = "Oponente Jogar";
            }
            else if (vez == 3)
            {
                FimdoTurno();
            }
        }
    }

    public void JogarCartaOponente()
    {
        if (!cartaOponenteSelecionada && vez == 2)
        {
            cartaAtivaOponente = EscolherCartaAleatoria(oponente);
            if (cartaAtivaOponente != null)
            {
                cartaOponenteSelecionada = true;
                MudaPosicaoCartaOponente();
                StartCoroutine(ContadorTurno());
            }
            else
            {
                textoIndicador.text = "Nenhuma carta disponível para o oponente.";
                textoBotao.GetComponent<TextMeshProUGUI>().text = "Inicie o próximo turno";
                aguardaVez = false;
                cartaOponenteSelecionada = true; // Evitar a seleção de outra carta do oponente
            }
        }
    }

    private void VezDoPlayer()
    {
        textoIndicador.text = "Sua vez!";
        textoBotao.GetComponent<TextMeshProUGUI>().text = "Selecione sua carta";
        aguardaVez = false; // Permite que o jogador faça sua jogada
    }

    private void FimdoTurno()
    {
        textoIndicador.text = "Fim do Turno";
        textoBotao.GetComponent<TextMeshProUGUI>().text = "Aguarde...";
        aguardaVez = false;
    }

    IEnumerator ContadorTurno()
    {
        yield return new WaitForSeconds(tempoTurno);
        FinalizarTurno();
    }

    public void FinalizarTurno()
    {
        StopCoroutine(ContadorTurno());

        if (vez == 1)
        {
            vez++;
            VerificaCartaAtivaPlayer();
            MudaPosicaoCartaPlayer();
        }
        else if (vez == 2)
        {
            vez++;
            // Carta do oponente já foi selecionada e movida
        }
        else if (vez == 3)
        {
            vez = 1;
            FinalizaCombate();
            FimdoTurno(); // Finaliza o turno e prepara para o próximo
        }

        aguardaVez = true; // Permite que o próximo turno comece
        textoBotao.GetComponent<TextMeshProUGUI>().text = "Inicie o Turno";
        cartaOponenteSelecionada = false; // Resetar flag para o próximo turno
    }
    
    private void FinalizaCombate()
    {
        if (cartaAtivaPlayer != null && cartaAtivaOponente != null)
        {
            Carta cartaPlayer = cartaAtivaPlayer.GetComponent<Carta>();
            Carta cartaOponente = cartaAtivaOponente.GetComponent<Carta>();

            // A carta do jogador causa dano à carta do oponente
            cartaOponente.CalculaDano(cartaPlayer.DanoCarta());

            // A carta do oponente causa dano à carta do jogador
            cartaPlayer.CalculaDano(cartaOponente.DanoCarta());

            // Verifica se a carta do jogador foi destruída
            if (cartaPlayer.defesa <= 0)
            {
                Destroy(cartaAtivaPlayer);
                cartaAtivaPlayer = null;
            }

            // Verifica se a carta do oponente foi destruída
            if (cartaOponente.defesa <= 0)
            {
                Destroy(cartaAtivaOponente);
                cartaAtivaOponente = null;
            }

            // Atualiza o texto se uma carta foi destruída
            if (cartaAtivaPlayer == null || cartaAtivaOponente == null)
            {
                textoIndicador.text = "Carta destruída, jogue uma nova.";
                textoBotao.GetComponent<TextMeshProUGUI>().text = "Jogar Nova Carta";
            }
        }
    }

    private GameObject EscolherCartaAleatoria(Player jogador)
    {
        List<GameObject> deck = jogador.DeckNaTela();
        if (deck.Count > 0)
        {
            int indiceAleatorio = Random.Range(0, deck.Count);
            return deck[indiceAleatorio];
        }
        return null;
    }

    public void VerificaCartaAtivaPlayer()
    {
        List<GameObject> deckPlayer = player.DeckNaTela();
        cartaAtivaPlayer = null; // Resetar carta ativa
        foreach (GameObject carta in deckPlayer)
        {
            if (carta != null)
            {
                Carta cartaAtual = carta.GetComponent<Carta>();
                if (cartaAtual != null && cartaAtual.CartaClicada())
                {
                    cartaAtivaPlayer = carta;
                    cartaAtual.DesativaCarta(); // Desativa a carta após seleção
                    break; // Encontrou a carta clicada, pode parar a busca
                }
            }
        }
    }

    public void MudaPosicaoCartaPlayer()
    {
        if (cartaAtivaPlayer != null)
        {
            float posCartaX = localCartas.transform.position.x + 2.3f;
            cartaAtivaPlayer.transform.position = new Vector3(posCartaX, localCartas.transform.position.y, 0);
        }
    }

    public void MudaPosicaoCartaOponente()
    {
        if (cartaAtivaOponente != null)
        {
            float posCartaX = localCartas.transform.position.x - 1.9f;
            cartaAtivaOponente.transform.position = new Vector3(posCartaX, localCartas.transform.position.y, 0);
        }
    }
}