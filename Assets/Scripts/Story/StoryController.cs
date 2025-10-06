using System.Linq.Expressions;
using Enums;
using Unity.VisualScripting;
using UnityEngine;

public class StoryController : MonoBehaviour
{
    private Story[] stories;
    private Character[] characters;
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
            stories = FindObjectsByType<Story>(FindObjectsSortMode.InstanceID);
            characters = FindObjectsByType<Character>(FindObjectsSortMode.InstanceID);
            foreach (Character character in characters)
            {
                character.gameObject.SetActive(false);
            }
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
        Story searchedStory = stories[storyId - 1];
        if (searchedStory != null)
        {
            currentStory = searchedStory;
            foreach (Story story in stories)
            {
                if (story == currentStory)
                {
                    story.gameObject.SetActive(true);
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
            Debug.Log("Story object not found: " + storyId);
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
                speaker.SetExpression(currentDialogue.GetExpression());
            }
        }
        else
        {
            Debug.Log("No more dialogues in the story.");
        }
    }

    public void OnChoiceSelected(Choice choice)
    {
        Dialogue nextDialogue = choice.GetNextDialogue();
        Character listener = currentDialogue.GetListener();
        if (listener != null)
        {
            listener.SetExpression(choice.GetExpression());
        }
        if (nextDialogue == null && currentDialogue.IsEndDialogue())
        {
            LoadNextStory();
            return;
        }
        currentStory.SetDialogueIndex(nextDialogue.GetId());
        currentDialogue = currentStory.GetCurrentDialogue();
        ShowCurrentDialogue();
    }

    public void OnContinueSelected()
    {
        if (currentDialogue.IsEndDialogue())
        {
            LoadNextStory();
            return;
        }
        currentStory.SetDialogueIndex(currentDialogue.GetId() + 1);
        currentDialogue = currentStory.GetCurrentDialogue();
        ShowCurrentDialogue();
    }

    private void LoadNextStory()
    {
        int newStoryId = currentStory.getId() + 1;
        currentStory.EndStory();
        LoadStory(newStoryId);
        ShowCurrentDialogue();
    }

    
}
