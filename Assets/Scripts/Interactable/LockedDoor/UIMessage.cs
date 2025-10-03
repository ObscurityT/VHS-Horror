using UnityEngine;

public class UIMessage : MonoBehaviour
{
    public static UIMessage Instance;

    [SerializeField] public LocalizedText messageText;
    [SerializeField] private GameObject messagePanel;

    private void Awake()
    {
        Instance = this;
        messagePanel.SetActive(false);
    }

    public void ShowMessage(string key, float duration = 2f)
    {
        messagePanel.SetActive(true);
        messageText.key = key;
        messageText.UpdateText();
        CancelInvoke();
        Invoke(nameof(HideMessage), duration);
    }

    private void HideMessage()
    {
        messagePanel.SetActive(false);
    }
}
