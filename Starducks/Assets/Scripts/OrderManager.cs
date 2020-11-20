using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour // only for the order placing scene. apologies for the misleading name!
{
    [SerializeField]
    float timer, maxTime, timeDif, minTime;

    [SerializeField]
    int currentOrder;

    GameController gc;

    [SerializeField]
    TextMeshProUGUI orderText; // order being displayed

    [SerializeField]
    Slider slider;

    [SerializeField]
    GameObject[] myPanels; // 0 is drink type, 1 is temp, 2 is flavor, 3 is size

    [SerializeField]
    GameObject backButton, doneButton;
    int currentPanel;


    [SerializeField]
    Order rightOrder, newOrder; // newOrder will be manipulated during the game

    Character character; // character will be randomly generated for now



    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = maxTime;

        gc = GameController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentOrder < gc.maxOrders) // max orders per round. something we can alter and tune ourseves
        {
            if (timer <= 0)
            {
                OnToNextOrder();
            }
            timer -= Time.deltaTime;
            
        }
        slider.value = timer;
    }

    void OnToNextOrder(bool finishedOrder = false)
    {
        if (finishedOrder)
        {
            newOrder.myCharacter = character;
            newOrder.myName = rightOrder.myName; // temporarily here unless we decided to make name stuff a thing
            gc.orders.Add(newOrder);

            if (newOrder.doesMatch(rightOrder))
            {
                gc.correct++;
                Debug.Log("Nice!!!");
            }
            else
            {
                gc.incorrect++;
                Debug.Log("not cool bruv");
            }
        }
        maxTime -= timeDif;
        if (maxTime < minTime)
        {
            maxTime = minTime;
        }
        timer = maxTime;
        currentOrder++;

        if (currentOrder == gc.maxOrders)
        {
            gc.sc.LoadScene("BrewGame"); // temporarily goes straight to lid game for testing purposes
        }


        // resetting for next order
        newOrder = new Order();
        newOrder.myDrink = new Drink();
        character = new Character();
        rightOrder = new Order();

        if (gc.currentCustomers.Count > 0) character = gc.GetRandomCharacter();
        else character.CreateRandomCharacter();

        rightOrder = character.getOrder();
      

        orderText.text = "Hi! Can I have a " +  rightOrder.myDrink.getDrinkName();
        currentPanel = 0;
        ShowPanel();
    }

    public void FlavorPicked(int f)
    {
        newOrder.myDrink.flavor = (Flavor)f;
        NextPanel();

    }
    public void SizePicked(int s)
    {
        newOrder.myDrink.size = (Size)s;
        OrderFinished();
    }
    public void DrinkTypePicked(int d)
    {
        newOrder.myDrink.drinkType = (DrinkType)d;
        NextPanel();
    }
    public void TempPicked(bool b)
    {
        newOrder.myDrink.isCold = b; 
        NextPanel();
    }

    public void NameEntered(string n)
    {
        newOrder.myName = n;
        NextPanel();
    }    

    public void OrderFinished()
    {
        OnToNextOrder(true);
    }

    void NextPanel()
    {
        currentPanel++;
        ShowPanel();
    }

    public void PrevPanel()
    {
        currentPanel--;
        ShowPanel();
    }
    public void ShowPanel()
    {
        backButton.SetActive(false);
      //  doneButton.SetActive(false); // commenting out momentarily bc it doesn't feel right rn
        foreach (GameObject g in myPanels)
            g.SetActive(false);

        myPanels[currentPanel].SetActive(true);

        if (currentPanel != 0) backButton.SetActive(true);
      //  if (currentPanel == myPanels.Length -1) doneButton.SetActive(true);
    }
}
