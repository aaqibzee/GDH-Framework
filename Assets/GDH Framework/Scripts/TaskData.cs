using UnityEngine;

[System.Serializable]
public class TaskData
{
    [Tooltip("Finish point will be hidden when player will completes the task")]
    public GameObject fininshPoint = null;
    public string instruction= string.Empty;
    public int taskCompleteReward= 0;
    public TaskData()
    {

    }
}

