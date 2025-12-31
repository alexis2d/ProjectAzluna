using Enums;
using UnityEngine;

public class Choice : MonoBehaviour
{

    private int choiceId;
    private string choiceText;
    private Dialogue nextDialogue;
    private ExpressionEnum expression;
    private bool isImportant;
    private string functionName;

    public string GetText()
    {
        return choiceText;
    }

    public int GetId()
    {
        return choiceId;
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

    public string GetFunctionName()
    {
        return functionName;
    }

    public void SetId(int newId) 
    { 
        choiceId = newId; 
    }

    public void SetIsImportant(bool newIsImportant) 
    { 
        isImportant = newIsImportant; 
    }

    public void SetFunctionName(string newFunctionName) 
    { 
        functionName = newFunctionName; 
    }

    public void SetChoiceData(ChoiceDataJson choiceData)
    {
        choiceText = choiceData.choiceText;
        if (System.Enum.TryParse(choiceData.expression, out ExpressionEnum parsedExpression))
        {
            expression = parsedExpression;
        }
        else
        {
            expression = ExpressionEnum.Neutral;
        }
        isImportant = false;
    }
    
}
