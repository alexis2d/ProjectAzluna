using UnityEngine;

public class Choice : MonoBehaviour
{

    [SerializeField]
    private string choiceText;
    [SerializeField]
    private Dialogue nextDialogue;
    // expression ?

    public string GetText()
    {
        return choiceText;
    }

    public Dialogue GetNextDialogue()
    {
        return nextDialogue;
    }
    
}
