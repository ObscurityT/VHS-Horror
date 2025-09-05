using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    
    public string messageKey;

    private bool isActive = false;
    public bool oneShot = true;       
    public bool retriggerOnExit = true;

    private void OnTriggerEnter(Collider other)
    {
        if (oneShot && isActive) return;
        if (!other.CompareTag("Player")) return;

        string localizedText = LocalizationManager.Instance.GetText(messageKey);
        Debug.Log($"[LOC] {messageKey} -> len={localizedText?.Length} | '{localizedText}'");
        DialogueManager.Instance.ShowMessage(localizedText);
        isActive = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (retriggerOnExit) isActive = false;
    }
}
