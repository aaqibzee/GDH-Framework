using UnityEngine;

public class TimeManager : MonoBehaviour
{

    [HideInInspector]
    public TimeClass gameTime;
    [HideInInspector]
    public int time = 0;
    [HideInInspector]
    public static TimeManager instance;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    /// <summary>
    /// To start timer in game play scene 
    /// </summary>
    public void StartTime()
    {
        InvokeRepeating("UpdateTime", 1, 1);
    }
    /// <summary>
    /// To pause timer in game play scene
    /// </summary>
    public void PauseTime()
    {
        CancelInvoke("UpdateTime");
    }
    /// <summary>
    /// To resume timer in game play scene 
    /// </summary>
    public void ResumeTime()
    {
        StartTime();
    }
    /// <summary>
    /// To decrement time with each passed second 
    /// </summary>
    void UpdateTime()
    {
        time -= 1;
        gameTime.minutes = time / 60;
        gameTime.seconds = time % 60;
    }
    /// <summary>
    /// To make the game slow or fast 
    /// </summary>
    /// <param name="amount"></param>
    public void TimeScaleChange(float amount)
    {
        Time.timeScale = amount;
    }
    /// <summary>
    /// To set the toatal time for a particular level 
    /// </summary>
    /// <param name="minutes"></param>
    /// <param name="seconds"></param>
    public void SetGameTime(int minutes, int seconds)
    {
        gameTime.minutes = minutes;
        gameTime.seconds = seconds;
        time = minutes * 60 + seconds;
    }
    /// <summary>
    /// To get sum of seconds in time 
    /// </summary>
    public int ToSeconds(TimeClass time)
    {
        return time.minutes * 60 + time.seconds;
    }
    /// <summary>
    /// To get sum of seconds in time 
    /// </summary>
    public int GetGameTimeInSeconds()
    {
        return gameTime.minutes * 60 + gameTime.seconds;
    }
}

[System.Serializable]
public class TimeClass
{
    [Range(0, 60)]
    public int minutes;
    [Range(0, 60)]
    public int seconds;

    TimeClass()
    {
        minutes = 0;
        seconds = 0;
    }
}