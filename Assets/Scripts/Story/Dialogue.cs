using UnityEngine;
using Enums;

public class Dialogue : MonoBehaviour
{

    [SerializeField]
    private Character speaker;
    [SerializeField]
    private Character listener;
    [SerializeField]
    private string dialogueText;
    [SerializeField]
    private Expression expression;
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

    public Expression GetExpression()
    {
        return expression;
    }

    public Choice[] GetChoices()
    {
        return GetComponentsInChildren<Choice>();;
    }

    public bool IsEndDialogue()
    {
        return isEndDialogue;
    }

}
