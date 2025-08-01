using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiaryTabsUI : MonoBehaviour
{
    [Header("Tab Buttons")]
    public Button rulesTabButton;
    public Button legendsTabButton;

    [Header("Page Animation Direita")]
    public RectTransform paginaAnimadaDir; 
    public Image imagemPaginaDir;          
    public Sprite paginaSpriteDir;

    [Header("Page Animation Esquerda")]
    public RectTransform paginaAnimadaEsq;
    public Image imagemPaginaEsq;
    public Sprite paginaSpriteEsq;

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
    private bool isFlipping = false;

    void Start()
    {
        paginaAnimadaDir.gameObject.SetActive(false);
        paginaAnimadaEsq.gameObject.SetActive(false);

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

        if (isFlipping) return;

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
        if (isFlipping) return;

        List<string> pages = GetCurrentPages();
        bool hasNext = (currentPageIndex + 1 < pages.Count);
        if (!hasNext) return;

        int nextIndex = currentPageIndex + 2 <= pages.Count - 1 ? currentPageIndex + 2 : currentPageIndex + 1;

        StartCoroutine(FlipPageAndUpdate(nextIndex));
    }

    public void PreviousPage()
    {
        if (isFlipping) return;

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

    IEnumerator FlipPageAndUpdate(int newPageIndex)
    {
        isFlipping = true;

        paginaAnimadaDir.gameObject.SetActive(true);
        imagemPaginaDir.sprite = paginaSpriteDir;

        paginaAnimadaEsq.gameObject.SetActive(true);
        imagemPaginaEsq.sprite = paginaSpriteEsq;

        paginaAnimadaDir.pivot = new Vector2(0.5f, 0.5f); // borda direita
        paginaAnimadaDir.localRotation = Quaternion.Euler(0, 0, 0);

        paginaAnimadaEsq.pivot = new Vector2(0.5f, 0.5f); // borda Esq
        paginaAnimadaEsq.localRotation = Quaternion.Euler(0, 0, 0);

        float duration = 0.6f;
        float t = 0f;

        while (t < duration)
        {
            float rot = Mathf.Lerp(0, 180, t / duration);
            paginaAnimadaDir.localRotation = Quaternion.Euler(0, rot, 0);
            paginaAnimadaEsq.localRotation = Quaternion.Euler(0, rot, 0);
            t += Time.deltaTime;
            yield return null;
        }

        paginaAnimadaDir.localRotation = Quaternion.Euler(0, 180f, 0);
        paginaAnimadaDir.gameObject.SetActive(false);

        paginaAnimadaEsq.localRotation = Quaternion.Euler(0, 180f, 0);
        paginaAnimadaEsq.gameObject.SetActive(false);

        currentPageIndex = newPageIndex;
        UpdatePages();

        isFlipping = false;
    }


}