using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    private bool dragging;
    private Vector3 originalPosition;
    private bool canAct;

    private void OnEnable() {
        PauseMenu.OnGamePaused += GamePaused;
        UIManager.OnEnemyEvent += EnemyEvent;
    }

    private void OnDisable() {
        PauseMenu.OnGamePaused -= GamePaused;
        UIManager.OnEnemyEvent -= EnemyEvent;
    }

    private void Start() {
        originalPosition = transform.position;
    }

    private void Update() {
        if(dragging) {
            OnMouseDrag();
        }
    }

    private void OnMouseDrag() {
        transform.position = Input.mousePosition;
    }

    private void RestartPosition() {
        dragging = false;
        AudioManager.Instance.DropObject();
        transform.position = originalPosition;
    }

    // OnPointer events - drag and drop
    public void OnPointerDown(PointerEventData eventData) {
        dragging = true;
        AudioManager.Instance.TakeObject();
    }

    public void OnPointerUp(PointerEventData eventData) {
        RestartPosition();
    }

    private void GamePaused() {
        canAct = !canAct;
    }

    private void EnemyEvent() {
        RestartPosition();
    }
}
