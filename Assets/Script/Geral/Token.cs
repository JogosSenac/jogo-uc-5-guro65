using UnityEngine;
using System.Collections.Generic;

public class Token : MonoBehaviour
{
    public bool ativo; // Define se o token está ativo
    public List<Carta> cartasValidas; // Lista de cartas associadas a este token
    public Carta cartaSelecionada; // Carta associada a este token

    private void Awake()
    {
        ativo = false; // Inicialmente, o token está inativo
    }

    public void SetCartaSelecionada(Carta carta)
    {
        if (cartasValidas.Contains(carta)) // Verifica se a carta está na lista de cartas válidas
        {
            cartaSelecionada = carta; // Define a carta selecionada se for válida
        }
        else
        {
            Debug.LogWarning("A carta selecionada não é válida para este token.");
        }
    }

    public Carta GetCartaSelecionada()
    {
        return cartaSelecionada;
    }

    public string NomeToken()
    {
        return gameObject.name; // Retorna o nome do objeto do token
    }

    public void ResetToken()
    {
        ativo = false; // Reinicia o estado do token
        cartaSelecionada = null; // Reseta a carta selecionada
    }

    public void EvoluirCarta()
    {
        if (cartaSelecionada != null && cartaSelecionada.cartaEvolutivaPrefab != null)
        {
            Vector3 posicaoCarta = cartaSelecionada.transform.position;
            Destroy(cartaSelecionada.gameObject); // Destrói a carta antiga

            GameObject novaCarta = Instantiate(cartaSelecionada.cartaEvolutivaPrefab, posicaoCarta, Quaternion.identity); // Instancia a nova carta
            Debug.Log($"Carta {cartaSelecionada.NomeCarta()} evoluiu para: {novaCarta.GetComponent<Carta>().NomeCarta()}");

            ResetToken(); // Reseta o token após a evolução
        }
        else
        {
            Debug.LogWarning("Não há carta selecionada ou prefab de evolução não está definido.");
        }
    }
}
