using System.Collections.Generic;
using UnityEngine;

public class BotaoEvoluir : MonoBehaviour
{
    public List<Token> tokens; // Lista de tokens que podem ser usados para evolução
    private Carta cartaSelecionada;

    public void SetCartaSelecionada(Carta carta)
    {
        cartaSelecionada = carta;
        // Lógica adicional se necessário
    }

    public void ResetCartaSelecionada()
    {
        cartaSelecionada = null; // Reseta a carta selecionada
    }

        public void TentarEvoluir()
    {
        Carta carta = FindObjectOfType<Token>().GetCartaSelecionada(); // Obtém a carta selecionada pelo token
        if (carta != null && PodeEvoluir(carta))
        {
            Debug.Log($"Evoluindo a carta: {carta.NomeCarta()} para {carta.nomeEvolucao}");
            GameObject cartaEvolutiva = Instantiate(carta.cartaEvolutivaPrefab, carta.transform.position, Quaternion.identity);
            Destroy(carta.gameObject);
        }
        else
        {
            Debug.Log("Não é possível evoluir.");
        }
    }

    private bool PodeEvoluir(Carta carta)
    {
        // Verifica se há algum token ativo que pode ser usado para evoluir a carta
        foreach (Token token in tokens)
        {
            if (token.ativo && token.NomeToken() == carta.NomeCarta()) // Verifica se o token está ativo
            {
                return true;
            }
        }
        return false;
    }

    // Método chamado pelo botão de evolução
    public void OnBotaoEvolucaoClick()
    {
        TentarEvoluir(); // Chama o método que tenta evoluir a carta selecionada
    }
}
