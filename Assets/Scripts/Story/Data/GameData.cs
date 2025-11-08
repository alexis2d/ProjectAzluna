[System.Serializable]
public class GameData
{
    public PlayerData playerData;
    public LevelData levelData;

    public GameData()
    {
        if (StoryController.Instance == null)
        {
            return;
        }
        playerData = new PlayerData();
        levelData = new LevelData();
    }

}
