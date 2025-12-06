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

    public void SetChoiceData(ChoiceJson choiceJson)
    {
        choiceText = choiceJson.choiceText;
        if (System.Enum.TryParse(choiceJson.expression, out ExpressionEnum parsedExpression))
        {
            expression = parsedExpression;
        }
        else
        {
            expression = ExpressionEnum.Neutral;
        }
        isImportant = false; // Default value; can be modified later if needed
    }
    
}
