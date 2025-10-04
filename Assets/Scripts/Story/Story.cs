using UnityEngine;
using System.Text.RegularExpressions;

public class Story : MonoBehaviour
{

    [SerializeField]
    private string title;
    private Dialogue[] dialogues;
    private int currentDialogueIndex = 0;
    private int id;


    public void StartStory()
    {
        SetId();
        SetDialogues();
    }

    private void SetDialogues()
    {
        dialogues = GetComponentsInChildren<Dialogue>();
    }

    private void SetId()
    {
        id = int.Parse(Regex.Replace(gameObject.name, "[^0-9]", ""));
    }

    public int getId()
    {
        return id;
    }

    public string GetTitle()
    {
        return title;
    }

    public Dialogue GetCurrentDialogue()
    {
        if (dialogues != null && currentDialogueIndex < dialogues.Length)
        {
            return dialogues[currentDialogueIndex];
        }
        return null;
    }

    public void SetDialogueIndex(int newDialogueId)
    {
        int newDialogueIndex = newDialogueId - 1;
        if (newDialogueIndex >= 0 && newDialogueIndex < dialogues.Length)
        {
            currentDialogueIndex = newDialogueIndex;
        }
    }
    
}
