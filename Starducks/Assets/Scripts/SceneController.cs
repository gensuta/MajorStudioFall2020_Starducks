using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public int currentScene;

    // The below variables are used if we're loading the next scene based on a timer
    public float timer;
    public bool timerIsOn;
    public string storedName;

    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.5f;
    }


    void Update()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;

 
        // cheats >v>
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                BackAScene();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ForwardAScene();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetScene();
            }
        }



        if (timerIsOn)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {

                LoadScene(storedName);
                timerIsOn = false;
            }
        }
    }

    public bool IsSceneCalled(string s) // is my current scene called "s"?
    {
        return SceneManager.GetActiveScene().name == s;
    }

    public void BackAScene() //when you wanna go back one scene
    {
        if (currentScene != 0)
        {
            currentScene -= 1;
            LoadScene(currentScene);
        }
    }
    public void ForwardAScene() //when you wanna go forward one scene
    {
        if (currentScene != SceneManager.sceneCountInBuildSettings - 1)
        {
            currentScene += 1;
            LoadScene(currentScene);
        }
    }

    public void ResetScene()
    {
        LoadScene(currentScene);

    }

    public void LoadAnotherScene(string s) // for when you want to load another scene into your current scene
    {
        SceneManager.LoadScene(s, LoadSceneMode.Additive);
    }
    public void UnloadScene(string s) // for when you want to unload a scene ( when you have multiple scenes running at once )
    {
        SceneManager.UnloadSceneAsync(s);
    }

    public void LoadScene(int num)
    {
        SceneManager.LoadScene(num);
    }

    public void LoadScene(string sName)
    {
        if(!IsSceneCalled("StartScreen"))GameController.Instance.maxOrders = GameController.Instance.orders.Count;
        StudioEventEmitter em = GetComponent<StudioEventEmitter>();

        if (IsSceneCalled("OrderGame")) em.SetParameter("isInOrderGame", 1f);
        else em.SetParameter("isInOrderGame", 0f);


        SceneManager.LoadScene(sName);
    }

    public void WaitThenLoad(string sName, float time)
    {
        timerIsOn = true;
        timer = time;
        storedName = sName;
    }

}
