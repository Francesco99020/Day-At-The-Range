using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    List<InputEntry> entries = new List<InputEntry>();

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI GameOverMenu;
    [SerializeField] TextMeshProUGUI MainMenu;
    [SerializeField] Image HowToPlayMenu;
    [SerializeField] TextMeshProUGUI PlayerUI;
    [SerializeField] TextMeshProUGUI NameDisplay;

    [SerializeField] Button RestartBtn;
    [SerializeField] Button StartBtn;
    [SerializeField] Button HowToPlayBtn;
    [SerializeField] Button BackBtn;
    [SerializeField] Button ExitBtn;

    [SerializeField] string filename;

    [SerializeField] GameObject Camera;
    [SerializeField] GameObject TargetHolder;

    bool gameInProgress;
    bool nameSelected = false;
    bool gameOverHasRan = false;
    int score;
    float timer;
    private string userInput;

    // Start is called before the first frame update
    void Start()
    {
        entries = FileHandler.ReadListFromJSON<InputEntry>(filename);

        gameInProgress = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Camera.GetComponent<CameraController>().enabled = false;
        Camera.GetComponent<ProjectileManager>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0 || !gameInProgress)
        {
            timer -= Time.deltaTime;
            UpdateTimer(timer);
        }
        else
        {
            if (!gameOverHasRan)
            {
                GameOver();
                gameOverHasRan = true;
            }
        }
    }

    private void GameOver()
    {
        GameOverMenu.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Camera.GetComponent<CameraController>().enabled = false;
        Camera.GetComponent<ProjectileManager>().enabled = false;
        AddUserToList(userInput, score);
        FileHandler.SaveToJSON<InputEntry>(entries, filename);
    }

    public void UpdateScore(int scoreInc)
    {
        score += scoreInc;
        scoreText.text = "Score: " + score;
    }

    void UpdateTimer(float time)
    {
        timerText.text = "Time: " + Mathf.Round(time);
    }

    public void StartGame()
    {
        if (!nameSelected)
        {
            NameDisplay.text = "Please Enter a Name";
            NameDisplay.gameObject.SetActive(true);
        }
        else
        {
            //Hide main menu and turn on Player UI
            MainMenu.gameObject.SetActive(false);
            PlayerUI.gameObject.SetActive(true);


            //hide and lock cursor to center of screen
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            //Activate Targets
            TargetHolder.SetActive(true);

            //Activates Camera and Shooting control
            Camera.GetComponent<CameraController>().enabled = true;
            Camera.GetComponent<ProjectileManager>().enabled = true;

            //Set up score and timer
            score = 0;
            timer = 60;
            UpdateScore(score);
            gameInProgress = true;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void HowToPlay()
    {
        HowToPlayMenu.gameObject.SetActive(true);
        BackBtn.gameObject.SetActive(true);
    }

    public void Back()
    {
        HowToPlayMenu.gameObject.SetActive(false);
        BackBtn.gameObject.SetActive(false);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void HighScoreMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void ReadStringInput(string input)
    {
        userInput = input;
        MainManager.instance.playerName = userInput;
        SetGreedingText();
    }

    private void SetGreedingText()
    {
        if(MainManager.instance.playerName == "" || userInput == "")
        {
            NameDisplay.gameObject.SetActive(false);
        }
        else
        {
            NameDisplay.text = "Hello " + MainManager.instance.playerName;
            NameDisplay.gameObject.SetActive(true);
            nameSelected = true;
        }
    }

    public void AddUserToList(string name, int score)
    {
        entries.Add(new InputEntry(name, score));
    }
}
