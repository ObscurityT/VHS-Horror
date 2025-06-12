using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [TextArea(2, 5)]
    public string message;

    private bool isActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isActive) return;

        if (other.CompareTag("Player"))
        {
            DialogueManager.Instance.ShowMessage(message);
            isActive = true;
        }
    }
}
