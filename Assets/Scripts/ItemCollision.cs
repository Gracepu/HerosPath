using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemCollision : MonoBehaviour, IPointerExitHandler {

    // Drop object over bag
    public void OnPointerExit(PointerEventData eventData) {
        try {
            if(eventData.pointerCurrentRaycast.gameObject.tag == "Bag") {
                eventData.pointerCurrentRaycast.gameObject.GetComponent<Bag>().AddItem(name);
            }

        } catch (NullReferenceException e) { }
    }
}
