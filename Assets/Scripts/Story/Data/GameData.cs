[System.Serializable]
public class GameData
{
    public PlayerData playerData;
    public LevelData levelData;

    public GameData()
    {
        playerData = new PlayerData();
        levelData = new LevelData();
    }

}
