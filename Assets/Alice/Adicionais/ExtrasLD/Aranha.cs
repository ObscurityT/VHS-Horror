using UnityEngine;

public class Aranha : MonoBehaviour
{
    public float speed = 2f;               // Velocidade da aranha
    public Transform destino;              // O ponto final para onde a aranha vai

    private bool chegou = false;           // Marca se ela chegou no destino

    void Update()
    {
        if (!chegou && destino != null)
        {
            // Calcula a direção para o destino
            Vector3 direcao = (destino.position - transform.position).normalized;

            // Move a aranha na direção do destino
            transform.position += direcao * speed * Time.deltaTime;

            // Checa se ela chegou próximo o suficiente
            if (Vector3.Distance(transform.position, destino.position) < 0.1f)
            {
                chegou = true;
            }
        }
    }
}
