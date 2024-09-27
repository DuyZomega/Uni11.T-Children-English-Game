using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu]
public class GameLevelData : ScriptableObject
{
    [Serializable]
    public struct CategoryRecord
    {
        public string gamecategoryName;
        //public List<BoardData> boardData;
    }

    public List<CategoryRecord> data;
}
