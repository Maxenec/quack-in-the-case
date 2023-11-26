using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public int firstEpisodeUnlockedGames;
    public int firstEpisodeUnlockedScenes;

    public GameData()
    {
        this.firstEpisodeUnlockedGames = 1;
        this.firstEpisodeUnlockedScenes = 0;
    }

}
