using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashManager : MonoBehaviour // should be trashSpawner but its too late now -Geneva
{
    float timer, maxTime;
    public GameObject plastic, compost, paper;

    [SerializeField]
    Vector3 spawnPos;

    GameController gc;
    int currentTable;
    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameController>();
        gc.currentOrder = 0; // setting back to 0 everytime we start a new minigame!!
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTable < gc.orders.Count)
        {
            if (timer >= maxTime)
            {
                SpawnTrash();
            }
            timer += Time.deltaTime;
        }
    }

    public void SpawnTrash()
    {
        int trashType = Random.Range(0, 3);
        switch (trashType)
        {
            case (0):
                Instantiate(plastic, spawnPos, Quaternion.identity);
                break;
            case (1):
                Instantiate(compost, spawnPos, Quaternion.identity);
                break;
            case (2):
                Instantiate(paper, spawnPos, Quaternion.identity);
                break;
        }

        maxTime = Random.Range(2f, 5f);
        gc.currentOrder++;
        timer = 0;
    }
}
