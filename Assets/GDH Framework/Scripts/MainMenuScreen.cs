using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScreen : MonoBehaviour
{

    public GameObject confirmationPannel;
    public string RateUsUrl;
    /// <summary>
    /// To do the initializations
    /// </summary>
    void Start()
    {
        AudioManager.instance.PlayMainMenuMusic();
        if (confirmationPannel)
        {
            confirmationPannel.SetActive(false);
        }
    }
    /// <summary>
    /// To load level selection scene 
    /// </summary>
    public void LoadLevelSelection()
    {
        SceneManager.LoadScene(DataConstants.levelSelectionSceneName);
    }
    /// <summary>
    /// To load tutorial screen
    /// </summary>
    public void LoadTutorial()
    {
        PlayerPrefsManager.SetLevelSelected(1);
        SceneManager.LoadScene(DataConstants.tutorialSceneName);
    }
    /// <summary>
    /// To launch rate us url 
    /// </summary>
    public void RateUs()
    {
        Application.OpenURL(RateUsUrl);
    }
    /// <summary>
    /// To handle case when user presses exit button
    /// </summary>
    public void Exit()
    {
        confirmationPannel.SetActive(true);
        //ADManager.Paused ();

    }
    /// <summary>
    /// To handle scenario, when user confirm to exit the game
    /// </summary>
    public void ExitYes()
    {
        Application.Quit();
    }
    /// <summary>
    /// To handle scenario, when user confirm not to exit the game
    /// </summary>
    public void ExitNo()
    {
        confirmationPannel.SetActive(false);
    }
}
