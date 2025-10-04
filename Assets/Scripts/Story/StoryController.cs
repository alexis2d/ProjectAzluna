using Unity.VisualScripting;
using UnityEngine;

public class StoryController : MonoBehaviour
{
    private Story[] stories;
    private Story currentStory;
    private Dialogue currentDialogue;
    private static StoryController instance;
    public static StoryController Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            stories = FindObjectsByType<Story>(FindObjectsSortMode.None);
            LoadStory(1);
            if (currentStory == null)
            {
                Debug.Log("No story found.");
                return;
            }
            ShowCurrentDialogue();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadStory(int storyId)
    {
        string storyName = "Story" + storyId;
        GameObject storyObject = GameObject.Find(storyName);
        if (storyObject != null)
        {
            currentStory = storyObject.GetComponent<Story>();
            foreach (Story story in stories)
            {
                if (story == currentStory) {
                    continue;
                }
                story.gameObject.SetActive(false); 
            }
            currentStory.StartStory();
            currentDialogue = currentStory.GetCurrentDialogue();
        }
        else
        {
            currentStory = null;
            currentDialogue = null;
            Debug.Log("Story object not found: " + storyName);
        }
    }

    private void ShowCurrentDialogue()
    {
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

    public void OnChoiceSelected(Dialogue nextDialogue)
    {
        if (nextDialogue == null && currentDialogue.IsEndDialogue())
        {
            int newStoryId = currentStory.getId() + 1;
            currentStory.gameObject.SetActive(false);
            stories[newStoryId - 1].gameObject.SetActive(true);
            LoadStory(newStoryId);
            ShowCurrentDialogue();
            return;
        }
        currentStory.SetDialogueIndex(nextDialogue.GetId());
        currentDialogue = currentStory.GetCurrentDialogue();
        ShowCurrentDialogue();
    }

    
}
