using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField] GameObject[] HighScorePos;

    [SerializeField] string filename;

    List<InputEntry> HighScoreList;
    // Start is called before the first frame update
    void Start()
    {
        //locks mouse to scene
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        //get data from JSON file
        HighScoreList = FileHandler.ReadListFromJSON<InputEntry>(filename);

        //Put data into an array and sort from highest to lowest
        HighScoreList = HighScoreList.OrderBy(o => o.playerScore).ToList();

        //reverses list order to become highest to lowest playerScore
        HighScoreList.Reverse();

        //display the names and score of each player in High Score menu
        for(int i = 0; i < HighScorePos.Length; i++)
        {
            if (HighScoreList.ElementAtOrDefault(i) != null)
            {
                HighScorePos[i].GetComponent<TextMeshProUGUI>().text = HighScoreList[i].playerName + ": " + HighScoreList[i].playerScore;
            }
            else
            {
                HighScorePos[i].gameObject.SetActive(false);
            }
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
