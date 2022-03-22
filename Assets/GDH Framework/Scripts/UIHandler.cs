using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AttachListernsToAllTheButtons();
    }

    /// <summary>
    /// To attach click sound on all the buttons in the secne
    /// </summary>
    public void AttachListernsToAllTheButtons()
    {
        AudioManager.instance.PlayButtonClickSound();

        var buttonInScene = Resources.FindObjectsOfTypeAll<Button>();
        for (int buttonCounter=0;buttonCounter<buttonInScene.Length;buttonCounter++)
        {
            buttonInScene[buttonCounter].onClick.AddListener
            (   delegate
                {
                    AudioManager.instance.PlayButtonClickSound();
                }
            );
        }   
    }
    /// <summary>
    /// To mute and unmute sounds
    /// </summary>
    public void MuteAndUnmute()
    {
        if(AudioManager.instance.canPlaySound)
        {
            AudioManager.instance.MuteSounds();
        }
        else
        {
            AudioManager.instance.UnMuteSounds();
        }
    }
}
