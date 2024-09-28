using UnityEngine;

public class Token : MonoBehaviour
{
    public string nomeToken; // Nome do token
    public bool ativo; // Indica se o token está ativo
    private Carta cartaSelecionada;

    public void Ativar()
    {
        ativo = true;
        // Lógica adicional para visualização do token ativo, se necessário
    }

    public void Desativar()
    {
        ativo = false;
        // Lógica adicional para visualização do token inativo, se necessário
    }

    public string NomeToken()
    {
        return nomeToken; // Retorna o nome do token
    }

   public void SetCartaSelecionada(Carta carta)
    {
        cartaSelecionada = carta; // Armazena a referência da carta selecionada
        Debug.Log($"Carta selecionada: {carta.NomeCarta()}");
    }

    public void ResetToken()
    {
        cartaSelecionada = null; // Reseta a carta selecionada
        Debug.Log("Token resetado.");
    }

    public Carta GetCartaSelecionada()
    {
        return cartaSelecionada; // Retorna a carta selecionada
    }
}
