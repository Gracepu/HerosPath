using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public void LoadScene(string name) {
        Time.timeScale = 1f;
        SceneManager.LoadScene(name);
    }
}

