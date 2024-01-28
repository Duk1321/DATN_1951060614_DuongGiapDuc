using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameState
    {
        Gameplay,
        Pause,
        GameOver,
        LevelUp
    }

    public GameState currentState;

    public GameState previousState;

    [Header("Damage Text")]
    public Canvas damageTextCanvas;
    public float textFontSize = 20;
    public TMP_FontAsset textFont;
    public Camera referenceCamera;

    [Header("Screen")]
    public GameObject pauseScreen;
    public GameObject resultScreen;
    public GameObject levelUpScreen;


    [Header("Curren Stat Display")]
    public TMPro.TMP_Text currentHealthDisplay;
    public TMPro.TMP_Text currentRecoveryDisplay;
    public TMPro.TMP_Text currentMoveSpeedDisplay;
    public TMPro.TMP_Text currentMightDisplay;
    public TMPro.TMP_Text currentProjectileSpeedDisplay;
    public TMPro.TMP_Text currentMagnetDisplay;

    [Header("Results Screen Displays")]
    public Image chosenCharacterImage;
    public TMPro.TMP_Text chosenCharacterName;
    public TMPro.TMP_Text levelReachedDisplay;
    public TMPro.TMP_Text timeSurviveDisplay;
    public List<Image> chosenWeaponsUI = new List<Image>(6);
    public List<Image> chosenPassiveUI = new List<Image>(6);

    [Header("Stopwatch")]
    public float timeLimit; //limit in the second
    float stopwatchTime; // how many laps have pass the timeLimit
    public TMPro.TMP_Text stopwatchDisplay;

    // check game over or not
    public bool isGameOver = false;

    //ref to player's game object
    public GameObject playerObject;
    
    // check chosing upgrade when leveling up or not
    public bool choosingUpgrade = false;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("EXTRA" + this + "DELETE");
        }
        DisableScreen();
    }

    void Update()
    {
        switch (currentState)
        {
            case GameState.Gameplay:
                CheckForPauseAndResume();
                UpdateStopWatch();
                break;

            case GameState.Pause:
                CheckForPauseAndResume();
                break;

            case GameState.GameOver:
                if (!isGameOver)
                {
                    isGameOver = true;
                    Time.timeScale = 0f;
                    Debug.Log("Game is over");
                    DisplayResults();
                }
                break;

            case GameState.LevelUp:
                if(!choosingUpgrade)
                {
                    choosingUpgrade = true;
                    Time.timeScale = 0f;
                    Debug.Log("Upgrades shown");
                    levelUpScreen.SetActive(true);
                }
                break;
            
            default:
                Debug.LogWarning("state doesnt exist");
                break;
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }

    public void PauseGame()
    {
        if(currentState != GameState.Pause)
        {
            previousState = currentState;
            ChangeState(GameState.Pause);
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
            Debug.Log("Game is paused");
        }
    }

    public void ResumeGame()
    {
        if(currentState == GameState.Pause)
        {
            ChangeState(previousState);
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
            Debug.Log("Game is resumed");
        }
    }

    void CheckForPauseAndResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(currentState == GameState.Pause)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void DisableScreen()
    {
        pauseScreen.SetActive(false);
        resultScreen.SetActive(false);
        levelUpScreen.SetActive(false); 
    }

    public void GameOver()
    {
        timeSurviveDisplay.text = stopwatchDisplay.text;
        ChangeState(GameState.GameOver);
    }

    void DisplayResults()
    {
        resultScreen.SetActive(true);
    }

    public void AssignChosenCharacterUI(CharacterScriptableObject chosenCharacterData)
    {
        chosenCharacterImage.sprite = chosenCharacterData.Icon;
        chosenCharacterName.text = chosenCharacterData.name;
    }

    public void AssignLevelReaced(int levelReachedData)
    {
        Debug.Log("Level run");
        levelReachedDisplay.text = levelReachedData.ToString();
    }
    IEnumerator GenerateFloatingTextCoroutine(string text, Transform target, float duration = 1f, float speed = 50f)
    {
        // Create gameobject damage text floating
        GameObject textObj = new GameObject("Damage Floating Text");
        RectTransform rect = textObj.AddComponent<RectTransform>();
        TextMeshProUGUI tmPro = textObj.AddComponent<TextMeshProUGUI>();
        tmPro.text = text;
        tmPro.horizontalAlignment = HorizontalAlignmentOptions.Center;
        tmPro.verticalAlignment = VerticalAlignmentOptions.Middle;
        tmPro.fontSize = textFontSize;
        if (textFont) tmPro.font = textFont;
        // end

        //set position for rect
        rect.position = referenceCamera.WorldToScreenPoint(target.position);

        //destroy after sometime
        Destroy(textObj, duration);

        //spawn it in destination canvas
        textObj.transform.SetParent(instance.damageTextCanvas.transform);
        textObj.transform.SetAsFirstSibling();

        
        WaitForEndOfFrame w = new WaitForEndOfFrame();
        float t = 0;
        float yOffset = 0;
        while (t < duration)
        {
            tmPro.color = new Color(tmPro.color.r, tmPro.color.g, tmPro.color.b, 1 - t / duration);
            
            if (target)
            {
                yOffset += speed * Time.deltaTime;
                rect.position = referenceCamera.WorldToScreenPoint(target.position + new Vector3(0, yOffset));
            }
            else
            {
                rect.position += new Vector3(0, speed * Time.deltaTime, 0);
            }

            yield return w;
            t += Time.deltaTime;
        }
    }

    public static void GenerateFloatingText(string text, Transform target, float duration = 1f, float speed = 1f)
    {
        if (!instance.damageTextCanvas) return;

        if (!instance.referenceCamera) instance.referenceCamera = Camera.main;

        instance.StartCoroutine(instance.GenerateFloatingTextCoroutine(
            text, target, duration, speed
            ));
    }


    public void AssignChosenWeaponsAndPassiveUI(List<Image> chosenWeaponsData, List<Image> chosenPassiveData)
    {
        if(chosenWeaponsData.Count != chosenWeaponsUI.Count || chosenPassiveData.Count != chosenPassiveUI.Count)
        {
            Debug.Log("Chosen weapon and passive items data list have dif lengths");
            return;
        }

        for(int i =  0; i < chosenWeaponsUI.Count; i++)
        {
            if (chosenWeaponsData[i].sprite)
            {
                chosenWeaponsUI[i].enabled = true;
                chosenWeaponsUI[i].sprite = chosenWeaponsData[i].sprite;
            }
            else
            {
                chosenWeaponsUI[i].enabled = false;
            }
        }

        for (int i = 0; i < chosenPassiveUI.Count; i++)
        {
            if (chosenPassiveData[i].sprite)
            {
                chosenPassiveUI[i].enabled = true;
                chosenPassiveUI[i].sprite = chosenPassiveData[i].sprite;
            }
            else
            {
                chosenPassiveUI[i].enabled = false;
            }
        }
    }

    void UpdateStopWatch()
    {
        stopwatchTime += Time.deltaTime;

        UpdateStopWatchDisplay();

        if (stopwatchTime >= timeLimit)
        {
            playerObject.SendMessage("Kill");
        }
    }

    void UpdateStopWatchDisplay()
    {
        int minutes = Mathf.FloorToInt(stopwatchTime / 60);
        int seconds = Mathf.FloorToInt(stopwatchTime % 60);

        stopwatchDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartLevelUp()
    {
        ChangeState(GameState.LevelUp);
        playerObject.SendMessage("RemoveAndApplyUpgrades"); 
    }

    public void EndLevelUp()
    {
        choosingUpgrade = false;
        Time.timeScale = 1f; // resume the game
        levelUpScreen.SetActive(false);
        ChangeState(GameState.Gameplay);
    }
}
