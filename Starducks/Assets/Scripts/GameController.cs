using FMODUnity;
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

    [Space]
    [Header("Order Stuff")]

    public int maxOrders;

    public List<Order> orders;
    public List<Character> currentCustomers; // these characters are ones we can drag into the inspector for the game! If it's empty we'll have all random characters

    public int currentOrder; // so we can go through each order for each minigame

    public bool isPaused;

    public TipsMenu tipMenu;

    bool playSongOnce;

    public StudioEventEmitter music, ambience;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            if (this != Instance) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        namesList = nameDoc.text.Split('\n'); // splitting text document so each name is grabbed from ea. line

        sc = FindObjectOfType<SceneController>();

        if (sc.IsSceneCalled("BrewGame") || sc.IsSceneCalled("LidGame"))
        {
            if (orders.Count == 0)
            {
                for (int i = 0; i < maxOrders; i++)
                {
                    Order o = new Order();
                    Character c = new Character();
                    c.CreateRandomCharacter();

                    o = c.getOrder();
                    orders.Add(o);
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
       if(sc.IsSceneCalled("StartScreen") && Input.GetMouseButtonDown(0))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/OrderGame/ScreenTap");

            sc.LoadScene("OrderGame");
            tipMenu.gameObject.SetActive(true);
            if(!playSongOnce)
            {
                ambience.gameObject.transform.parent.gameObject.SetActive(true);
                playSongOnce = true;
            }
        }
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
