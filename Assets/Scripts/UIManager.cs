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
    private VisualElement choicesContainer;

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
        choicesContainer = root.Q<VisualElement>("ChoicesContainer");
    }

    public void ShowDialogue(Dialogue dialogue)
    {
        dialogueLabel.text = dialogue.GetDialogueText();
        string speakerName = "Narrator";
        if (dialogue.GetSpeaker() != null)
        {
            speakerName = dialogue.GetSpeaker().GetName();
        }
        speakerLabel.text = speakerName;
        choices = dialogue.GetChoices();
        choicesContainer.Clear();
        GenerateButtons();
        root.Q<VisualElement>("DialogueBubble").style.display = DisplayStyle.Flex;
    }

    private void GenerateButtons()
    {
        if (choices == null || choices.Length == 0)
        {
            Button btn = new Button();
            btn.text = "Continue";
            btn.name = "Choice1";
            btn.clicked += () => ContinueClicked();
            choicesContainer.Add(btn);
            return;
        }
        for (int i = 0; i < choices.Length; i++)
        {
            int index = i;
            Button btn = new Button();
            btn.text = choices[index].GetText();
            btn.name = "Choice" + (index + 1);
            btn.clicked += () => ChoiceClicked(index);
            choicesContainer.Add(btn);
        }
    }

    private void ChoiceClicked(int choiceIndex)
    {
        StoryController.Instance.OnChoiceSelected(choices[choiceIndex].GetNextDialogue());
    }

    private void ContinueClicked()
    {
        StoryController.Instance.OnContinueSelected();
    }

}