using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    // order game - how many are correct? +time bonus based on time remaining
    // brew game - how close to the line were you? + is order correct
    // lid game - is it the right lid? are you on top of the cup or nah?


    public int correct, incorrect, score, scoreMultiplier = 1, bonus, totalBonus;

    public static ScoreHandler sc;

    // Start is called before the first frame update
    void Start()
    {
        if (sc == null)
            sc = this;
        else
          if (this != sc) Destroy(gameObject);
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


                //10% 25% 50%
    }

    public void AddDistanceBonus(float pos, float yourPos) // for cup line and lid placement. lid sends x, cup sends y
    {

    }

    public int GetScore()
    {
        return (correct * scoreMultiplier) + totalBonus;
    }

    public void AddTotalScore()
    {

    }

    // each aspect that's correct/incorrect is counted. bonus if everything's correct
}

[System.Serializable]
public class Score
{

}