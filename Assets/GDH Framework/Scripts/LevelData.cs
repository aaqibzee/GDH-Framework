using UnityEngine;

[System.Serializable]
public class LevelData
{
    [Tooltip("Player number to spawn for this level")]
    public int player = 0;
    [Tooltip("Time user can use to complete the level.")]
    public TimeClass levelTime;
    [Tooltip("Point in environment where player would be spawned.")]
    public Transform spawnPoint;
    [Tooltip("Data for each level. Like enemies, environment objects, rewards etc")]
    public GameObject levelData;
    [Tooltip("Data for each task user has to complete")]
    public TaskData[] tasks;
    public int levelCompleteReward = 0;
    //private bool isLocked;
    //private bool hasLevelCompleteReward;

    public LevelData()
    {
    }
}