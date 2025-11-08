[System.Serializable]
public class PlayerData
{
    public Choice[] importantChoices;

    public PlayerData()
    {
        if (StoryController.Instance == null)
        {
            return;
        }
        importantChoices = StoryController.Instance.GetImportantChoices();
    }

}
