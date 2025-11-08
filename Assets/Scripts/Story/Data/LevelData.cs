[System.Serializable]
public class LevelData
{
    public int storyId;

    public LevelData()
    {
        if (StoryController.Instance == null)
        {
            return;
        }
        storyId = StoryController.Instance.GetCurrentStoryId();
    }

}
