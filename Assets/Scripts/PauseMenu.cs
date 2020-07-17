using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    public static bool pausedGame = false;
    public GameObject pauseMenu;
    public TextMeshProUGUI countdownText;
    public Button pauseButton;

    public delegate void PauseGame();
    public static event PauseGame OnGamePaused;

    public void OpenMenu() {
        if (pausedGame) {
            Resume();
        } else {
            Pause();
            OnGamePaused();
        }
    }

    public static void PauseForEnd() {
        OnGamePaused();
    }

    public void Pause() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        pausedGame = true;
    }

    IEnumerator WaitToGetReady() {
        countdownText.text = "" + 3;
        yield return WaitToResumeGame();

        countdownText.text = "" + 2;
        yield return WaitToResumeGame();

        countdownText.text = "" + 1;
        yield return WaitToResumeGame();

        Time.timeScale = 1f;
        OnGamePaused();

        countdownText.gameObject.SetActive(false);
        pauseButton.interactable = true;
    }

    IEnumerator WaitToResumeGame() {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + 1f) {
            yield return 0;
        }
    }

    public void Resume() {
        pauseMenu.SetActive(false);
        pauseButton.interactable = false;
        countdownText.gameObject.SetActive(true);
        StartCoroutine(WaitToGetReady());
        pausedGame = false;
    }
}
