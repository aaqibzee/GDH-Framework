using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public GameObject player;
    public CanvasGroup playerControlls;

    public PlayerData()
    {
        player = null;
        playerControlls = null;
    }
}