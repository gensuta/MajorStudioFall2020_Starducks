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


       // HERETESTING PURPOSES
       /* characters = new Character[10];
        for(int i = 0; i < characters.Length; i++)
        {
            characters[i] = new Character();
            characters[i].CreateRandomCharacter();
        } // it works btw - Geneva
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public string GrabRandomName() // grabbing random name from big list
    {
        return namesList[Random.Range(0, namesList.Length)];
    }
}
