using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public Text timerText;
    public float gameTime = 180f;
    public int totalObjects = 15;
    private int collectedObjects = 0;
    private bool gameEnded = false;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject tutorialPanel1;
    public GameObject tutorialPanel2;
    public GameObject tutorialPanel3;
    public GameObject pausePanel;
    public GameObject returnToMenuPanel;
    public GameObject settingsPanel;
    public GameObject createPanel;
    public GameObject musicObject;
    public GameObject collectsoundObject;
    private bool gameStarted = false;

        
    public Text collectText;

    public static string username = "";

    void Start()
    {
        UpdateTimerText();
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        tutorialPanel1.SetActive(true);
        tutorialPanel2.SetActive(false);
        tutorialPanel3.SetActive(false);
        pausePanel.SetActive(false);
        Time.timeScale = 0f;

        
        if (collectText != null)
        {
            collectText.text = "0/" + totalObjects;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;

        musicObject.SetActive(true);

    }

    

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Start();
    }

    void Update()
    {
        if (gameStarted && !gameEnded)
        {
            gameTime -= Time.deltaTime;
            UpdateTimerText();

            if (gameTime <= 0f)
            {
                EndGame(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameEnded)
            {
                PauseGame();
            }
        }

        if (winPanel.activeSelf || losePanel.activeSelf || tutorialPanel1.activeSelf ||
        tutorialPanel2.activeSelf || tutorialPanel3.activeSelf || pausePanel.activeSelf ||
        returnToMenuPanel.activeSelf || settingsPanel.activeSelf)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }

    }

    void UpdateTimerText()
    {
        timerText.text = "" + Mathf.RoundToInt(gameTime).ToString();
    }

    void UpdateCollectText()
    {
        collectText.text = "" + collectedObjects.ToString() + "/" + totalObjects;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collect"))
        {
            Debug.Log("Perfect");
            collectsoundObject.SetActive(true);
            StartCoroutine(DeactivateCollectSound());
            collectedObjects++;
            Destroy(other.gameObject);
            UpdateCollectText();

            


            if (collectedObjects >= totalObjects)
            {
                EndGame(true);
            }
        }

        else if (other.gameObject.CompareTag("Engel"))
        {
            EndGame(false);

        }

        else if (other.gameObject.CompareTag("Bitis"))
        {
            EndGame(true);
        }
    }

    IEnumerator DeactivateCollectSound()
    {
        yield return new WaitForSeconds(0.5f);
        collectsoundObject.SetActive(false);
    }

    void EndGame(bool won)
    {
        gameEnded = true;
        if (won)
        {
            winPanel.SetActive(true);
            Debug.Log("You win!");
        }
        else
        {
            losePanel.SetActive(true);
            Debug.Log("You lose!");
        }
    }

    public void NextTutorial()
    {
        tutorialPanel1.SetActive(false);
        tutorialPanel2.SetActive(true);
    }

    public void FinalTutorial()
    {
        tutorialPanel2.SetActive(false);
        tutorialPanel3.SetActive(true);
    }

    public void StartGameFromTutorial()
    {
        tutorialPanel3.SetActive(false);
        Time.timeScale = 1f;
        StartGame();
    }
    

    public void StartGame()
    {
        tutorialPanel1.SetActive(false);
        tutorialPanel2.SetActive(false);
        tutorialPanel3.SetActive(false);
        Time.timeScale = 1f;
        gameStarted = true;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);
        Time.timeScale = 0f; // Oyunu duraklat
    }

    public void SettingsToPauseManuPanel()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void OpenReturnToMenuPanel()
    {
        returnToMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ConfirmReturnToMenu(bool confirm)
    {
        if (confirm)
        {
            MainMenu();
        }
        else
        {
            Decline();
        }
        
    }

    public void ToggleObject()
    {
        musicObject.SetActive(!musicObject.activeSelf);
    }

    public void ToggleAllSound()
    {
        AudioListener.pause = !AudioListener.pause;
    }


    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Decline()
    {
        returnToMenuPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void OpenCreateMenu()
    {
        createPanel.SetActive(true);
    }
    public void BackToMainMenu()
    {
        createPanel.SetActive(false);
    }

    public void OpenFirstLevet()
    {
        SceneManager.LoadScene("GameBir");
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("GameIki");
    }

    public void ShowNextTutorialPanel()
    {
        if (tutorialPanel1.activeSelf)
        {
            tutorialPanel1.SetActive(false);
            tutorialPanel2.SetActive(true);
        }
        else if (tutorialPanel2.activeSelf)
        {
            tutorialPanel2.SetActive(false);
            tutorialPanel3.SetActive(true);
        }
        else if (tutorialPanel3.activeSelf)
        {
            tutorialPanel3.SetActive(false);
            StartGame();
        }
    }

    
    public void QuitGame()
    {
        Application.Quit();
    }
}
