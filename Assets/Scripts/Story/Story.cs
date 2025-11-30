using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;

public class Story : MonoBehaviour
{

    [SerializeField]
    private string title;
    [SerializeField]
    private Character[] characters;
    private Dialogue[] dialogues;
    private int currentDialogueIndex = 0;
    private int id;


    public void StartStory()
    {
        SetId();
        SetDialogues();
        SetCharacters();
    }

    public void EndStory()
    {
        foreach (Character character in characters)
        {
            character.gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    private void SetDialogues()
    {
        dialogues = GetComponentsInChildren<Dialogue>();
    }

    private void SetId()
    {
        id = int.Parse(Regex.Replace(gameObject.name, "[^0-9]", ""));
    }

    private void SetCharacters()
    {
        foreach (Character character in characters)
        {
            character.gameObject.SetActive(true);
        }
    }

    public int getId()
    {
        return id;
    }

    public string GetTitle()
    {
        return title;
    }

    public Dialogue GetCurrentDialogue()
    {
        if (dialogues != null && currentDialogueIndex < dialogues.Length)
        {
            return dialogues[currentDialogueIndex];
        }
        return null;
    }

    public void SetDialogueIndex(int newDialogueId)
    {
        int newDialogueIndex = newDialogueId - 1;
        if (newDialogueIndex >= 0 && newDialogueIndex < dialogues.Length)
        {
            currentDialogueIndex = newDialogueIndex;
        }
    }

    public void SetStoryData(StoryJson storyJson)
    {
        title = storyJson.title;
        foreach (var characterName in storyJson.characters)
        {
            characters.Append(GameObject.Find(characterName).GetComponent<Character>());
        }
    }
    
}
