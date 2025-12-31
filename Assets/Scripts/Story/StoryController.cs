using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;

public class StoryController : MonoBehaviour
{
    [SerializeField]
    private List<TextAsset> storyFiles;
    private List<Story> stories;
    private List<Character> characters;
    private Story currentStory;
    private Dialogue currentDialogue;
    private static StoryController instance;
    public static StoryController Instance { get { return instance; } }
    private Choice[] importantChoices;
    private Dictionary<string, Action<int>> functionMap;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            characters = FindObjectsByType<Character>(FindObjectsSortMode.InstanceID).ToList();
            foreach (Character character in characters)
            {
                character.SetDialogueState(DialogueStateEnum.None);
                character.gameObject.SetActive(false);
            }

            LoadStoriesFromFiles();
            LoadStory(1);

            if (currentStory == null)
            {
                Debug.Log("No story found.");
                return;
            }

            SetFunctionMap();
            ShowCurrentDialogue();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadStory(int storyId)
    {
        Debug.Log("Loading story ID: " + storyId);
        Debug.Log("Stories length: " + stories.Count);
        Story searchedStory = stories.FirstOrDefault(s => s.getId() == storyId);
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

    public void ShowCurrentDialogue()
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
        if (choice.GetFunctionName() != null)
        {
            RunFunction(choice.GetFunctionName(), choice.GetId());
            return;
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

    private void LoadStoriesFromFiles()
    {
        var settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Include,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            Error = (sender, args) =>
            {
                Debug.LogWarning("JSON warning: " + args.ErrorContext.Error.Message);
                args.ErrorContext.Handled = true;
            }
        };
        stories = new List<Story>();
        for (int i = 0; i < storyFiles.Count; i++)
        {
           StoryJson storyJson = JsonConvert.DeserializeObject<StoryJson>(storyFiles[i].text, settings);

            if (storyJson != null)
            {
                Debug.Log("Nom : " + storyJson.title);
                if (!CreateStoryObjectFromData(storyJson, i + 1).TryGetComponent<Story>(out var story))
                {
                    Debug.Log("Story " + storyJson.title + " non crée");
                    return;
                }
                stories.Add(story);
            }
        }
    }

    private GameObject CreateStoryObjectFromData(StoryJson storyJson, int storyIndex)
    {
        GameObject storyObj = new("Story"+storyIndex);
        storyObj.transform.SetParent(gameObject.transform);
        Story story = storyObj.AddComponent<Story>();
        story.SetStoryData(storyJson);
        Debug.Log("Story created: " + story.GetTitle());

        return storyObj;
    }

    public List<Character> GetAllCharactersInScene()
    {
        return characters;
    }

    private void SetFunctionMap()
    {
         functionMap = new Dictionary<string, Action<int>>
        {
            {
                "ChoiceForLumiAskingOthersOrTellingAboutHerself",
                (value) => ChoiceController.Instance
                    .ChoiceForLumiAskingOthersOrTellingAboutHerself(value)
            }
        };
    }

    private void RunFunction(string functionName, int choiceId)
    {
        if (functionMap.TryGetValue(functionName, out var action))
        {
            action.Invoke(choiceId);
        }
        else
        {
            Debug.LogWarning($"Function not found: {functionName}");
        }
    }
    
}
