using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    // order game - how many are correct? +time bonus based on time remaining
    // brew game - how close to the line were you? + is order correct
    // lid game - is it the right lid? are you on top of the cup or nah?


    //public int[] scorebonuses; // the number that gets added if everything's correct or etc
    public int totalScore;
    public int scoreMultiplier = 5;
    public int[] orderTimeBonus;
    public int[] brewTimeBonus;
    public int[] brewDistanceBonus;
    public int[] lidDistanceBonus;
    public int allCorrectBonus;

    public static ScoreHandler sc;

    // Start is called before the first frame update
    void Start()
    {
        if (sc == null)
            sc = this;
        else
          if (this != sc) Destroy(gameObject);
    }

    public void CalculateTotalScore()
    {
        foreach(Order o in GameController.Instance.orders)
        {
            foreach (Score s in o.scores)
                totalScore += s.GetScore();
        }
    }

    public int GetTotalCorrect()
    {
        int c= 0;

        foreach (Order o in GameController.Instance.orders)
        {
            foreach (Score s in o.scores)
                c += s.correct;
        }

        return c;
    }


    public int GetTotalInCorrect()
    {
        int c = 0;

        foreach (Order o in GameController.Instance.orders)
        {
            foreach (Score s in o.scores)
                c += s.incorrect;
        }

        return c;
    }

    // each aspect that's correct/incorrect is counted. bonus if everything's correct
}

[System.Serializable]
public class Score
{
    public int correct, incorrect, myBonus;


    public void CountStuff(Drink myDrink, Drink correctDrink, bool isBrewGame = false) // counting corrects and incorrects
    {
        if (isBrewGame)
        {
            if (myDrink.isCold == correctDrink.isCold) CorrectMove(false);
            else IncorrectMove(false);

            if (myDrink.flavor == correctDrink.flavor) CorrectMove(false);
            else IncorrectMove(false);

            if (myDrink.size == correctDrink.size) CorrectMove(false);
            else IncorrectMove(false);

            if (correct == 3)
            {
                myBonus += ScoreHandler.sc.allCorrectBonus;
                FMODUnity.RuntimeManager.PlayOneShot("event:/CorrectDing");
            }
            else
                FMODUnity.RuntimeManager.PlayOneShot("event:/InorrectDing");

        }
        else
        {
            if (myDrink.isCold == correctDrink.isCold) CorrectMove(false);
            else IncorrectMove(false);

            if (myDrink.flavor == correctDrink.flavor) CorrectMove(false);
            else IncorrectMove(false);

            if (myDrink.size == correctDrink.size) CorrectMove(false);
            else IncorrectMove(false);

            if (myDrink.drinkType == correctDrink.drinkType) CorrectMove(false);
            else IncorrectMove(false);

            if (correct == 4)
            {
                myBonus += ScoreHandler.sc.allCorrectBonus;
                FMODUnity.RuntimeManager.PlayOneShot("event:/CorrectDing");
            }
            else
                FMODUnity.RuntimeManager.PlayOneShot("event:/InorrectDing");
        }

    }

    public void CorrectMove(bool isLidGame = true)
    {
        if(isLidGame)
        FMODUnity.RuntimeManager.PlayOneShot("event:/CorrectDing");
        correct++;
    }

    public void IncorrectMove(bool isOrderingGame = false)
    {
        if (!isOrderingGame)
            FMODUnity.RuntimeManager.PlayOneShot("event:/IncorrectDing");
        incorrect++;

    }
    
    public void BrewGameTimeBonus(float m, float y)
    {
        float timeLeft = m - y;
        Mathf.Abs(timeLeft);
        float timePercent = (timeLeft * 100f) / y;

        if (timePercent > 35) 
            AddBonus(ScoreHandler.sc.brewTimeBonus[3]); // temporarily adding the same score until it gets changed!
        if (timePercent > 20 && timePercent <= 35)
            AddBonus(ScoreHandler.sc.brewTimeBonus[2]); // temporarily adding the same score until it gets changed!
        if (timePercent > 8 && timePercent <= 20)
            AddBonus(ScoreHandler.sc.brewTimeBonus[1]); // temporarily adding the same score until it gets changed!
        if (timePercent < 8 && timePercent > 0)
            AddBonus(ScoreHandler.sc.brewTimeBonus[0]); // temporarily adding the same score until it gets changed!
    }

