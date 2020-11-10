using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public SceneController sc;

    [SerializeField]
    TextAsset nameDoc;
    [SerializeField]
    string[] namesList; // to be used if we want to give characters random names


    //public Character[] characters; // keeping track of the same characters throughout the game since it's one day

    // keeping track of orders instead of characters
    public List<Order> orders;
    public List<Character> currentCustomers; // these characters are ones we can drag into the inspector for the game! If it's empty we'll have all random characters

    public int currentOrder; // so we can go through each order for each minigame

    public int score;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            if (this != Instance) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        namesList = nameDoc.text.Split('\n'); // splitting text document so each name is grabbed from ea. line

        sc = FindObjectOfType<SceneController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public string GrabRandomName() // grabbing random name from big list
    {
        return namesList[Random.Range(0, namesList.Length)];
    }
    public Character GetRandomCharacter() // get a character from the list to add to the orders, remove from list afterwards to avoid doubles
    {
        if (currentCustomers.Count == 0) return null;
        else
        {
            int c = Random.Range(0, currentCustomers.Count - 1);
            Character holdBaby = currentCustomers[c];
            currentCustomers.Remove(currentCustomers[c]);
            return holdBaby;
        }
    }

}
