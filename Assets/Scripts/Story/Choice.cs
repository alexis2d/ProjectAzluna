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

    public void SetIsImportant(bool newIsImportant) 
    { 
        isImportant = newIsImportant; 
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
