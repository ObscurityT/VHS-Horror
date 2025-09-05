using TMPro;
using UnityEngine;

public class RuleUIController : MonoBehaviour
{
    public static RuleUIController instance;

    public GameObject ruleCanvas;
    public TMP_Text ruleUIText;
    private GameObject lastRuleObject;

    private PuzzleAudioHelper audioHelper;

    private void Start()
    {
        audioHelper = GetComponent<PuzzleAudioHelper>();
    }

    void Awake()
    {
        instance = this;
    }

    public void ShowRule(string content, GameObject ruleObject)
    {
        lastRuleObject = ruleObject;
        ruleUIText.text = content;
        ruleCanvas.SetActive(true);

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        FindFirstObjectByType<PlayerController>().canLook = false;
    }

    public void CloseRule()
    {
        ruleCanvas.SetActive(false);

        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        var player = FindFirstObjectByType<PlayerController>();
        if (player != null) player.canLook = true;

        if (lastRuleObject == null)
        {
            Debug.LogWarning("lastRuleObject está NULL!");
            return;
        }

        var rulePickup = lastRuleObject.GetComponent<RulesPickup>();
        if (rulePickup == null)
        {
            Debug.LogWarning("RulePickup não encontrado no objeto da regra");
            return;
        }

        DiaryManager.Instance.AddRulePage(rulePickup.ruleOrder, rulePickup.ruleKey);

        Destroy(lastRuleObject.gameObject);
        lastRuleObject = null;

        var diaryUI = FindFirstObjectByType<DiaryTabsUI>();
        if (diaryUI != null && diaryUI.gameObject.activeSelf)
        {
            diaryUI.SelectTab("Rules");
        }
    }
}
