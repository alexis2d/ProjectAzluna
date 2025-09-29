using UnityEngine;

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
    private Choice[] choices;
    
}
