using System.Collections.Generic;
using UnityEngine;

public class TokenDropManager : MonoBehaviour
{
    public List<GameObject> tokens; // Lista de prefabs de tokens disponíveis para drop
    public float chanceDeDrop = 0.25f; // Probabilidade de drop (25%)

    // Método para dropar um token
    public void DroparToken(Vector3 posicao)
    {
        if (Random.value <= chanceDeDrop) // Verifica se o drop deve ocorrer
        {
            if (tokens.Count > 0)
            {
                int indice = Random.Range(0, tokens.Count);
                GameObject tokenDroppado = Instantiate(tokens[indice], posicao, Quaternion.identity);
                Debug.Log("Token droppado: " + tokenDroppado.name);
            }
        }
        else
        {
            Debug.Log("Nenhum token droppado desta vez.");
        }
    }
}
