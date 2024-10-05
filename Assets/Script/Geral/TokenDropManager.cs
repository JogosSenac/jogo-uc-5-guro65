using System.Collections.Generic;
using UnityEngine;

public class TokenDropManager : MonoBehaviour
{
    public List<GameObject> tokens;
    [Range(0, 1)]
    public float chanceDeDrop = 0.25f;

    
    public void OnCartaDerrotada()
    {
        DroparToken();
    }

    
    private void DroparToken()
    {
        if (tokens.Count > 0)
        {
            if (Random.value <= chanceDeDrop)
            {
                
                Vector3 posicaoDroppada = new Vector3(-6.0f, -4.0f, 0);

                
                int indice = Random.Range(0, tokens.Count);
                GameObject tokenEscolhido = tokens[indice];

                
                GameObject tokenDroppado = Instantiate(tokenEscolhido, posicaoDroppada, Quaternion.identity);
                Debug.Log("Token droppado: " + tokenDroppado.name);
            }
            else
            {
                Debug.Log("Nenhum token foi droppado devido à chance de drop.");
            }
        }
        else
        {
            Debug.Log("Nenhum token disponível para drop.");
        }
    }
}
