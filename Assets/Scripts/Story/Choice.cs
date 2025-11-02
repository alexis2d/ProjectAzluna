using Enums;
using UnityEngine;

public class Choice : MonoBehaviour
{

    [SerializeField]
    private string choiceText;
    [SerializeField]
    private Dialogue nextDialogue;
    [SerializeField]
    private ExpressionEnum expression;
    [SerializeField]
    private bool isImportant;

    public string GetText()
    {
        return choiceText;
    }

    public Dialogue GetNextDialogue()
    {
        return nextDialogue;
    }

    public ExpressionEnum GetExpression()
    {
        return expression;
    }

    public bool GetIsImportant()
    {
        return isImportant;
    }
    
}
