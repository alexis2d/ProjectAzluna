using UnityEngine;
using Enums;
using System.Text.RegularExpressions;

public class Dialogue : MonoBehaviour
{

    [SerializeField]
    private Character speaker;
    [SerializeField]
    private Character listener;
    [SerializeField]
    private string dialogueText;
    [SerializeField]
    private ExpressionEnum expression;
    [SerializeField]
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

}
