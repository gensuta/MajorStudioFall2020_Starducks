using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BrewMachine : MonoBehaviour
{
    [SerializeField]
    bool isOn;
    [SerializeField]
    Drink rightDrink;

    [SerializeField]
    Slider drinkAmount; // this is what goes up when the brewMachine is on
    [SerializeField]
    Image sliderImg,stopLine;

    public int currentFlavor = 1;
    [SerializeField]
    float[] stopPoints,stopPositions;// where to stop for the brew machine depending on the size of the drink. prolly needs a better name

    [SerializeField]
    TextMeshProUGUI flavorText, toggleText;

    [SerializeField]
    Image[] cups;

    BrewManager bm;

    DraggableCup currentCup;

    bool isCold;
    Size chosenSize;

    List<Order> orders;

    public float brewSpeed;

    void Start()
    {
        bm = FindObjectOfType<BrewManager>();
        flavorText.text = ToFlavor(currentFlavor);
        drinkAmount.maxValue = stopPoints[stopPoints.Length - 1] + 0.5f;

        orders = new List<Order>();
        foreach (Order o in GameController.Instance.orders)
        {
            orders.Add(o);
        }

        foreach (Image i in cups)
        {
            if (isCold)
                i.color = Color.blue;
            else
                i.color = Color.red;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.Instance.isPaused)
            if (isOn) drinkAmount.value += (Time.deltaTime * brewSpeed);
    }

    public void PlaceCup(DraggableCup c) // when mouse lets go of a cup over a brew machine
    {
        currentCup = c;
        rightDrink = c.myDrink;
        sliderImg.color = Color.gray;
        c.enabled = false;
    }

    public void ClickToPlaceCup(int s)
    {
        rightDrink = null;
        sliderImg.color = Color.gray;
        chosenSize = (Size)s;
    }

    string ToFlavor(int flavorNum) // placing this here because enums can't have spaces ;-;
    {
        Flavor f = (Flavor)flavorNum;
        if (f.ToString() == Flavor.SaltedCaramel.ToString())
            return "Salted Caramel";
        else if (f.ToString() == Flavor.PumpkinSpice.ToString())
            return "Pumpkin Spice";
        else
            return f.ToString();
    }

    public void IcedButtonPressed()
    {
        isCold = true;
    }
    public void HotButtonPressed()
    {
        isCold = false;
    }

    public void ToggleTemperature()
    {
        isCold = !isCold;
        if (isCold) toggleText.text = "Switch to cold";
        else toggleText.text = "Switch to hot";

        foreach(Image i in cups)
        {
            if (isCold)
                i.color = Color.blue;
            else
                i.color = Color.red;
        }
    }

    public void NextFlavor()
    {
        if (currentFlavor < (int)Flavor.max - 1) currentFlavor++;
        else currentFlavor = 1;
        flavorText.text = ToFlavor(currentFlavor);
    }
    public void PrevFlavor()
    {
        if (currentFlavor > (int)Flavor.min + 1) currentFlavor--;
        else currentFlavor = (int)Flavor.PumpkinSpice;
        flavorText.text = ToFlavor(currentFlavor);
    }

    public void Brew()
    {
        if (!isOn)
        {
            isOn = true;
            stopLine.gameObject.SetActive(true);
            PlaceStopLine();
        }
        else
        {
            isOn = false;
            stopLine.gameObject.SetActive(false);

        }
    }

    void PlaceStopLine()
    {
        if (rightDrink != null) chosenSize = rightDrink.size;
        switch(chosenSize)
        {
            case (Size.small):
                stopLine.rectTransform.anchoredPosition = GetNewAnchoredPos(Size.small);
                break;
            case (Size.medium):
                stopLine.rectTransform.anchoredPosition = GetNewAnchoredPos(Size.medium);
                break;
            case (Size.large):
                stopLine.rectTransform.anchoredPosition = GetNewAnchoredPos(Size.large);
                break;
        }
    }

    Vector3 GetNewAnchoredPos(Size s)
    {
        Vector3 newPos;
        newPos = stopLine.rectTransform.anchoredPosition;

        float x = stopPositions[(int)s];

        newPos = new Vector3(x, newPos.y, newPos.z);
        return newPos;
    }

    public void Serve()
    {
        if (rightDrink != null)
        {
            Debug.Log(Mathf.Abs(drinkAmount.value - stopPoints[(int)rightDrink.size]));

            Drink newDrink = new Drink();
            newDrink.flavor = (Flavor)currentFlavor;
            newDrink.size = rightDrink.size;
            newDrink.drinkType = rightDrink.drinkType;
            newDrink.isCold = isCold;


            //currently irrelevant old code

            /*if (rightDrink.doesMatch(newDrink) && (Mathf.Abs(drinkAmount.value - stopPoints[(int)rightDrink.size]) < 0.7f))
            {
                ScoreHandler.sc.CorrectMove();
            }
            else
            {
                ScoreHandler.sc.CorrectMove();
            }*/

            Destroy(bm.currentDrinks[currentCup.drinkNum]);
            bm.numSpawned--;

            
        }
        else
        {
            DoesMatchAnOrder();
        }

        bm.ordersComplete++;
        drinkAmount.value = 0;
        sliderImg.color = Color.white;
    }

    void DoesMatchAnOrder()
    {
        Drink createdDrink = new Drink();
        createdDrink.size = chosenSize;
        createdDrink.isCold = isCold;
        createdDrink.flavor = (Flavor)currentFlavor;

        int n = 0, similarities = 0, prevSim = -1, simOrder = -1;
        bool isCorrect = false;
        foreach(Order o in GameController.Instance.orders)
        {
            if (o.myDrink.doesSortaMatch(createdDrink))
            {
                // int in the score is 1 bc we have 3 games and start at 0 for counting
                o.scores[1].CountStuff(createdDrink, o.myDrink,true);
                o.scores[1].BrewGameDistBonus(drinkAmount.value, stopPoints[(int)createdDrink.size]);
                isCorrect = true;
                break;
            }

            if (o.myDrink.size == createdDrink.size) similarities++;
            if (o.myDrink.isCold == createdDrink.isCold) similarities++;
            if (o.myDrink.flavor == createdDrink.flavor) similarities++;

            if (prevSim < similarities)
            {
                prevSim = similarities;
                simOrder = n;
            }

            n++;
        }
        if(isCorrect)
        {
            RemoveOrder(n);
        }
        else
        {
            if(simOrder < 0)
            {
                int rand = Random.Range(0, bm.currentOrders.Count);
                RemoveOrder(rand);
            }
            else
            {
                GameController.Instance.orders[simOrder].scores[1].CountStuff(createdDrink,GameController.Instance.orders[simOrder].myDrink,true);
                GameController.Instance.orders[simOrder].scores[1].BrewGameDistBonus(drinkAmount.value, stopPoints[(int)createdDrink.size]);

                prevSim = 0;
                simOrder = 0;
                n = 0;
                foreach(Order o in orders)
                {
                    if (o.myDrink.size == createdDrink.size) similarities++;
                    if (o.myDrink.isCold == createdDrink.isCold) similarities++;
                    if (o.myDrink.flavor == createdDrink.flavor) similarities++;

                    if (prevSim < similarities)
                    {
                        prevSim = similarities;
                        simOrder = n;
                    }
                    n++;
                }
                
                RemoveOrder(simOrder);
            }
        }
    }

    // similarities 
    // prev sim
    // sim order
    // remove function

    void RemoveOrder(int num)
    {
        Debug.Log("Removing..." + num);
        bm.currentOrders[num].SetActive(false);
        orders.Remove(orders[num]);
    }

    public void Trash()
    {
        drinkAmount.value = 0;
        sliderImg.color = Color.white;
/*        Destroy(bm.currentDrinks[currentCup.drinkNum]);
*/    }
}
