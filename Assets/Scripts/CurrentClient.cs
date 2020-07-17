using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public enum Fears { DRAGON, TROLL, GHOST }
public class CurrentClient : MonoBehaviour {

    public Fears fear;
    public float patience = 20f;
    public Items itemNames;
    public Transform sandwich;
    public GameObject itemSandwich;
    public Slider patienceSlider;
    public AudioSource audioSource;

    private int maxItems = 3;
    private float impatience = 10f;
    private float randomChange;
    private List<string> items;
    private List<GameObject> itemSprites;
    private WaitForSeconds seconds;


    private void OnEnable() {
        GameController.OnReceiveFear += ReducePatienceBecauseOfFear;
    }

    private void OnDestroy() {
        GameController.OnReceiveFear -= ReducePatienceBecauseOfFear;
    }

    void Start() {
        audioSource = GetComponent<AudioSource>();
        items = new List<string>();
        itemSprites = new List<GameObject>();
        seconds = new WaitForSeconds(1.5f);
        patienceSlider.maxValue = patience;
        patienceSlider.value = patience;
        GenerateNewCombination();
    }

    void Update() {
        if (patience > 0f) {
            patience -= Time.deltaTime;
            impatience -= Time.deltaTime;
            patienceSlider.value = patience;

            randomChange = Random.Range(0f, 1f);
            if (randomChange > 0.99f && impatience < 0f) {
                StartCoroutine(PatienceIndicator(Color.yellow));
                AudioManager.Instance.ChangeCombination();
                GenerateNewCombination();
            }

        } else {
            GameController.Instance.ClientIsGone(gameObject);
        }
    }

    public void ReducePatienceBecauseOfFear(Fears fear) {
        if (fear == this.fear) {
            StartCoroutine(PatienceIndicator(Color.red));
            AudioManager.Instance.CharacterScream();
            patience -= 10;
            patienceSlider.value = patience;
        }
    }

    public void CheckCombination(List<string> itemsInside) {
        int coincidences = 0;

        for (int i = 0; i < itemsInside.Count; i++) {
            if (items.Contains(itemsInside[i])) {
                coincidences++;
            }
        }
        GameController.Instance.AddScore(coincidences);
        GameController.Instance.ClientIsGone(gameObject);
    }

    private void GenerateNewCombination() {
        items.Clear();
        DeleteSandwichCombination();
        itemSprites.Clear();
        int nItems = Random.Range(1, maxItems + 1);

        for (int i = 0; i < nItems; i++) {
            Sprite randomSprite = itemNames.names[Random.Range(0,itemNames.names.Length)];
            string randomItem = randomSprite.name;

            items.Add(randomItem);
            itemSprites.Add(Instantiate(itemSandwich, sandwich));
            itemSprites[i].GetComponent<Image>().sprite = randomSprite;
        }
        impatience = 10f;
    }

    IEnumerator PatienceIndicator(Color color) {
        GetComponent<Image>().color = color;
        yield return seconds;
        GetComponent<Image>().color = Color.white;
    }

    private void DeleteSandwichCombination() {
        for (int i = 0; i < itemSprites.Count; i++) {
            Destroy(itemSprites[i]);
        }
    }
}
