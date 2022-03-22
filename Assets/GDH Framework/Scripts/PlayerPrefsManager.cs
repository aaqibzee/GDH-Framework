using UnityEngine;

public class PlayerPrefsManager
{
    #region Constants
    const string levelsUnlockedKey = "levels_Unlocked";
    const string lockAllLevelsKey = "all_Levels_locked";
    const string muteSoundKey = "sound_Mute";
    const string removeAdsKey = "remove_Ads";
    const string firstRunKey = "first_Run";
    const string levelSlectedKey = "level_Selected";
    const string playerSlectedKey = "player_Selected";
    const string unlockedPlayersKey = "unlockedPlayers";
    const string unlockedEnvoirementKey = "unlockedEnvoirement";
    const string playerCoins = "playerCoins";
    const string levelCoins = "currentLevelCoins";
    #endregion

    /// <summary>
    /// To add coins to inventory for the current level 
    /// </summary>
    /// <param name="coins"></param>
    public static void AddCurrentLevelCoins(int coins)
    {
        coins += PlayerPrefs.GetInt(levelCoins);
        PlayerPrefs.SetInt(levelCoins, coins);
    }
    /// <summary>
    /// To set coins for the current level
    /// </summary>
    /// <param name="coins"></param>
    public static void SetCurrentLevelCoins(int coins)
    {
        PlayerPrefs.SetInt(levelCoins, coins);
    }
    /// <summary>
    /// To get coins for the current level
    /// </summary>
    /// <returns></returns>
    public static int GetCurrentLevelCoins()
    {
        return PlayerPrefs.GetInt(levelCoins, 0);
    }
    /// <summary>
    /// To subtract coins from player's inventory
    /// </summary>
    /// <param name="coins"></param>
    public static void SubtractPlayerCoins(int coins)
    {
        int currentAmount = PlayerPrefs.GetInt(playerCoins);
        currentAmount -= coins;
        PlayerPrefs.SetInt(playerCoins, currentAmount);
    }
    /// <summary>
    /// To add coins to player inventory 
    /// </summary>
    /// <param name="coins"></param>
    public static void AddPlayerCoins(int coins)
    {
        coins += PlayerPrefs.GetInt(playerCoins);
        PlayerPrefs.SetInt(playerCoins, coins);
    }
    /// <summary>
    /// To get coins player has in inventory
    /// </summary>
    /// <returns></returns>
    public static int GetPlayerCoins()
    {
        return PlayerPrefs.GetInt(playerCoins, 0);
    }
    /// <summary>
    /// To set player 
    /// </summary>
    /// <param name="playerNo"></param>
    public static void SetUnlockedPlayers(int playerNo)
    {
        PlayerPrefs.SetInt(unlockedPlayersKey, playerNo);
    }
    /// <summary>
    /// To get players that are unlocked 
    /// </summary>
    /// <returns></returns>
    public static int GetUnlockedPlayers()
    {
        return PlayerPrefs.GetInt(unlockedPlayersKey, 1000);
    }
    /// <summary>
    /// To set the environments user has unlocked uptil now
    /// </summary>
    /// <param name="envNo"></param>
    public static void SetUnlockedEnvoirement(int envNo)
    {
        PlayerPrefs.SetInt(unlockedEnvoirementKey, envNo);
    }
    /// <summary>
    /// To get environments user has unlocked uptil now
    /// </summary>
    /// <returns></returns>
    public static int GetUnlockedEnvoirement()
    {
        return PlayerPrefs.GetInt(unlockedEnvoirementKey, 1000);
    }
    /// <summary>
    /// To set the level number unlocked 
    /// </summary>
    /// <param name="levelNo"></param>
    public static void SetLevelsUnlocked(int levelNo)
    {
        PlayerPrefs.SetInt(levelsUnlockedKey, levelNo);
    }
    /// <summary>
    /// To get the levels that are unlocked 
    /// </summary>
    /// <returns></returns>
    public static int GetLevelsUnlocked()
    {
        return PlayerPrefs.GetInt(levelsUnlockedKey);
    }
    /// <summary>
    /// To set player, user decided to play with 
    /// </summary>
    /// <param name="levelNo"></param>
    public static void SetPlayerSelected(int levelNo)
    {
        PlayerPrefs.SetInt(playerSlectedKey, levelNo);
    }
    /// <summary>
    /// To get the player user decided to play with 
    /// </summary>
    /// <returns></returns>
    public static int GetPlayerSelected()
    {
        return PlayerPrefs.GetInt(playerSlectedKey, 0);
    }
    /// <summary>
    /// To set the level user decided to play 
    /// </summary>
    /// <param name="levelNo"></param>
    public static void SetLevelSelected(int levelNo)
    {
        PlayerPrefs.SetInt(levelSlectedKey, levelNo);
    }
    /// <summary>
    /// To get the number of level user selected 
    /// </summary>
    /// <returns></returns>
    public static int GetLevelSelected()
    {
        return PlayerPrefs.GetInt(levelSlectedKey, 1);
    }
    /// <summary>
    /// To set the key, that all levels are locked 
    /// </summary>
    /// <param name="flag"></param>
    public static void SetAllLevelsLocked(bool flag)
    {
        if (flag)
        {
            PlayerPrefs.SetInt(lockAllLevelsKey, 1);
            SetLevelsUnlocked(1);
        }
        else
        {
            PlayerPrefs.SetInt(lockAllLevelsKey, 0);
        }
    }
    /// <summary>
    /// To set key that indicates that user has selected to mute the game sounds
    /// </summary>
    /// <param name="flag"></param>
    public static void SetMuteSoundsStatus(bool flag)
    {
        if (flag)
        {
            PlayerPrefs.SetInt(muteSoundKey, 1);
        }
        else
        {
            PlayerPrefs.SetInt(muteSoundKey, 0);
        }
    }
    /// <summary>
    /// To check if user has selected to mute the sounds
    /// </summary>
    /// <returns></returns>
    public static bool GetMuteSoundsStatus()
    {
        int result = PlayerPrefs.GetInt(muteSoundKey);

        if (result == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// To set key which indicates that user has removed the ads and has paid version
    /// </summary>
    /// <param name="flag"></param>
    public static void SetRemoveAds(bool flag)
    {
        if (flag)
        {
            PlayerPrefs.SetInt(removeAdsKey, 1);
        }
        else
        {
            PlayerPrefs.SetInt(removeAdsKey, 0);
        }
    }
    /// <summary>
    /// To get status, if player has removed the ads 
    /// </summary>
    /// <returns></returns>
    public static bool GetRemoveAds()
    {
        int result = PlayerPrefs.GetInt(removeAdsKey);

        if (result == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// To set key that indicates if user is playing this game for the first time 
    /// </summary>
    /// <param name="flag"></param>
    public static void SetFirstRunStatus(bool flag)
    {
        if (!flag)
        {
            PlayerPrefs.SetInt(firstRunKey, 1);
        }
        else
        {
            PlayerPrefs.SetInt(firstRunKey, 0);
        }
    }
    /// <summary>
    /// Check if the user is playing game for the first time 
    /// </summary>
    /// <returns></returns>
    public static bool GetFirstRunStatus()
    {
        int result = PlayerPrefs.GetInt(firstRunKey);

        if (result == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}