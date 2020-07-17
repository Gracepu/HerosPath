using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController Instance { private set; get; }

    public float gameTime = 60f;
    public GameObject[] clientSpaces;
    public GameObject[] possibleClients;
    public GameObject[] particles;
    public GameObject parallax;
    
    private float randomChange;
    private int score;
    private float allowEnemy;
    private WaitForSeconds seconds;
    private List<GameObject> clients;

    public delegate void ReceiveFear(Fears fear);
    public static event ReceiveFear OnReceiveFear;

    void Start() {
        Instance = this;
        clients = new List<GameObject>();
        seconds = new WaitForSeconds(1f);
        AddNewClient();
    }

    void Update() {
        if (gameTime > 0f) {
            gameTime -= Time.deltaTime;
            parallax.transform.localPosition += new Vector3(-.23f,0,0);

            randomChange = UnityEngine.Random.Range(0f, 1f);
            if (randomChange > 0.99f && allowEnemy > 10f) ActivateEnemyPopup();
            else allowEnemy += Time.deltaTime;

        } else UIManager.Instance.ActivateEndPanel(score);
    }

    private void ActivateEnemyPopup() {
        
        try {
            Fears fears = (Fears)UnityEngine.Random.Range(0, 3);
            UIManager.Instance.EnemyPopUp(fears);
            OnReceiveFear(fears);
            allowEnemy = 0f;

        } catch (NullReferenceException e) { }
    }

    public void AddScore(int score) {
        this.score += score;
        UIManager.Instance.UpdateScore(this.score);
    }

    public void ClientIsGone(GameObject client) {
        AudioManager.Instance.ClientGoes();
        int n = GetClientPosition(client.transform.parent.name) - 1;    // Get number for particles

        // Delete client from list
        clients.Remove(client);

        // Delete client from scene
        client.transform.SetParent(null);
        Destroy(client);

        particles[n].SetActive(true);

        // Add new client
        StartCoroutine(WaitToAddNewClient(n));
    }

    private int GetClientPosition(string name) {
        for (int i = 0; i < name.Length; i++) {
            if(Char.IsDigit(name[i])) {
                return (int) char.GetNumericValue(name[i]);
            }
        }
        return -1;
    }

    IEnumerator WaitToAddNewClient(int n) {
        yield return seconds;   // ADD WOW EFFECT
        AddNewClient();
        particles[n].SetActive(false);
    }

    private void AddNewClient() {
        for (int i = 0; i < clientSpaces.Length; i++) {
            if (clientSpaces[i].transform.childCount == 0) {
                int n = UnityEngine.Random.Range(0, possibleClients.Length);
                GameObject client = Instantiate(possibleClients[n], clientSpaces[i].transform);
                clients.Add(client);
                AudioManager.Instance.ClientArrives();
            }
        }
    }
}
