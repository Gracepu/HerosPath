using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance { private set; get; }

    public AudioClip[] audios;

    private AudioSource audioSource;

    private void Start() {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void ClickSound() {
        audioSource.PlayOneShot(audios[0]);
    }

    public void ClientArrives() {
        audioSource.PlayOneShot(audios[1]);
    }

    public void ClientGoes() {
        audioSource.PlayOneShot(audios[2]);
    }

    public void TakeObject() {
        audioSource.PlayOneShot(audios[3]);
    }

    public void DropObject() {
        audioSource.PlayOneShot(audios[4]);
    }

    public void CloseBag() {
        audioSource.PlayOneShot(audios[5]);
    }

    public void CharacterScream() {
        audioSource.PlayOneShot(audios[6]);
    }

    public void ChangeCombination() {
        audioSource.PlayOneShot(audios[7]);
    }

    public void DropBagInTrash() {
        audioSource.PlayOneShot(audios[8]);
    }

    public void DropObjectInBag() {
        audioSource.PlayOneShot(audios[9]);
    }
}
