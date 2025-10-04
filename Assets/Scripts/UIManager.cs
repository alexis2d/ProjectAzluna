using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private UIDocument uiDocument;
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }
    private VisualElement root;
    private Label dialogueLabel;
    private Label speakerLabel;
    private Choice[] choices;
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
        Debug.Log(dialogue.GetDialogueText());
        dialogueLabel.text = dialogue.GetDialogueText();
        speakerLabel.text = dialogue.GetSpeaker().GetName();
        choices = dialogue.GetChoices();
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
            button.text = choices[index].GetText();
        }
        return button;
    }

    private void ChoiceClicked(int choiceIndex)
    {
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            choiceButtons[i].clicked -= () => ChoiceClicked(i);
        }
        StoryController.Instance.OnChoiceSelected(choices[choiceIndex].GetNextDialogue());
    }

}