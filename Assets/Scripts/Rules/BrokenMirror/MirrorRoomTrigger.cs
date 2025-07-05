using System.Collections;
using UnityEngine;

public class MirrorRoomTrigger : MonoBehaviour
{
    public float maxTimeInRoom = 5f; // tempo permitido
    private float timeInside = 0f;
    private bool playerInside = false;

    PlayerStatus playerStatus;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (HasBrokenMirror())
            {
                playerInside = true;
                timeInside = 0f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            timeInside = 0f;
        }
    }

    private void Update()
    {
        if (playerInside)
        {
            timeInside += Time.deltaTime;
            if (timeInside >= maxTimeInRoom)
            {
                TriggerPunishment();
                playerInside = false;
            }
        }
    }

    private bool HasBrokenMirror()
    {
        Collider[] mirrors = Physics.OverlapBox(transform.position, transform.localScale / 2f);
        foreach (var mirror in mirrors)
        {
            if (mirror.CompareTag("BrokenMirror"))
                return true;
        }
        return false;
    }

    private void TriggerPunishment()
    {
        Debug.Log("Punição ativada!");
        float sanityBefore = playerStatus.GetCurrentSanity();

        Debug.Log($"Jogador ficou tempo demais com espelho quebrado!");
        Debug.Log($"Sanidade antes: {sanityBefore}");

        playerStatus.DecreaseSanity(1);

        float sanityAfter = playerStatus.GetCurrentSanity();
        Debug.Log($"Sanidade depois: {sanityAfter}");


    }
}
