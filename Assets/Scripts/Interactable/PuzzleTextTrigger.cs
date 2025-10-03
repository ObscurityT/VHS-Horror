using UnityEngine;

public class PuzzleTextTrigger : MonoBehaviour
{
    public FloatingText floatingText;
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
            floatingText.ShowText();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
            floatingText.HideText();
    }
}
