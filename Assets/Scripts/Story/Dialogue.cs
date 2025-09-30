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
    
}
