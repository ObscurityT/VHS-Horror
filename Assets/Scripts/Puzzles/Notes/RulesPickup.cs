using UnityEngine;

public class RulesPickup : MonoBehaviour, IInteractable
{
    [Header("Texto e Ordem da Regra")]
    public string ruleKey;
    public int ruleOrder = 0;

    private PuzzleAudioHelper audioHelper;

    void Start()
    {
        audioHelper = GetComponent<PuzzleAudioHelper>();
    }

    public void Interact(GameObject interactor)
    {
        Debug.Log("Interacting with the rule");

        if (audioHelper != null)
            audioHelper.PlayOpenSound();

        string localizedText = LocalizationManager.Instance.GetText(ruleKey);
        RuleUIController.instance.ShowRule(localizedText, this.gameObject);
    }
}