    public void OrderGameBonus(float m, float y)
    {
        float timeLeft = m - y;
        Mathf.Abs(timeLeft);
        float timePercent = (timeLeft * 100f) / y;

        if (timePercent > 50)
            AddBonus(ScoreHandler.sc.orderTimeBonus[3]); // temporarily adding the same score until it gets changed!
        if (timePercent > 25 && timePercent <= 50)
            AddBonus(ScoreHandler.sc.orderTimeBonus[2]); // temporarily adding the same score until it gets changed!
        if (timePercent > 10 && timePercent <= 25)
            AddBonus(ScoreHandler.sc.orderTimeBonus[1]); // temporarily adding the same score until it gets changed!
        if (timePercent < 10 && timePercent > 0)
            AddBonus(ScoreHandler.sc.orderTimeBonus[0]); // temporarily adding the same score until it gets changed!
    }

    public void LidGameBonus(float p, float y)
    {
        float timeLeft = p - y;
        Mathf.Abs(timeLeft);
        Debug.Log(timeLeft);

        if (timeLeft > 0.35 && timeLeft <= 0.5)
            AddBonus(ScoreHandler.sc.lidDistanceBonus[0]); // temporarily adding the same score until it gets changed!
        if (timeLeft > 0.20 && timeLeft <= 0.35)
            AddBonus(ScoreHandler.sc.lidDistanceBonus[1]); // temporarily adding the same score until it gets changed!
        if (timeLeft > 0.1 && timeLeft <= 0.20)
            AddBonus(ScoreHandler.sc.lidDistanceBonus[2]); // temporarily adding the same score until it gets changed!
        if (timeLeft <= 0.1)
            AddBonus(ScoreHandler.sc.lidDistanceBonus[3]); // temporarily adding the same score until it gets changed!
    }

    public void BrewGameDistBonus(float p, float y)
    {
        float timeLeft = p - y;
        Mathf.Abs(timeLeft);
        Debug.Log(timeLeft);

        if (timeLeft > 0.2 && timeLeft <= 0.3)
            AddBonus(ScoreHandler.sc.brewDistanceBonus[0]); // temporarily adding the same score until it gets changed!
        if (timeLeft > 0.1 && timeLeft <= 0.2)
            AddBonus(ScoreHandler.sc.brewDistanceBonus[1]); // temporarily adding the same score until it gets changed!
        if (timeLeft < 0.05 && timeLeft > 0.1)
            AddBonus(ScoreHandler.sc.brewDistanceBonus[2]);
        if (timeLeft <= 0.05)
            AddBonus(ScoreHandler.sc.brewDistanceBonus[3]);// temporarily adding the same score until it gets changed!
    }

    void AddBonus(int n)
    {
        myBonus += n;
    }

    public int GetScore()
    {
        return (correct * ScoreHandler.sc.scoreMultiplier) + myBonus;
    }


    /*  void AddDistanceBonus(float pos, float yourPos) // for cup line and lid placement. lid sends x, cup sends y
      {
          float dist = Mathf.Abs(pos - yourPos);
          float distPercent = (dist * 100f) / pos;

          if (distPercent > 50) myBonus += ScoreHandler.sc.scorebonuses[7];
          if (distPercent > 25 && distPercent <= 50) myBonus += ScoreHandler.sc.scorebonuses[6];
          if (distPercent > 10 && distPercent <= 25) myBonus += ScoreHandler.sc.scorebonuses[5];
          if (distPercent < 10) myBonus += ScoreHandler.sc.scorebonuses[4];
      }*/



}