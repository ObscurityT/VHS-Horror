using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Mensagem")]
    public string messageKey;
    private string messageKeyAtual;

    [Header("Configurações")]
    public bool allowMessageOverride = false;
    private bool isActive = false;
    public bool oneShot = true;
    public bool retriggerOnExit = true;

    private void Awake()
    {
        messageKeyAtual = messageKey;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (oneShot && isActive) return;
        if (!other.CompareTag("Player")) return;

        // Verificações de segurança
        if (LocalizationManager.Instance == null)
        {
            Debug.LogError("[DialogueTrigger] LocalizationManager.Instance está nulo! Verifique se o objeto está ativo na cena.");
            return;
        }

        if (DialogueManager.Instance == null)
        {
            Debug.LogError("[DialogueTrigger] DialogueManager.Instance está nulo! Verifique se o objeto está ativo na cena.");
            return;
        }

        // Obtém texto localizado
        string localizedText = LocalizationManager.Instance.GetText(messageKey);
        Debug.Log($"[LOC] {messageKey} -> len={localizedText?.Length} | '{localizedText}'");

        // Mostra diálogo
        DialogueManager.Instance.ShowMessage(localizedText);

        isActive = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (retriggerOnExit) isActive = false;
    }

    public void OverrideMessage(string novaKey)
    {
        if (allowMessageOverride)
        {
            messageKeyAtual = novaKey;
            Debug.Log($"[DialogueTrigger] Texto sobrescrito para: {novaKey}");
        }
    }
}
