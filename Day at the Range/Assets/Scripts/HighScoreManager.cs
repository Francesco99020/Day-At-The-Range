using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField] public GameObject[] HighScorePos;

    public new string name;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        HighScorePos[0] = GameObject.Find("First place");
        HighScorePos[1] = GameObject.Find("Second place");
        HighScorePos[2] = GameObject.Find("Third place");
        HighScorePos[3] = GameObject.Find("Fourth place");
        HighScorePos[4] = GameObject.Find("Fith place");
        HighScorePos[5] = GameObject.Find("Sixth place");
        HighScorePos[6] = GameObject.Find("Seventh place");
        HighScorePos[7] = GameObject.Find("Eighth place");
        HighScorePos[8] = GameObject.Find("Ninth place");
        HighScorePos[9] = GameObject.Find("Tenth place");

        //locks mouse to scene
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        //get data from JSON file
        MainManager.instance.LoadScore();
        MainManager.instance.LoadName();

        if(MainManager.instance != null)
        {
            name = MainManager.instance.playerName;
            score = MainManager.instance.playerScore;
        }
        //Put data into an array and sort from highest to lowest

        //drop all but the first 9 indexs from arrray

        //display the names and score of each player in High Score menu
        HighScorePos[0].GetComponent<TextMeshPro>().text = name + ": " + score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
