using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Bag : MonoBehaviour, IPointerClickHandler, IPointerUpHandler {

    public Sprite[] sprites;

    private DragObject dragScript;
    private List<string> itemsInside;
    private WaitForSeconds seconds;
    private Image image;

    void Start() {
        image = GetComponent<Image>();
        image.sprite = sprites[0];
        seconds = new WaitForSeconds(.2f);
        itemsInside = new List<string>();
        dragScript = GetComponent<DragObject>();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if(!dragScript.enabled && itemsInside.Count > 0) {
            CloseBag();
        }
    }

    private void CloseBag() {   // Close animation - status
        image.sprite = sprites[1];
        AudioManager.Instance.CloseBag();
        dragScript.enabled = true;
        StartCoroutine(ChangeBagSize());
    }

    IEnumerator ChangeBagSize() {
        transform.localScale += new Vector3(.5f, .5f, .5f);
        yield return seconds;
        transform.localScale -= new Vector3(.5f, .5f, .5f);
    }

    public void AddItem(string name) {
        itemsInside.Add(name);
        AudioManager.Instance.DropObjectInBag();
    }

    public void OnPointerUp(PointerEventData eventData) {
        if(dragScript.enabled) {
            try {
                if (eventData.pointerCurrentRaycast.gameObject.tag == "Client") {
                    dragScript.enabled = false;
                    eventData.pointerCurrentRaycast.gameObject.GetComponent<CurrentClient>().CheckCombination(itemsInside);
                    itemsInside.Clear();
                    image.sprite = sprites[0];
                }
                if (eventData.pointerCurrentRaycast.gameObject.tag == "Trash") {
                    dragScript.enabled = false;
                    itemsInside.Clear();
                    AudioManager.Instance.DropBagInTrash();
                    image.sprite = sprites[0];
                }
            } catch (NullReferenceException e) { }
        }        
    }
}
