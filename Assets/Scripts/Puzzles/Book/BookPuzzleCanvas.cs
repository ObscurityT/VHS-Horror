using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BookPuzzleCanvas : MonoBehaviour
{
    public GameObject canvas;
    public List<Button> bookButtons;
    public Button submitButton;
    public TextMeshProUGUI feedbackText;

    private bool puzzleSolved = false;

    private List<int> correctOrder = new List<int> { 0, 1, 2 };
    private List<int> currentOrder = new List<int>();

    void Start()
    {
        submitButton.onClick.AddListener(CheckOrder);

        foreach (Button btn in bookButtons)
        {
            btn.onClick.AddListener(() => OnBookClicked(btn));
        }

        feedbackText.text = "Escolha os livros na ordem.";
    }

     void Update()
    {
        if(canvas.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        { CloseCanvas(); }    
    }

    void OnBookClicked(Button btn)
    {
        int id = btn.GetComponent<BookButton>().bookID;    

        if (!currentOrder.Contains(id))
        {
            currentOrder.Add(id);
            feedbackText.text = $"Selecionados: {currentOrder.Count}/3";
        }
    }

    void CheckOrder()
    {
        if (currentOrder.Count < correctOrder.Count)
        {
            feedbackText.text = "Você precisa selecionar os 3 livros.";
            return;
        }

        for (int i = 0; i < correctOrder.Count; i++)
        {
            if (currentOrder[i] != correctOrder[i])
            {
                feedbackText.text = "Ordem incorreta!";
                currentOrder.Clear();
                return;
            }
        }
        puzzleSolved = true;
        feedbackText.text = "Correto! Puzzle resolvido.";
        StartCoroutine(CloseAfterDelay(1f));
        // abrir uma porta, tocar um som, etc
    }
    private System.Collections.IEnumerator CloseAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // funciona com Time.timeScale = 0
        CloseCanvas();
    }


    public void OpenCanvas()
    {
        if (puzzleSolved) return;
        FindFirstObjectByType<PlayerController>().canLook = false;
        canvas.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        currentOrder.Clear();
        feedbackText.text = "Escolha os livros na ordem.";

    }

    public void CloseCanvas()
    {
        FindFirstObjectByType<PlayerController>().canLook = true;
        canvas.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}