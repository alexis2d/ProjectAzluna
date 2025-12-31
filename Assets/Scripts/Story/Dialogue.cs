using UnityEngine;
using Enums;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

public class Dialogue : MonoBehaviour
{

    private Character speaker;
    private Character listener;
    private string dialogueText;
    private ExpressionEnum expression;
    private bool isEndDialogue = false;

    public Character GetSpeaker()
    {
        return speaker;
    }

    public Character GetListener()
    {
        return listener;
    }

    public string GetDialogueText()
    {
        return dialogueText;
    }

    public ExpressionEnum GetExpression()
    {
        return expression;
    }

    public Choice[] GetChoices()
    {
        return GetComponentsInChildren<Choice>(); ;
    }

    public bool IsEndDialogue()
    {
        return isEndDialogue;
    }

    public int GetStoryId()
    {
        MatchCollection matches = Regex.Matches(name, @"\d+");

        if (matches.Count >= 2)
        {
            return int.Parse(matches[0].Value);
        }

        return 0;
    }

    public int GetId()
    {
        MatchCollection matches = Regex.Matches(name, @"\d+");

        if (matches.Count >= 2)
        {
            return int.Parse(matches[1].Value);
        }

        return 0;
    }

    public void SetDialogueData(DialogueJson dialogueJson)
    {
        List<Character> allCharacters = StoryController.Instance.GetAllCharactersInScene();
        if (dialogueJson.speaker != null)
        {
            Character character = allCharacters.FirstOrDefault(c => c.GetName() == dialogueJson.speaker);
            if (character != null)
            {   
                speaker = character;
            }
        }
        if (dialogueJson.listener != null)
        {
            Character character = allCharacters.FirstOrDefault(c => c.GetName() == dialogueJson.listener);
            if (character != null)
            {   
                listener = character;
            }
        }
        dialogueText = dialogueJson.dialogueText;
        if (System.Enum.TryParse(dialogueJson.expression, out ExpressionEnum parsedExpression))
        {
            expression = parsedExpression;
        }
        else
        {
            expression = ExpressionEnum.Neutral;
        }
        
        if (dialogueJson.choices == null || dialogueJson.choices.datas == null)
        {
            Debug.LogWarning("Choices array is null in the dialogue JSON.");
            return;
        }
        
        if (dialogueJson.choices.datas.Length > 0)
        {
            for (int i = 0; i < dialogueJson.choices.datas.Length; i++)
            {
                ChoiceDataJson choiceDataJson = dialogueJson.choices.datas[i];
                if (choiceDataJson != null)
                {
                    if (!CreateChoiceObjectFromData(choiceDataJson, i + 1).TryGetComponent<Choice>(out var choice))
                    {
                        Debug.Log("Choix " + i + " non créé");
                        return;
                    }
                    if (dialogueJson.choices.function != null)
                    {
                        choice.SetIsImportant(true);
                        choice.SetFunctionName(dialogueJson.choices.function);
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("No choices found in the story JSON.");
        }
    }

    private GameObject CreateChoiceObjectFromData(ChoiceDataJson choiceDataJson, int choiceIndex)
    {
        GameObject choiceObj = new("Choice" + GetId() + "-" + choiceIndex);
        choiceObj.transform.SetParent(gameObject.transform);
        Choice choice = choiceObj.AddComponent<Choice>();
        choice.SetChoiceData(choiceDataJson);
        choice.SetId(choiceIndex);
        Debug.Log("Dialogue created: " + choiceIndex);

        return choiceObj;
    }

    public void SetAsEndDialogue()
    {
        isEndDialogue = true;
    }

    public void LogDatas()
    {
        string speakerName = speaker != null ? speaker.GetName() : "null";
        string listenerName = listener != null ? listener.GetName() : "null";
        Debug.Log($"Dialogue Name: {name}, Dialogue ID: {GetId()}, Speaker: {speakerName}, Listener: {listenerName}, Text: {dialogueText}, Expression: {expression}, IsEndDialogue: {isEndDialogue}");
    }

}
