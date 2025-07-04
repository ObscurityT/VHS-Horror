using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiaryTabsUI : MonoBehaviour
{
    [Header("Tab Buttons")]
    public Button rulesTabButton;
    public Button legendsTabButton;

    [Header("Tab Transforms (Buttons as objects)")]
    public Transform rulesTabTransform;
    public Transform legendsTabTransform;

    [Header("Tab Sprites")]
    public Sprite selectedSprite;
    public Sprite unselectedSprite;

    [Header("Page Overlay Reference")]
    public Transform pageOverlayTransform;

    [Header("Navigation Buttons")]
    public Button nextPageButton;
    public Button previousPageButton;

    [Header("Page Texts")]
    public TextMeshProUGUI leftPageText;
    public TextMeshProUGUI rightPageText;

    [Header("Content Containers")]
    public GameObject rulesContent;
    public GameObject legendsContent;

    private List<string> rulesPages = new List<string>();
    private List<string> legendsPages = new List<string>();

    private int currentPageIndex = 0;
    private string currentTab = "Rules";

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Example placeholder content
        rulesPages = new List<string> { "Regras 1", "Regras 2", "Regras 3", "Regras 4" };
        legendsPages = new List<string> { "Lenda 1", "Lenda 2", "Lenda 3" };

        SelectTab("Rules");

        // Button events
        rulesTabButton.onClick.AddListener(() => SelectTab("Rules"));
        legendsTabButton.onClick.AddListener(() => SelectTab("Legends"));
        nextPageButton.onClick.AddListener(NextPage);
        previousPageButton.onClick.AddListener(PreviousPage);
    }

    public void SelectTab(string tab)
    {
        currentTab = tab;
        currentPageIndex = 0;

        bool isRulesActive = tab == "Rules";

        // Sprite swap directly via Button.image
        rulesTabButton.image.sprite = isRulesActive ? selectedSprite : unselectedSprite;
        legendsTabButton.image.sprite = isRulesActive ? unselectedSprite : selectedSprite;

        // Content containers activation
        rulesContent.SetActive(isRulesActive);
        legendsContent.SetActive(!isRulesActive);

        // Reorder inside TabsGroup safely
        if (isRulesActive)
        {
            rulesTabTransform.SetSiblingIndex(pageOverlayTransform.GetSiblingIndex() + 1);
            legendsTabTransform.SetSiblingIndex(pageOverlayTransform.GetSiblingIndex() - 1);
        }
        else
        {
            legendsTabTransform.SetSiblingIndex(pageOverlayTransform.GetSiblingIndex() + 1);
            rulesTabTransform.SetSiblingIndex(pageOverlayTransform.GetSiblingIndex() - 1);
        }

        UpdatePages();
    }

    void UpdatePages()
    {
        List<string> pages = GetCurrentPages();

        leftPageText.text = currentPageIndex < pages.Count ? pages[currentPageIndex] : "";
        rightPageText.text = (currentPageIndex + 1 < pages.Count) ? pages[currentPageIndex + 1] : "";
    }

    public void NextPage()
    {
        List<string> pages = GetCurrentPages();
        if (currentPageIndex + 2 < pages.Count)
        {
            currentPageIndex += 2;
        }
        else if (currentPageIndex + 1 < pages.Count)
        {
            currentPageIndex += 1;
        }
        UpdatePages();
    }

    public void PreviousPage()
    {
        if (currentPageIndex - 2 >= 0)
        {
            currentPageIndex -= 2;
            UpdatePages();
        }
    }

    List<string> GetCurrentPages()
    {
        return currentTab == "Rules" ? rulesPages : legendsPages;
    }
}