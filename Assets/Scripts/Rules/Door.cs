using UnityEngine;

public class Door : MonoBehaviour
{
    public string doorID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStatus status = other.GetComponent<PlayerStatus>();
            if (status != null)
            {
                if (status.lastDoorID == doorID)
                {
                    Debug.Log("Você está tentando voltar pela mesma porta!");
                    status.DecreaseSanity(50f);
                    return;
                }

                Debug.Log("Porta: " + doorID);
                status.lastDoorID = doorID;

                // Aqui pode teleportar, trocar cena etc.
            }
        }
    }
}
