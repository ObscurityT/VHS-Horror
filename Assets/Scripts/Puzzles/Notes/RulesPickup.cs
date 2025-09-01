using UnityEngine;

public class RulesPickup : MonoBehaviour, IInteractable
{
    [Header("Texto e Ordem da Regra")]
    [TextArea]
    public string ruleText;
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

        RuleUIController.instance.ShowRule(ruleText, this.gameObject);
    }
}