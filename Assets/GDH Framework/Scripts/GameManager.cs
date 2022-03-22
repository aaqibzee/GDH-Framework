using UnityEngine;
public class GameManager 
{
    /// <summary>
    /// To mar the task as completed 
    /// </summary>
    public static void Taskcomplete()
    {
        Object.FindObjectOfType<GameController>().TaskComplete();
    }
    /// <summary>
    /// Use this to do tasks to be done at Game lose event.
    /// </summary>
    public static void GameOver()
    {
        Object.FindObjectOfType<GameController>().OnGameLose();
    }
    /// <summary>
    /// To pause timer in game play scene
    /// </summary>
    public static void PauseTimer()
    {
        Object.FindObjectOfType<TimeManager>().PauseTime();
    }
    /// <summary>
    /// To resume timer in game play scene 
    /// </summary>
    public static void ResumeTimer()
    {
        Object.FindObjectOfType<TimeManager>().ResumeTime();
    }
    /// <summary>
    /// To switch between player. There could be games with more than one player type.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="turnOffPrevious"></param>
    public static void SwitchPlayer(int index, bool flag)
    {
        Object.FindObjectOfType<GameController>().SwitchPlayer(index, flag);
    }
}
