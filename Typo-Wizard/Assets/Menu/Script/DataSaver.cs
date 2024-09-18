using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
    public static int ReadCategoryCurrentIndexValues(string name)
    {
        var value = -1;
        if (PlayerPrefs.HasKey(name))
            value = PlayerPrefs.GetInt(name);
        return value;
    }

    public static void SaveCategoryData(string gamecategoryName, int currentIndex)
    {
        PlayerPrefs.SetInt(gamecategoryName, currentIndex);
        PlayerPrefs.Save();
        ApiClient.Instance.SendUserProgress(gamecategoryName, currentIndex, false);
    }

    public static void ClearGameData(GameLevelData levelData)
    {
        foreach (var data in levelData.data)
        {
            PlayerPrefs.SetInt(data.gamecategoryName, -1);
        }

        //Unlock first level
        PlayerPrefs.SetInt(levelData.data[0].gamecategoryName, 0);
        PlayerPrefs.Save();
    }
}
