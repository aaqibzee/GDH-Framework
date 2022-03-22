using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameViewManager : MonoBehaviour
{

    #region Declarations
    // Dialogue Screens
    public CanvasGroup pauseDialogue;
    public CanvasGroup levelCompleteDialogue;
    public CanvasGroup gameCompleteDialogue;
    public CanvasGroup gameOverDialogue;
    public CanvasGroup exitDialogue;
    // HUD Object
    public CanvasGroup HUD;
    public Text timeText;
    public Text instructionText;
    public Text coinsAmountText;
    public GameController gameController;
    #endregion

    #region Constants
    private string updateCoinsMethodName= "UpdateCoinsAmount";
    #endregion
    /// <summary>
    /// To initialize 
    /// </summary>
    void Start()
    {
        TurnOffDialogueScreens();
        TurnOnHUD();
        Invoke(updateCoinsMethodName, 1);
    }
    /// <summary>
    /// To let the user play same level 
    /// </summary>
    public void Retry()
    {
        gameController.ReplayLevel();
    }
    /// <summary>
    /// To load next level 
    /// </summary>
    public void PlayNextLevel()
    {
        gameController.PlayNextLevel();
    }
    /// <summary>
    /// To pause the game when user selects from the UI 
    /// </summary>
    public void GamePauseFunction()
    {
        ToggleDialogue(true, new CanvasGroup[] { pauseDialogue });
        AudioManager.instance.PlayGamePauseMusic();
        GameManager.PauseTimer();
        TimeManager.instance.TimeScaleChange(0f);
    }
    /// <summary>
    /// To resume the game after it was was 
    /// </summary>
    public void ResumeGame()
    {
        AudioManager.instance.PlayGamePlayMusic();
        ToggleDialogue(false, new CanvasGroup[] { pauseDialogue });
        TimeManager.instance.TimeScaleChange(1f);
        GameManager.ResumeTimer();
    }
    /// <summary>
    /// To show user game over screen 
    /// </summary>
    public void ShowGameOverScreen()
    {
        ToggleDialogue(true, new CanvasGroup[] { gameOverDialogue });
    }
    /// <summary>
    /// To show user game complete screen
    /// </summary>
    public void ShowGameCompleteScreen()
    {
        ToggleDialogue(true, new CanvasGroup[] { gameCompleteDialogue });

    }
    /// <summary>
    /// To show user level complete screen
    /// </summary>
    public void ShowLevelCompleteScreen()
    {
        ToggleDialogue(true, new CanvasGroup[] { levelCompleteDialogue });
    }
    /// <summary>
    /// To handle case when user decides to cancel the game 
    /// </summary>
    public void GameExitFunction()
    {
        ToggleDialogue(true, new CanvasGroup[] { exitDialogue });
    }
    /// <summary>
    /// To hande case when user confirms game exit 
    /// </summary>
    public void DoGameExit()
    {
        //TODO 
        //clear data
        //apllication.loadlevel
    }
    /// <summary>
    /// To handle case, when user decides not to cancel the game
    /// </summary>
    public void CancelGameExit()
    {
        //TODO 
        // To be implemented 
    }
    /// <summary>
    /// To turn on HUD
    /// </summary>
    public void TurnOnHUD()
    {
        ToggleDialogue(true, new CanvasGroup[] { HUD });
    }
    /// <summary>
    /// To turn off all the UI dialogues, so that they are not active in game play
    /// </summary>
    public void TurnOffDialogueScreens()
    {
        ToggleDialogue(false, new CanvasGroup[] { pauseDialogue, levelCompleteDialogue, gameOverDialogue, exitDialogue });
    }
    /// <summary>
    /// To turn of or off any UI dialogue
    /// </summary>
    /// <param name="flag"></param>
    /// <param name="dialogue"></param>
    public void ToggleDialogue(bool flag, CanvasGroup[] dialogue)
    {
        for (int dialogueCounter = 0; dialogueCounter < dialogue.Length; dialogueCounter++)
        {
            dialogue[dialogueCounter].gameObject.SetActive(flag);

            if (flag)
            {
                dialogue[dialogueCounter].alpha = 1;
            }
            else
            {
                dialogue[dialogueCounter].alpha = 0;
            }
            dialogue[dialogueCounter].interactable = flag;
            dialogue[dialogueCounter].blocksRaycasts = flag;
        }
    }
    /// <summary>
    /// To load Main Menue scene 
    /// </summary>
    public void MainMenuSceneLoad()
    {
        TimeManager.instance.TimeScaleChange(1f);
        gameController.TurnOffLevelData();
        SceneManager.LoadScene(DataConstants.mainMainuSceneName);
    }
    /// <summary>
    /// To load level selection scene
    /// </summary>
    public void LevelSelectionsSceneLoad()
    {
        TimeManager.instance.TimeScaleChange(1f);
        gameController.TurnOffLevelData();
        SceneManager.LoadScene(DataConstants.levelSelectionSceneName);
    }
    /// <summary>
    /// To update task instruction on game play screen
    /// </summary>
    /// <param name="str"></param>
    public void UpdatePlayerInstructions(string str)
    {
        instructionText.text = str;
    }
    /// <summary>
    /// To update level time on screen
    /// </summary>
    /// <param name="minute"></param>
    /// <param name="seconds"></param>
    public void UpdateTimeOnScreen(int minute, int seconds)
    {
        timeText.text = minute + ":" + seconds;
    }
    /// <summary>
    /// To update coins amount on screen
    /// </summary>
    public void UpdateCoinsAmountOnScreen()
    {
        coinsAmountText.text = PlayerPrefsManager.GetCurrentLevelCoins().ToString();
    }
}