using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour, IPointerClickHandler
{
    
    [SerializeField]
    public Image icon;//display icon

    [SerializeField]
    public Image borderIcon;

    private bool empty = true;
    public bool IsEmpty => empty;

    public event Action<UIInventoryItem> OnItemClicked;
    public void SetData(Sprite sprite)
    {
        this.icon.gameObject.SetActive(true);
        this.icon.sprite = sprite;
        empty = false;
    }
    public void Select()
    {
        borderIcon.enabled = true;
    }

    public void ClearSlot()
    {
        icon.sprite = null;
        icon.enabled = false;
        Deselect();
        empty = true;
    }

    public void Awake()
    {
        ResetData();
        Deselect();
    }

    public void Deselect()
    {
        borderIcon.enabled = false;
    }

    private void ResetData()
    {
        this.icon.gameObject.SetActive(false);
        empty = true;
    }

    public void HandlePointerClick(BaseEventData data )
    {
        PointerEventData pointerData = data as PointerEventData;

        if (pointerData != null && pointerData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Clique detectado via EventTrigger em: " + gameObject.name);
            OnItemClicked?.Invoke(this);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clique detectado: " + gameObject.name);
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnItemClicked?.Invoke(this);
        }
    }
}
