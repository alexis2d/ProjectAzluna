using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private UIDocument uiDocument;
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }
    private VisualElement root;
    private Label dialogueLabel;
    private Label speakerLabel;
    private Button[] choiceButtons;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        root = uiDocument.rootVisualElement;
        dialogueLabel = root.Q<Label>("DialogueText");
        speakerLabel = root.Q<Label>("Speaker");
    }

    public void ShowDialogue(Dialogue dialogue)
    {
        dialogueLabel.text = dialogue.GetDialogueText();
        speakerLabel.text = dialogue.GetSpeaker().GetName();
        Choice[] choices = dialogue.GetChoices();
        choiceButtons = new Button[choices.Length];
        
        for (int i = 0; i < choices.Length; i++)
        {
            choiceButtons[i] = GetButtonByIndex(i);
        }
        root.Q<VisualElement>("DialogueBubble").style.display = DisplayStyle.Flex;
    }

    private Button GetButtonByIndex(int index)
    {
        Button button = root.Q<Button>("Choice" + (index + 1));
        if (button != null)
        {
            button.clicked += () => ChoiceClicked(index);
        }
        return button;
    }
    
    private void ChoiceClicked(int choiceIndex)
    {
        Debug.Log("Choice " + (choiceIndex + 1) + " clicked.");
    }

}