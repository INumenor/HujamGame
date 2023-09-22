using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        Inventory draggableItem = dropped.GetComponent<Inventory>();
        draggableItem.parentAfterDrag = transform;
    }
}
