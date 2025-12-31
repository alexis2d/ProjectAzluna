using UnityEngine;

public class ChoiceController : MonoBehaviour
{

    private static ChoiceController instance;
    public static ChoiceController Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChoiceForLumiAskingOthersOrTellingAboutHerself(int choiceId)
    {
        switch (choiceId)
        {
            case 1:
                StoryController.Instance.LoadStory(3);
                break;
            case 2:
                StoryController.Instance.LoadStory(4);
                break;
            case 3:
                StoryController.Instance.LoadStory(5);
                break;
            default:
                Debug.Log("Invalid choice ID for Lumi Asking Others or Telling About Herself: " + choiceId);
                return;
        }
        StoryController.Instance.ShowCurrentDialogue();
    }

}