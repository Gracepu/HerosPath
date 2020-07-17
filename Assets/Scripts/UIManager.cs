using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {
    
    public static UIManager Instance { private set; get; }

    public GameObject[] enemies;
    public TextMeshProUGUI score;

    public GameObject endPanel;
    public TextMeshProUGUI finalScore;

    public delegate void EnemyEvent();
    public static event EnemyEvent OnEnemyEvent;

    void Start() {
        Instance = this;
    }

    public void ActivateEndPanel(int finalScore) {
        Time.timeScale = 0f;
        PauseMenu.PauseForEnd();
        this.finalScore.text = finalScore.ToString();
        endPanel.SetActive(true);
    }

    public void UpdateScore(int score) {
        this.score.text = score.ToString();
    }

    public void EnemyPopUp(Fears fear) {
        OnEnemyEvent();
        int n = 0;
        switch(fear) {
            case Fears.DRAGON:
                n = 0;
                break;

            case Fears.GHOST:
                n = 1;
                break;

            case Fears.TROLL:
                n = 2;
                break;
        }

        for (int i = 0; i < enemies.Length; i++) {
            if (i == n) enemies[i].SetActive(true);
            else enemies[i].SetActive(false);
        }

        enemies[n].transform.parent.gameObject.SetActive(true);
        StartCoroutine(DissapearEnemyPopUp(n));
    }

    IEnumerator DissapearEnemyPopUp(int n) {
        yield return new WaitForSeconds(2f);
        enemies[n].transform.parent.gameObject.SetActive(false);
    }
}
