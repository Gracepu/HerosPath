using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicLoad : MonoBehaviour {
    private static MusicLoad instance = null;

    public static MusicLoad Instance {
        get { return instance; }
    }

    void Awake() {
        if (instance == null) {
            DontDestroyOnLoad(gameObject);
            instance = this;

        } else Destroy(gameObject);
    }
}

