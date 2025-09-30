using UnityEngine;
using System.Text.RegularExpressions;

public class Story : MonoBehaviour
{

    [SerializeField]
    private string title;
    private Dialogue[] dialogues;
    private int currentDialogueIndex = 0;

    public int getId()
    {
        return int.Parse(Regex.Replace(gameObject.name, "[^0-9]", ""));;
    }
    
}
