using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    AsyncOperation asynchOperation = new AsyncOperation();
    public Slider progressBar;

    //Constatnts Start
    private string AllowSceneActivationMethodName="AllowSceneActivation";
    private string LoadProgressBarMethodName = "LoadProgressBar";
    // Constants End

    void OnEnable()
    {
        EventHub.AttachListener(DataConstants.showPauseAdKey, DoTrigger);
    }
    /// <summary>
    /// 
    /// </summary>
    void DoTrigger()
    {

    }
    /// <summary>
    /// Do all necessary initilizations here 
    /// </summary>
    void Start()
    {
        Invoke("ShowAd", 2f);
        Invoke(AllowSceneActivationMethodName, 5f);

        if (PlayerPrefsManager.GetFirstRunStatus())
        {
            PlayerPrefsManager.SetFirstRunStatus(false);
            PlayerPrefsManager.SetAllLevelsLocked(true);
        }
        progressBar.value = 0f;
        AudioManager.instance.PlaySplashScreenMusic();
        asynchOperation = SceneManager.LoadSceneAsync(DataConstants.mainMainuSceneName);
        while (asynchOperation.progress < 0.88f)
        {

        }
        asynchOperation.allowSceneActivation = false;
        StartCoroutine(LoadProgressBarMethodName);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadProgressBar()
    {
        while (progressBar.value < 95)
        {
            yield return new WaitForSeconds(.1f);
            progressBar.value += 10;
        }
    }
    void AllowSceneActivation()
    {
        asynchOperation.allowSceneActivation = true;
    }
    /// <summary>
    /// To show ad 
    /// </summary>
    void ShowAd()
    {
        //ADManager.ShowInterstitial ();
        EventHub.TriggerEvent(DataConstants.showPauseAdKey);
    }
}
