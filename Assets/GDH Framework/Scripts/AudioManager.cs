using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip splashScreenMusic;
    public AudioClip mainMenuMusic;
    public AudioClip gamePauseMusic;
    public AudioClip levelSelectionMusic;
    public AudioClip gamePlayMusic;
    public AudioClip gameWinMusic;
    public AudioClip gameLooseMusic;
    public AudioClip uISelctionSound;
    public AudioClip taskCompleteSound;
    public AudioClip alaramSound;
    public AudioSource audioPlayer;

    public bool canPlaySound;
    public static AudioManager instance;


    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        canPlaySound = !PlayerPrefsManager.GetMuteSoundsStatus();
    }
    void Start()
    {
        if (canPlaySound)
        {
            audioPlayer.loop = true;
            audioPlayer.Play();
        }
    }
    /// <summary>
    /// To play splash screen music
    /// </summary>
    public void PlaySplashScreenMusic()
    {
        PlaySoundInloop(splashScreenMusic);
    }
    /// <summary>
    /// To play level selection scene music 
    /// </summary>
    public void PlayLevelSelectionMusic()
    {
        PlaySoundInloop(levelSelectionMusic);
    }
    /// <summary>
    /// To plya main menu scene music
    /// </summary>
    public void PlayMainMenuMusic()
    {
        PlaySoundInloop(mainMenuMusic);
    }
    /// <summary>
    /// To plya game pause music
    /// </summary>
    public void PlayGamePauseMusic()
    {
        PlaySoundInloop(gamePauseMusic);
    }
    /// <summary>
    /// To play sound during game play 
    /// </summary>
    public void PlayGamePlayMusic()
    {
        PlaySoundInloop(gamePlayMusic);
    }
    /// <summary>
    /// To play game win music 
    /// </summary>
    public void PlayGameWinMusic()
    {
        PlaySoundInloop(gameWinMusic);
    }
    /// <summary>
    /// To play ganme lose music
    /// </summary>
    public void PlayGameLoseMusic()
    {
        PlaySoundInloop(gameLooseMusic);
    }
    /// <summary>
    /// To play music on completing tasks, like completing a certain goal
    /// </summary>
    public void PlayTaskCompleteSound()
    {
        if (taskCompleteSound)
        {
            audioPlayer.PlayOneShot(taskCompleteSound);
        }
    }
    /// <summary>
    /// To play alarm sound, usually when player is about to lose
    /// </summary>
    public void PlayAlarmSound()
    {
        PlaySoundInloop(alaramSound);
    }
    /// <summary>
    /// To play button click sound
    /// </summary>
    public void PlayButtonClickSound()
    {
        if (uISelctionSound)
        {
            audioPlayer.PlayOneShot(uISelctionSound);
        }
    }
    /// <summary>
    /// To mute the game sounds
    /// </summary>
    public void MuteSounds()
    {
        canPlaySound = false;
        PlayerPrefsManager.SetMuteSoundsStatus(true);
        audioPlayer.Stop();
    }
    /// <summary>
    /// To unmute sounds
    /// </summary>
    public void UnMuteSounds()
    {
        canPlaySound = true;
        PlayerPrefsManager.SetMuteSoundsStatus(false);
        audioPlayer.Play();
    }
    /// <summary>
    /// To unmute sounds
    /// </summary>
    public void PlaySoundInloop(AudioClip clip)
    {
        if (clip)
        {
            audioPlayer.clip = clip;
            audioPlayer.loop = true;
            audioPlayer.Play();
        }
    }
}
