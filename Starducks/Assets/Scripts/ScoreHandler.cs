using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    // order game - how many are correct? +time bonus based on time remaining
    // brew game - how close to the line were you? + is order correct
    // lid game - is it the right lid? are you on top of the cup or nah?


    public int[] scorebonuses; // the number that gets added if everything's correct or etc
    public int totalScore;
    public int scoreMultiplier = 1;

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
            if (myDrink.isCold == correctDrink.isCold) CorrectMove();
            else IncorrectMove();

            if (myDrink.flavor == correctDrink.flavor) CorrectMove();
            else IncorrectMove();

            if (myDrink.size == correctDrink.size) CorrectMove();
            else IncorrectMove();

            if (correct == 3) myBonus += ScoreHandler.sc.scorebonuses[2];
        }
        else
        {
            if (myDrink.isCold == correctDrink.isCold) CorrectMove();
            else IncorrectMove();

            if (myDrink.flavor == correctDrink.flavor) CorrectMove();
            else IncorrectMove();

            if (myDrink.size == correctDrink.size) CorrectMove();
            else IncorrectMove();

            if (myDrink.drinkType == correctDrink.drinkType) CorrectMove();
            else IncorrectMove();

            if (correct == 4) myBonus += ScoreHandler.sc.scorebonuses[2];
        }

    }

    public void CorrectMove()
    {
        Debug.Log("correct!");
        correct++;
    }

    public void IncorrectMove()
    {
        Debug.Log("incorrect!!");
        incorrect++;

    }
    public void AddTimeBonus(float maxTime, float yourTime)
    {
        float timeLeft = maxTime - yourTime;
        float timePercent = (timeLeft * 100f) / maxTime;

        if (timePercent > 50) myBonus += ScoreHandler.sc.scorebonuses[3];
        if (timePercent > 25 && timePercent <= 50) myBonus += ScoreHandler.sc.scorebonuses[2];
        if (timePercent > 10 && timePercent <= 25) myBonus += ScoreHandler.sc.scorebonuses[1];
        if (timePercent < 10) myBonus += ScoreHandler.sc.scorebonuses[0];

        //10% 25% 50%
    }

    public void AddDistanceBonus(float pos, float yourPos) // for cup line and lid placement. lid sends x, cup sends y
    {
        float dist = Mathf.Abs(pos - yourPos);
        float distPercent = (dist * 100f) / pos;

        if (distPercent > 50) myBonus += ScoreHandler.sc.scorebonuses[3];
        if (distPercent > 25 && distPercent <= 50) myBonus += ScoreHandler.sc.scorebonuses[2];
        if (distPercent > 10 && distPercent <= 25) myBonus += ScoreHandler.sc.scorebonuses[1];
        if (distPercent < 10) myBonus += ScoreHandler.sc.scorebonuses[0];
    }

    public int GetScore()
    {
        return (correct * ScoreHandler.sc.scoreMultiplier) + myBonus;
    }


}