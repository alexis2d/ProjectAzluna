[System.Serializable]
public class PlayerData
{
    public Choice[] importantChoices;

    public PlayerData()
    {
        importantChoices = StoryController.Instance.GetImportantChoices();
    }

}
