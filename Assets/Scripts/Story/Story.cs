using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

public class Story : MonoBehaviour
{

    [SerializeField]
    private string title;
    [SerializeField]
    private List<Character> characters;
    private List<Dialogue> dialogues;
    private int currentDialogueIndex = 0;
    private int id;


    public void StartStory()
    {
       /*SetDialogues();
        SetCharacters();*/
    }

    public void EndStory()
    {
        foreach (Character character in characters)
        {
            character.gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    private void SetId()
    {
        id = int.Parse(Regex.Replace(gameObject.name, "[^0-9]", ""));
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
        if (dialogues != null && currentDialogueIndex < dialogues.Count)
        {
            return dialogues[currentDialogueIndex];
        }
        return null;
    }

    public void SetDialogueIndex(int newDialogueId)
    {
        int newDialogueIndex = newDialogueId - 1;
        if (newDialogueIndex >= 0 && newDialogueIndex < dialogues.Count)
        {
            currentDialogueIndex = newDialogueIndex;
        }
    }

    public void SetStoryData(StoryJson storyJson)
    {
        title = storyJson.title;
        List<Character> allCharacters = StoryController.Instance.GetAllCharactersInScene();
        
        if (storyJson == null || allCharacters == null)
        {
            Debug.LogError("Parameters to SetStoryData cannot be null.");
            return;
        }
        SetId();
        characters = new List<Character>();
        if (storyJson.characters.Length > 0)
        {
            foreach (var characterName in storyJson.characters)
            {
                Debug.Log(characterName);
                Character character = allCharacters.FirstOrDefault(c => c.GetName() == characterName);
                if (character != null)
                {   
                    characters.Add(character);
                }
            }
        }
        else
        {
            Debug.LogWarning("No characters found in the story JSON.");
        }
        dialogues = new List<Dialogue>();
        if (storyJson.dialogues.Length > 0)
        {
            for (int i = 0; i < storyJson.dialogues.Length; i++)
            {
                DialogueJson dialogueJson = storyJson.dialogues[i];

                if (dialogueJson != null)
                {
                    if (!CreateDialogueObjectFromData(dialogueJson, i + 1).TryGetComponent<Dialogue>(out var dialogue))
                    {
                        Debug.Log("Dialogue " + i + " non créé");
                        return;
                    }
                    if (i + 1 == storyJson.dialogues.Length)
                    {
                        dialogue.SetAsEndDialogue();
                    }
                    dialogues.Add(dialogue);
                }
            }
        }
        else
        {
            Debug.LogWarning("No dialogues found in the story JSON.");
        }
    }

    private GameObject CreateDialogueObjectFromData(DialogueJson dialogueJson, int dialogueIndex)
    {
        GameObject dialogueObj = new("Dialogue" + id + "-" + dialogueIndex);
        dialogueObj.transform.SetParent(gameObject.transform);
        Dialogue dialogue = dialogueObj.AddComponent<Dialogue>();
        dialogue.SetDialogueData(dialogueJson);
        Debug.Log("Dialogue created: " + dialogueIndex);

        return dialogueObj;
    }

    public void LogDatas()
    {
        Debug.Log("Story Title: " + title);
        Debug.Log("Story Id: " + id);
        Debug.Log("Number of Characters: " + characters.Count);
        if (characters.Count > 0)
        {
            Debug.Log("Characters in Story:");
            foreach (Character character in characters)
            {
                Debug.Log("- " + character.GetName());
            }
        }
        
        Debug.Log("Number of Dialogues: " + dialogues.Count);
    }
    
}
