using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private UIDocument uiDocument;
    private VisualElement root;

    private void OnEnable()
    {
        root = uiDocument.rootVisualElement;
    }

    public void ShowDialogue(Dialogue dialogue)
    {
        Label dialogueLabel = root.Q<Label>("DialogueText");
        dialogueLabel.text = dialogue.GetDialogueText();
        root.Q<VisualElement>("DialogueBox").style.display = DisplayStyle.Flex;
    }

}