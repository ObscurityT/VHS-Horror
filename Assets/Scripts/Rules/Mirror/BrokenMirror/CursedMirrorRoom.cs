using UnityEngine;

public class CursedMirrorRoom : MonoBehaviour
{
    public float timeLimit = 3f;
    public int decreaseAmount = 1;
    private float timer = 0f;
    private bool playerInside = false;
    private PlayerStatus status;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            timer = 0f;

            status = other.GetComponent<PlayerStatus>();
            if (status == null)
                Debug.LogWarning("Player entrou na sala, mas não tem PlayerStatus.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            timer = 0f;
        }
    }

    private void Update()
    {
        if (playerInside)
        {
            timer += Time.deltaTime;

            if (timer >= timeLimit)
            {
                TriggerPunishment();
                timer = 0f; 
                Debug.Log("Jogador ficou muito tempo na sala do espelho amaldiçoado!");
            }
        }
    }

    private void TriggerPunishment()
    {
        status.DecreaseSanity(decreaseAmount);
    }
}
