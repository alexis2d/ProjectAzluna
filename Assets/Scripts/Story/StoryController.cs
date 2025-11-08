using System.Linq;
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
    private Choice[] importantChoices;

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
                character.SetDialogueState(DialogueStateEnum.None);
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

    public void LoadStory(int storyId)
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
        ResetCharacters();
        if (currentDialogue != null)
        {
            VNUIManager.Instance.ShowDialogue(currentDialogue);
            Character speaker = currentDialogue.GetSpeaker();
            if (speaker != null)
            {
                speaker.SetDialogueState(DialogueStateEnum.Speaker);
                speaker.SetExpression(currentDialogue.GetExpression());
            }
            Character listener = currentDialogue.GetListener();
            if (listener != null)
            {
                listener.SetDialogueState(DialogueStateEnum.Listener);
            }
        }
        else
        {
            Debug.Log("No more dialogues in the story.");
        }
    }

    public void OnChoiceSelected(Choice choice)
    {
        if (choice.GetIsImportant() == true)
        {
            RegisterImportantChoice(choice);
        }
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

    private void ResetCharacters()
    {
        foreach (Character character in characters)
        {
            character.SetDialogueState(DialogueStateEnum.None);
        }
    }

    public void RegisterImportantChoice(Choice choice)
    {
        if (importantChoices == null)
        {
            importantChoices = new Choice[] { choice };
        }
        else
        {
            if (!importantChoices.Contains(choice))
            {
                var tempList = importantChoices.ToList();
                tempList.Add(choice);
                importantChoices = tempList.ToArray();
            }
        }
    }

    public Choice[] GetImportantChoices()
    {
        return importantChoices;
    }

    public int GetCurrentStoryId()
    {
        if (currentStory != null)
        {
            return currentStory.getId();
        }
        return -1;
    }

    
}
