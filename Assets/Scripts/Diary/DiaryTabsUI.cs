using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiaryTabsUI : MonoBehaviour
{

    [Header("Diary Canvas")]
    public GameObject diaryCanvas;

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
    public Transform tabsContainer;
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

        rulesTabButton.onClick.RemoveAllListeners();
        legendsTabButton.onClick.RemoveAllListeners();

        rulesTabButton.onClick.AddListener(() => SelectTab("Rules"));
        legendsTabButton.onClick.AddListener(() => SelectTab("Legends"));

        SelectTab("Rules");

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ToggleDiary();
        }
    }

    void ToggleDiary()
    {
        diaryCanvas.SetActive(!diaryCanvas.activeSelf);

        if (diaryCanvas.activeSelf)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            var player = FindFirstObjectByType<PlayerController>();
            if (player != null) player.canLook = false;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            var player = FindFirstObjectByType<PlayerController>();
            if (player != null) player.canLook = true;
        }
    }

    public void CloseDiary()
    {
        isFlipping = false;
        StopAllCoroutines();

        if (paginaAnimadaDir)
        {
            paginaAnimadaDir.gameObject.SetActive(false);
            paginaAnimadaDir.pivot = new Vector2(0f, 0.5f);
            paginaAnimadaDir.localRotation = Quaternion.identity;
        }
        if (paginaAnimadaEsq)
        {
            paginaAnimadaEsq.gameObject.SetActive(false);
            paginaAnimadaEsq.pivot = new Vector2(1f, 0.5f);
            paginaAnimadaEsq.localRotation = Quaternion.identity;
        }

        diaryCanvas.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        var player = FindFirstObjectByType<PlayerController>();
        if (player) player.canLook = true;
    }

    public void SelectTab(string tab)
    {

        //if (isFlipping) return;

        currentTab = tab;
        currentPageIndex = 0;

        bool isRulesActive = tab == "Rules";

        rulesTabButton.image.sprite = isRulesActive ? selectedSprite : unselectedSprite;
        legendsTabButton.image.sprite = isRulesActive ? unselectedSprite : selectedSprite;

        rulesContent.SetActive(isRulesActive);
        legendsContent.SetActive(!isRulesActive);

        if (isRulesActive)
            rulesTabTransform.SetAsLastSibling();
        else
            legendsTabTransform.SetAsLastSibling();


        if (isRulesActive)
            rulesPages = DiaryManager.Instance.GetRulesPages(); 
        else
            legendsPages = DiaryManager.Instance.GetLegendPages();

        UpdatePages();

        nextPageButton.gameObject.SetActive(true);
        previousPageButton.gameObject.SetActive(true);
    }

    void UpdatePages()
    {
        var pages = GetCurrentPages();

        Debug.Log($"[UpdatePages] Tab: {currentTab} | Página atual: {currentPageIndex} de {pages.Count}");

        if (pages.Count == 0)
        {
            leftPageText.text = "";
            rightPageText.text = "";
            nextPageButton.interactable = false;
            previousPageButton.interactable = false;
            return;
        }

        currentPageIndex = Mathf.Clamp(currentPageIndex, 0, pages.Count - 1);
        if (currentPageIndex % 2 != 0) currentPageIndex--; 

        leftPageText.text = currentPageIndex < pages.Count ? pages[currentPageIndex] : "";
        rightPageText.text = (currentPageIndex + 1 < pages.Count) ? pages[currentPageIndex + 1] : "";

        nextPageButton.interactable = currentPageIndex + 2 < pages.Count;
        previousPageButton.interactable = currentPageIndex - 2 >= 0;

        Debug.Log($"[UpdatePages] Botão voltar: {(previousPageButton.interactable ? "ativo" : "inativo")}");
    }

    public void NextPage()
    {
        //if (isFlipping) return;
        //var pages = GetCurrentPages();
        //int nextIndex = currentPageIndex + 2;
        //if (nextIndex <= pages.Count - 1)
        //    StartCoroutine(FlipPageAndUpdate(nextIndex));

        if (isFlipping) return;

        List<string> pages = GetCurrentPages();
        int nextIndex = currentPageIndex + 2;

        Debug.Log($"Tentando virar para a página {nextIndex}");

        if (nextIndex <= pages.Count - 1)
        {
            Debug.Log("Chamando FlipPageAndUpdate!");
            StartCoroutine(FlipPageAndUpdate(nextIndex));
        }
        else
        {
            Debug.Log("Não tem mais páginas suficientes para virar.");
        }
    }

    public void PreviousPage()
    {
        if (isFlipping) return;
        int previousIndex = currentPageIndex - 2;
        if (previousIndex >= 0)
            StartCoroutine(FlipPageBackAndUpdate(previousIndex));
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

        paginaAnimadaDir.pivot = new Vector2(0.5f, 0.5f);
        paginaAnimadaDir.localRotation = Quaternion.Euler(0, 0f, 0);

        paginaAnimadaEsq.gameObject.SetActive(false);

        const float duration = 0.6f;
        float t = 0f;

        while (t < duration)
        {
            float rot = Mathf.Lerp(0, 180, t / duration);
            paginaAnimadaDir.localRotation = Quaternion.Euler(0, rot, 0);
            t += Time.unscaledDeltaTime; 
            yield return null;
        }

        paginaAnimadaDir.localRotation = Quaternion.Euler(0, 180f, 0);
        paginaAnimadaDir.gameObject.SetActive(false);

        currentPageIndex = newPageIndex;
        UpdatePages();
        isFlipping = false;
    }

    IEnumerator FlipPageBackAndUpdate(int newPageIndex)
    {
        isFlipping = true;

        paginaAnimadaEsq.gameObject.SetActive(true);
        imagemPaginaEsq.sprite = paginaSpriteEsq;

        paginaAnimadaEsq.pivot = new Vector2(0.5f, 0.5f);
        paginaAnimadaEsq.localRotation = Quaternion.Euler(0, 0f, 0);

        paginaAnimadaDir.gameObject.SetActive(false);

        const float duration = 0.6f;
        float t = 0f;

        while (t < duration)
        {
            float rot = Mathf.Lerp(0, -180, t / duration);
            paginaAnimadaEsq.localRotation = Quaternion.Euler(0, rot, 0);
            t += Time.unscaledDeltaTime; 
            yield return null;
        }

        paginaAnimadaEsq.localRotation = Quaternion.Euler(0, -180f, 0);
        paginaAnimadaEsq.gameObject.SetActive(false);

        currentPageIndex = newPageIndex;
        UpdatePages();
        isFlipping = false;
    }

}