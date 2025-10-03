using UnityEngine;

public class StoryController : MonoBehaviour
{
    private Story currentStory;

    private void Awake()
    {
        LoadStory(1);
        if (currentStory == null)
        {
            Debug.Log("No story found.");
            return;
        }
        currentStory.StartStory();
        ShowCurrentDialogue();
    }

    private void LoadStory(int storyId)
    {
        string storyName = "Story" + storyId;
        GameObject storyObject = GameObject.Find(storyName);
        if (storyObject != null)
        {
            currentStory = storyObject.GetComponent<Story>();
        }
        else
        {
            currentStory = null;
        }
    }

    private void ShowCurrentDialogue()
    {
        Dialogue currentDialogue = currentStory.GetCurrentDialogue();
        if (currentDialogue != null)
        {
            UIManager.Instance.ShowDialogue(currentDialogue);
            Character speaker = currentDialogue.GetSpeaker();
            if (speaker != null)
            {
                //speaker.SetExpression(currentDialogue.GetExpression());
            }
        }
        else
        {
            Debug.Log("No more dialogues in the story.");
        }
    }

    
}
