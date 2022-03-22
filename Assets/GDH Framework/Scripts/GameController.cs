using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    #region Declarations
    public GameViewManager gameViewManager;
    [Tooltip("Data for each level")]
    [SerializeField]
    public LevelData[] levels;
    [Space(10)]
    [Tooltip("All players avaialble for user in this game")]
    public PlayerData[] players;
    [Space(10)]
    [Tooltip("Level from where game should be started, this can be helpful for testing")]
    public int startLevel;
    [Space(10)]
    [Tooltip("Number of seconds to wait before announcing game win")]
    public float gameWinDelay;
    [Space(10)]
    [Tooltip("Number of seconds to wait before announcing game lose")]
    public float gameLoseDelay;
    [Tooltip("Time to start warning, that game is about to end")]
    public float warningStartTime = 10f;
    [SerializeField]
    private int currentLevel = 1;// level number would be in real digits
    private int completedTasksCount = 0;
    private int currentLevelIndex; // index to refer current level 
    [SerializeField]
    [Tooltip("Current Player selected by player")]
    private int currentPlayer = 0;
    private int currentLevelTime;
    #endregion

    #region Constants
    private string checkGameLoseStatusMethodName = "CheckGameLoseStatus";
    private string onGameLoseMethodName = "OnGameLose";
    private string onGameWinMethodName = "OnGameWin";
    #endregion

    public void Start()
    {
        //ADManager.HideBanner ();
        //ADManager.HideNativeBanner ("defaultNativeBanner");
        PlayerPrefsManager.SetCurrentLevelCoins(0);
        currentLevel = PlayerPrefsManager.GetLevelSelected();
        #if UNITY_EDITOR
        currentLevel = startLevel;
        #endif
        currentPlayer = PlayerPrefsManager.GetPlayerSelected();
        InitLevel(currentLevel);

    }
    /// <summary>
    /// Initiate the level: Set level data, spawn player, setup audio
    /// </summary>
    /// <param name="levelNo"></param>
    public void InitLevel(int levelNo)
    {
        currentLevelIndex = currentLevel - 1;
        LevelData currentLevelData = levels[currentLevelIndex];
        TimeClass levelTime = currentLevelData.levelTime;
        completedTasksCount = 0;
        //currentPlayer = levels [currentLevelIndex].player;
        currentLevelTime = TimeManager.instance.ToSeconds(levelTime);
        //turn off all the levels first
        for (int levelCounter = 0; levelCounter < levels.Length; levelCounter++)
        {
            if (levelCounter != currentLevelIndex)
            {
                levels[levelCounter].levelData.SetActive(false);
            }
        }
        currentLevelData.levelData.SetActive(true);
        SpawnPlayer(currentPlayer, currentLevelData.spawnPoint);
        ToggleFinishPoint(false);
        gameViewManager.UpdatePlayerInstructions(currentLevelData.tasks[completedTasksCount].instruction);
        gameViewManager.TurnOffDialogueScreens();
        // Here you can add any aditional functionality, like if you want to show some thing in start of the level to user
        TimeManager.instance.SetGameTime(levelTime.minutes, levelTime.seconds);
        AudioManager.instance.PlayGamePlayMusic();
        TimeManager.instance.StartTime();
        InvokeRepeating(checkGameLoseStatusMethodName, 0, 1);
    }
    /// <summary>
    /// To check if player lost the game 
    /// </summary>
    public void CheckGameLoseStatus()
    {
        gameViewManager.UpdateTimeOnScreen(TimeManager.instance.gameTime.minutes, TimeManager.instance.gameTime.seconds);
        if (TimeManager.instance.GetGameTimeInSeconds() <= 0f)
        {
            TimeManager.instance.PauseTime();
            AudioManager.instance.PlayGameLoseMusic();
            Invoke(onGameLoseMethodName, gameLoseDelay);
        }
        //Warn the player that level time is about to end 
        else if (TimeManager.instance.GetGameTimeInSeconds() <= warningStartTime)
        {
            AudioManager.instance.PlayAlarmSound();
        }
    }

    /// <summary>
    /// To spawn the player at a particular position: 
    /// </summary>
    /// <param name="playerIndex"></param>
    /// <param name="spawnPoint"></param>
    void SpawnPlayer(int playerIndex, Transform spawnPoint)
    {
        for (int playerCount = 0; playerCount < players.Length; playerCount++)
        {
            players[playerCount].player.gameObject.SetActive(false);
        }
        GameObject currentPlayer = players[playerIndex].player;
        currentPlayer.gameObject.SetActive(true);
        currentPlayer.transform.position = spawnPoint.position;
        currentPlayer.transform.rotation = spawnPoint.rotation;
    }
    /// <summary>
    /// To switch between player. There could be games with more than one player type: On a side note,
    /// when using this method, make sure you make it alligned with the package you are using
    /// Some time, we can't deactivate the players directly, that can cause problems like health reset, etc. 
    /// Change it as epr your need
    /// </summary>
    /// <param name="playerIndex"></param>
    /// <param name="turnOffCurrentPlayer"></param>
    public void SwitchPlayer(int playerIndex, bool turnOffCurrentPlayer = true)
    {
        for (int playerCount = 0; playerCount < players.Length; playerCount++)
        {
            if (playerCount == playerIndex)
            {
                players[playerCount].player.gameObject.SetActive(true);
                if (turnOffCurrentPlayer)
                {
                    players[currentPlayer].player.gameObject.SetActive(false);
                }
            }
        }
    }

    /// <summary>
    /// To swicth the camera for player, for example 3rd person, or first person or any other. 
    /// </summary>
    /// <param name="player"></param>
    void SwitchCamera(GameObject player)
    {
        //TODO
    }
    /// <summary>
    /// To mar the task as completed 
    /// </summary>
    public void TaskComplete()
    {
        if (completedTasksCount < levels[currentLevel - 1].tasks.Length)
        {
            PlayerPrefsManager.AddCurrentLevelCoins(levels[currentLevel - 1].tasks[completedTasksCount].taskCompleteReward);
            gameViewManager.UpdateCoinsAmountOnScreen();
            completedTasksCount++;
            AudioManager.instance.PlayTaskCompleteSound();
            CheckGameStatus();
        }
    }
    /// <summary>
    /// To check if player has won the game 
    /// </summary>
    void CheckGameStatus()
    {
        if (completedTasksCount == levels[currentLevel - 1].tasks.Length)
        {
            CancelInvoke(checkGameLoseStatusMethodName);
            TimeManager.instance.TimeScaleChange(.5f);
            Invoke(onGameWinMethodName, gameWinDelay);
            AudioManager.instance.PlayGameWinMusic();
        }
        else
        {
            ToggleFinishPoint(false);
            if (completedTasksCount < levels[currentLevelIndex].tasks.Length)
            {
                gameViewManager.UpdatePlayerInstructions(levels[currentLevelIndex].
                    tasks[completedTasksCount].instruction);
            }
        }
        //here you can call to activate and deactivate point on map 
        //DetachMapItem();
    }
    /// <summary>
    /// Use this to do tasks to be done at Game win event.
    /// </summary>
    void OnGameWin()
    {
        //replace this function later with game manager instance
        TimeManager.instance.PauseTime();
        PlayerPrefsManager.AddPlayerCoins(PlayerPrefsManager.GetCurrentLevelCoins());
        TimeManager.instance.TimeScaleChange(0f);
        

        if (currentLevel == levels.Length)
        {
            gameViewManager.ShowGameCompleteScreen();
        }
        else
        {
            gameViewManager.ShowLevelCompleteScreen();
        }
        PlayerPrefsManager.SetLevelsUnlocked(currentLevel + 1);
        //ADManager.Paused ();
    }
    /// <summary>
    /// Use this to do tasks to be done at Game lose event.
    /// </summary>
    public void OnGameLose()
    {  
        CancelInvoke(checkGameLoseStatusMethodName);
        TimeManager.instance.TimeScaleChange(0f);
        TimeManager.instance.PauseTime();
        gameViewManager.ShowGameOverScreen();
        //	ADManager.Paused ();
    }
    /// <summary>
    /// To give any rewards on the end of level
    /// </summary>
    void GiveLevelReward()
    {
        //TODO
    }
    /// <summary>
    /// To toggle between finish points, like hide the enemies from previous goal, or soemthing of that sort. 
    /// </summary>
    /// <param name="TurnOffLastPoint"></param>
    void ToggleFinishPoint(bool TurnOffLastPoint)
    {
        TaskData[] tasks = levels[currentLevelIndex].tasks;
        if (TurnOffLastPoint)
        {
            // deactivate last point 
            if (completedTasksCount != 0 && tasks[completedTasksCount - 1].fininshPoint)
            {
                tasks[completedTasksCount - 1].fininshPoint.SetActive(false);
            }
        }
        //Activate next point
        if (tasks[completedTasksCount].fininshPoint)
        {
            tasks[completedTasksCount].fininshPoint.SetActive(true);
        }
    }
    /// <summary>
    /// To remove the previous point from map
    /// </summary>
    /// <param name="obj"></param>
    void DetachMapItem(GameObject obj)
    {
        //TODO
    }
    /// <summary>
    /// To start the same level again
    /// </summary>
    public void ReplayLevel()
    {
        TimeManager.instance.TimeScaleChange(1f);
        levels[currentLevelIndex].levelData.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //InitLevel (currentLevel);
    }
    /// <summary>
    /// To play next level to current level
    /// </summary>
    public void PlayNextLevel()
    {
        TimeManager.instance.TimeScaleChange(1f);
        TurnOffLevelData();
        PlayerPrefsManager.SetLevelSelected(currentLevel + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    /// <summary>
    /// To turn of the object related to current level 
    /// </summary>
    public void TurnOffLevelData()
    {
        levels[currentLevelIndex].levelData.SetActive(false);
    }
    /// <summary>
    /// To add coins to player inventory 
    /// </summary>
    /// <param name="amount"></param>
    public void AddCoinsToInventory(int amount)
    {
        PlayerPrefsManager.AddPlayerCoins(amount);
    }
    /// <summary>
    /// To get sum of seconds in time 
    /// </summary>
    public int ToSeconds(TimeClass time)
    {
        return time.minutes * 60 + time.seconds;
    }
}

public enum scenes
{
    splash,
    mainManue,
    levelSelection,
    playerSelection,
    gamePlay,
}



