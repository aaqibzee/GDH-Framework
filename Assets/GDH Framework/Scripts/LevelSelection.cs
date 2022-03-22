using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelSelection : MonoBehaviour
{
    #region Declarations
    public GameObject levelSelectionScreen;
    public Slider progress;
    public GameObject loadingScreen;
    public Transform levelsHolder;
    public GameObject homeButton;
    public GameObject environmentAndPlayerSelction;
    public Button[] players;
    public GameObject[] playersPricesObjects;
    public int[] playerPrices;
    public Button[] envoirements;
    public int[] envoirementPrices;
    public GameObject[] envoirementsPricesObjects;
    [Tooltip("For the testing ")]
    public int unlockedLevels = 1;
    int levelToLoad;
    int slectedPlayer = 0;
    int slectedEnvoirement = 0;
    
    string sceneToLoad;
    bool isSlectedPlayerUnlocked;
    bool isSlectedEnvoirementUnlocked;
    public GameObject notificationPannel;
    public Text notificationPannelText;

    AsyncOperation aop = new AsyncOperation();
    public bool hasPlayerAndEnvoirementSelection;
    public Text coinsAmount;
    #endregion

    #region Constants
    private string loadGameMethodName = "LoadGame";
    private string playerSelectedText = "Player Selected";
    private string playerUnlockedAndSelectedText = "Player Unlocked And Selected";
    private string notEnoguhResourcesText = "Not Enough Resources";
    private string environmentSelectedText = "Environment Selected";
    private string environmentUnlockedAndSelectedText = "Environment Unlocked And Selected";
    #endregion

    //public bool debugLockLevel;

    /// <summary>
    /// To initiate the necessary elements
    /// </summary>
    void Start()
    {
        levelSelectionScreen.gameObject.SetActive(true);
        coinsAmount.text += PlayerPrefsManager.GetPlayerCoins();
        levelsHolder.gameObject.SetActive(true);
        environmentAndPlayerSelction.gameObject.SetActive(false);
        homeButton.SetActive(true);
        loadingScreen.SetActive(false);
        notificationPannel.SetActive(false);
        AudioManager.instance.PlayLevelSelectionMusic();
        //Set unlocked levels to test in editor
        if (Application.isEditor)
        {
            PlayerPrefsManager.SetLevelsUnlocked(unlockedLevels);
        }
        for (int childObjectCount = 0; childObjectCount < levelsHolder.childCount; childObjectCount++)
        {
            Button temp = levelsHolder.GetChild(childObjectCount).GetComponent<Button>();
            temp.interactable = false;
            AddValue(childObjectCount + 1, temp);
        }
        for (int levelCounter = 0; levelCounter < PlayerPrefsManager.GetLevelsUnlocked(); levelCounter++)
        {
            levelsHolder.GetChild(levelCounter).GetComponent<Button>().interactable = true;
        }
        TogglePrices();
    }
    /// <summary>
    /// To turn the prices on or off 
    /// </summary>
    public void TogglePrices()
    {
        int tempValueHolder = PlayerPrefsManager.GetUnlockedPlayers();

        for (int playerPriceCounter = 1; playerPriceCounter < playersPricesObjects.Length; playerPriceCounter++)
        {
            playersPricesObjects[playerPriceCounter].SetActive(true);

            if (playerPriceCounter == 1 && tempValueHolder % 1000 >= 100)
            {
                playersPricesObjects[1].SetActive(false);
            }
            else if (playerPriceCounter == 2 && tempValueHolder % 100 >= 10)
            {
                playersPricesObjects[2].SetActive(false);
            }
            else if (playerPriceCounter == 3 && tempValueHolder % 10 >= 1)
            {
                playersPricesObjects[3].SetActive(false);
            }
        }

        tempValueHolder = PlayerPrefsManager.GetUnlockedEnvoirement();

        for (int envoirementsPriceCounter = 1; envoirementsPriceCounter < envoirementsPricesObjects.Length; envoirementsPriceCounter++)
        {
            envoirementsPricesObjects[envoirementsPriceCounter].SetActive(true);

            if (envoirementsPriceCounter == 1 && tempValueHolder % 1000 >= 100)
            {
                envoirementsPricesObjects[1].SetActive(false);
            }
            else if (envoirementsPriceCounter == 2 && tempValueHolder % 100 >= 10)
            {
                envoirementsPricesObjects[2].SetActive(false);
            }
            else if (envoirementsPriceCounter == 3 && tempValueHolder % 10 >= 1)
            {
                envoirementsPricesObjects[3].SetActive(false);
            }
        }
    }
    /// <summary>
    /// To add value to the button, which will be passed next when the button is clicked 
    /// </summary>
    /// <param name="levelNumber"></param>
    /// <param name="button"></param>
    public void AddValue(int levelNumber, Button button)
    {
        button.onClick.AddListener
        (delegate
        {
            SlectedLevel(levelNumber);
        }
        );
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="levelNo"></param>
    public void SlectedLevel(int levelNo)
    {
        levelToLoad = levelNo;
        //levelsHolder.gameObject.SetActive (false);
        if (hasPlayerAndEnvoirementSelection)
        {
            environmentAndPlayerSelction.gameObject.SetActive(true);
        }
        else
        {
            StartCoroutine(loadGameMethodName);
        }
        //ADManager.ShowInterstitial ();
    }
    /// <summary>
    /// To handle player selection case 
    /// </summary>
    /// <param name="playerNo"></param>
    public void SelectPlayer(int playerNo)
    {
        if (playerNo == 1)
        {
            if (PlayerPrefsManager.GetUnlockedPlayers() % 1000 >= 100)
            {
                slectedPlayer = playerNo;
                notificationPannelText.text = playerSelectedText;
                notificationPannel.gameObject.SetActive(true);
            }
            else if (playerPrices[1] <= PlayerPrefsManager.GetPlayerCoins())
            {
                PlayerPrefsManager.SubtractPlayerCoins(playerPrices[1]);
                notificationPannel.gameObject.SetActive(true);
                notificationPannelText.text = playerUnlockedAndSelectedText;
                int temp = PlayerPrefsManager.GetUnlockedEnvoirement() + 100;
                PlayerPrefsManager.SetUnlockedEnvoirement(temp);
                playersPricesObjects[playerNo].gameObject.SetActive(false);
                slectedPlayer = playerNo;
            }
            else
            {

                notificationPannel.gameObject.SetActive(true);
                notificationPannelText.text = notEnoguhResourcesText;
            }
        }
        else if (playerNo == 2)
        {
            if (PlayerPrefsManager.GetUnlockedPlayers() % 100 > 10)
            {
                slectedPlayer = playerNo;
                notificationPannelText.text = playerSelectedText;
                notificationPannel.gameObject.SetActive(true);
            }
            else if (playerPrices[2] <= PlayerPrefsManager.GetPlayerCoins())
            {
                PlayerPrefsManager.SubtractPlayerCoins(playerPrices[2]);
                notificationPannel.gameObject.SetActive(true);
                notificationPannelText.text = playerUnlockedAndSelectedText;
                int temp = PlayerPrefsManager.GetUnlockedEnvoirement() + 10;
                PlayerPrefsManager.SetUnlockedEnvoirement(temp);
                playersPricesObjects[playerNo].gameObject.SetActive(false);
                slectedPlayer = playerNo;
            }
            else
            {
                notificationPannel.gameObject.SetActive(true);
                notificationPannelText.text = notEnoguhResourcesText;
            }
        }
        else if (playerNo == 3)
        {
            if (PlayerPrefsManager.GetUnlockedPlayers() % 10 >= 1)
            {
                slectedPlayer = playerNo;
                notificationPannelText.text = playerSelectedText;
                notificationPannel.gameObject.SetActive(true);
            }
            else if (playerPrices[3] <= PlayerPrefsManager.GetPlayerCoins())
            {
                PlayerPrefsManager.SubtractPlayerCoins(playerPrices[2]);
                notificationPannel.gameObject.SetActive(true);
                notificationPannelText.text = playerUnlockedAndSelectedText;
                int temp = PlayerPrefsManager.GetUnlockedEnvoirement() + 1;
                PlayerPrefsManager.SetUnlockedEnvoirement(temp);
                playersPricesObjects[playerNo].gameObject.SetActive(false);
                slectedPlayer = playerNo;
            }
            else
            {
                notificationPannel.gameObject.SetActive(true);
                notificationPannelText.text = notEnoguhResourcesText;
            }
        }
        else
        {
            slectedPlayer = playerNo;
            notificationPannel.SetActive(true);
            notificationPannelText.text = playerSelectedText;
        }
        //playersPriceGameObjects[playerNo].gameObject.SetActive (false);
    }
    /// <summary>
    /// To let player select the environment they want to play in 
    /// </summary>
    /// <param name="environmentNo"></param>
    public void SelectEnvoirement(int environmentNo)
    {

        if (environmentNo == 1)
        {
            if (PlayerPrefsManager.GetUnlockedEnvoirement() % 1000 >= 100)
            {
                slectedEnvoirement = environmentNo;
                notificationPannelText.text = environmentSelectedText;
                notificationPannel.gameObject.SetActive(true);
            }
            else if (envoirementPrices[1] <= PlayerPrefsManager.GetPlayerCoins())
            {
                PlayerPrefsManager.SubtractPlayerCoins(playerPrices[1]);
                notificationPannel.gameObject.SetActive(true);
                notificationPannelText.text = environmentUnlockedAndSelectedText;
                int temp = PlayerPrefsManager.GetUnlockedEnvoirement() + 100;
                PlayerPrefsManager.SetUnlockedEnvoirement(temp);
                envoirementsPricesObjects[environmentNo].gameObject.SetActive(false);
                slectedEnvoirement = environmentNo;
            }
            else
            {
                notificationPannel.gameObject.SetActive(true);
                notificationPannelText.text = notEnoguhResourcesText;
            }
        }
        else if (environmentNo == 2)
        {
            if (PlayerPrefsManager.GetUnlockedEnvoirement() % 100 > 10)
            {
                slectedEnvoirement = environmentNo;
                notificationPannel.gameObject.SetActive(true);
                notificationPannelText.text = environmentSelectedText;
            }
            else if (envoirementPrices[2] <= PlayerPrefsManager.GetPlayerCoins())
            {
                PlayerPrefsManager.SubtractPlayerCoins(playerPrices[2]);
                notificationPannel.gameObject.SetActive(true);
                notificationPannelText.text = environmentUnlockedAndSelectedText;
                int temp = PlayerPrefsManager.GetUnlockedEnvoirement() + 10;
                PlayerPrefsManager.SetUnlockedEnvoirement(temp);
                envoirementsPricesObjects[environmentNo].gameObject.SetActive(false);
                slectedEnvoirement = environmentNo;
            }
            else
            {
                notificationPannel.gameObject.SetActive(true);
                notificationPannelText.text = notEnoguhResourcesText;
            }
        }
        else if (environmentNo == 3)
        {
            if (PlayerPrefsManager.GetUnlockedEnvoirement() % 10 >= 1)
            {
                slectedEnvoirement = environmentNo;
                notificationPannelText.text = environmentSelectedText;
                notificationPannel.gameObject.SetActive(true);
            }
            else if (envoirementPrices[2] <= PlayerPrefsManager.GetPlayerCoins())
            {
                PlayerPrefsManager.SubtractPlayerCoins(playerPrices[2]);
                notificationPannel.gameObject.SetActive(true);
                notificationPannelText.text = environmentUnlockedAndSelectedText;
                int temp = PlayerPrefsManager.GetUnlockedEnvoirement() + 1;
                PlayerPrefsManager.SetUnlockedEnvoirement(temp);
                envoirementsPricesObjects[environmentNo].gameObject.SetActive(false);
                slectedEnvoirement = environmentNo;
            }
            else
            {
                notificationPannel.gameObject.SetActive(true);
                notificationPannelText.text = notEnoguhResourcesText;
            }
        }
        else
        {
            slectedEnvoirement = environmentNo;
            notificationPannel.SetActive(true);
            notificationPannelText.text = environmentSelectedText;
        }
    }
    /// <summary>
    /// Let player play the level 
    /// </summary>
    public void PlayLevel()
    {
        if (slectedEnvoirement == 0)
        {
            sceneToLoad = DataConstants.gamePlayeSceneOne;
        }
        else if ((slectedEnvoirement == 1))
        {
            sceneToLoad = DataConstants.gamePlayeSceneTwo;
        }
        else if ((slectedEnvoirement == 2))
        {
            sceneToLoad = DataConstants.gamePlayeSceneFour;
        }
        else if ((slectedEnvoirement == 3))
        {
            sceneToLoad = DataConstants.gamePlayeSceneThree;
        }

        PlayerPrefsManager.SetPlayerSelected(slectedPlayer);
        StartCoroutine(loadGameMethodName);
        //ADManager.ShowInterstitial ();
    }
    /// <summary>
    /// To load the game gradually 
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadGame()
    {
        loadingScreen.gameObject.SetActive(true);
        PlayerPrefsManager.SetLevelSelected(levelToLoad);
        environmentAndPlayerSelction.gameObject.SetActive(false);
        homeButton.SetActive(false);
        while (progress.value < 95)
        {
            yield return new WaitForSeconds(.2f);
            progress.value += 10;
        }
        SceneManager.LoadScene(sceneToLoad);
    }
    /// <summary>
    /// To load main menu scene 
    /// </summary>
    public void MainMenuSceneLoad()
    {
        //ADManager.BackPress ();
        SceneManager.LoadScene(DataConstants.mainMainuSceneName);
        //ADManager.Paused ();
    }

}