using UnityEngine;
using UnityEngine.UI;

public class DragGhostManager : MonoBehaviour
{
    public static DragGhostManager Instance;

    private Image ghostImage;

    void Awake()
    {
        Instance = this;
        GameObject obj = new GameObject("DragGhostImage", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        obj.transform.SetParent(transform, false);
        ghostImage = obj.GetComponent<Image>();
        ghostImage.raycastTarget = false;
        ghostImage.enabled = false;
    }

    public void ShowGhost(Sprite sprite)
    {
        ghostImage.sprite = sprite;
        ghostImage.SetNativeSize();
        ghostImage.rectTransform.sizeDelta *= 0.7f;
        ghostImage.enabled = true;
    }

    public void HideGhost()
    {
        ghostImage.enabled = false;
    }

    void Update()
    {
        if (ghostImage.enabled)
        {
            ghostImage.transform.position = Input.mousePosition;
        }
    }
}
