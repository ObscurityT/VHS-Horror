using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotBook : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Image bookImage;
    public int bookID;

    private static SlotBook draggingSlot;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Mostra ghost image
        DragGhostManager.Instance.ShowGhost(bookImage.sprite);
        draggingSlot = this;
        bookImage.color = new Color(1, 1, 1, 0.5f); 
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragGhostManager.Instance.HideGhost();
        bookImage.color = Color.white;
        draggingSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var dragged = draggingSlot;
        if (dragged != null && dragged != this)
        {
            Debug.Log($"OnDrop chamado! Drag: {dragged.name} (ID:{dragged.bookID}) -> Drop: {name} (ID:{bookID})");

            // Swap
            Sprite tempSprite = bookImage.sprite;
            int tempID = bookID;

            bookImage.sprite = dragged.bookImage.sprite;
            bookID = dragged.bookID;

            dragged.bookImage.sprite = tempSprite;
            dragged.bookID = tempID;
        }
    }
}
