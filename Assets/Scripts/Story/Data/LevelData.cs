[System.Serializable]
public class LevelData
{
    public int storyId;

    public LevelData()
    {
        storyId = StoryController.Instance.GetCurrentStoryId();
    }

}
