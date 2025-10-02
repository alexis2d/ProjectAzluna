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
    private Choice[] choices;
    [SerializeField]
    private bool isEndDialogue = false;

    private void Start()
    {
        SetChoices();
    }

    private void SetChoices()
    {
        choices = GetComponentsInChildren<Choice>();
    }

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
        return choices;
    }

    public bool IsEndDialogue()
    {
        return isEndDialogue;
    }

}
