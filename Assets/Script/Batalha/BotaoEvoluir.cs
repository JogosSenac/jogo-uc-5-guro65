using System.Collections.Generic;
using UnityEngine;

public class BotaoEvoluir : MonoBehaviour
{
    public List<Token> tokens; // Lista de tokens que podem ser usados para evolução

    public void TentarEvoluir()
    {
        Token tokenAtivo = null; // Variável para armazenar o token ativo
        Carta cartaSelecionada = null; // Variável para armazenar a carta selecionada

        // Encontra o token ativo e a carta selecionada
        foreach (Token token in tokens)
        {
            if (token.ativo)
            {
                tokenAtivo = token;
                cartaSelecionada = token.GetCartaSelecionada();
                break; // Para ao encontrar o primeiro token ativo
            }
        }

        // Verifica se um token ativo e uma carta selecionada foram encontrados
        if (tokenAtivo != null && cartaSelecionada != null)
        {
            Debug.Log($"Tentando evoluir a carta: {cartaSelecionada.NomeCarta()} com o token: {tokenAtivo.NomeToken()}");
            tokenAtivo.EvoluirCarta(); // Chama a função que realiza a evolução da carta
        }
        else
        {
            // Exibe mensagens de erro apropriadas
            if (tokenAtivo == null)
            {
                Debug.LogWarning("Nenhum token ativo encontrado.");
            }
            if (cartaSelecionada == null)
            {
                Debug.LogWarning("Nenhuma carta foi selecionada.");
            }
        }
    }

    // Método chamado pelo botão de evolução
    public void OnBotaoEvolucaoClick()
    {
        Debug.Log("Botão de evolução clicado.");
        TentarEvoluir(); // Chama o método que tenta evoluir a carta selecionada
    }
}
