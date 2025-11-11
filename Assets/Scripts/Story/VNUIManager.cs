using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using UnityEngine.SceneManagement;

public class VNUIManager : MonoBehaviour
{

    [SerializeField]
    private UIDocument uiDocument;
    private static VNUIManager instance;
    public static VNUIManager Instance { get { return instance; } }
    private VisualElement root;
    private Button optionsButton;
    private Button saveButton;
    private Button quitButton;
    private Button resumeButton;
    private VisualElement optionsContainer;
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

        // Options Elements
        optionsButton = root.Q<Button>("OptionsButton");
        optionsContainer = root.Q<VisualElement>("OptionsContainer");
        optionsButton.clicked += OptionsButtonClicked;
        saveButton = root.Q<Button>("SaveButton");
        saveButton.clicked += SaveButtonClicked;
        quitButton = root.Q<Button>("QuitButton");
        quitButton.clicked += QuitButtonClicked;
        resumeButton = root.Q<Button>("ResumeButton");
        resumeButton.clicked += ResumeButtonClicked;

        // Dialogue Elements
        dialogueLabel = root.Q<Label>("DialogueText");
        speakerLabel = root.Q<Label>("Speaker");
        choicesContainer = root.Q<VisualElement>("ChoicesContainer");
    }

    private void ShowOptions()
    {
        optionsContainer.style.display = DisplayStyle.Flex;
    }

    private void HideOptions()
    {
        optionsContainer.style.display = DisplayStyle.None;
    }

    private void OptionsButtonClicked()
    {
        if (optionsContainer.style.display != DisplayStyle.Flex)
        {
            ShowOptions();
        }
    }

    private void SaveButtonClicked()
    {
        SaveManager.SaveGame();
    }

    private void QuitButtonClicked()
    {
        DestroyScene();
        SceneManager.LoadScene(0);
    }

    private void ResumeButtonClicked()
    {
        if (optionsContainer.style.display != DisplayStyle.None)
        {
            HideOptions();
        }
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
        StoryController.Instance.OnChoiceSelected(choices[choiceIndex]);
    }

    private void ContinueClicked()
    {
        StoryController.Instance.OnContinueSelected();
    }

    private void DestroyScene()
    {
        Destroy(gameObject);
        Destroy(StoryController.Instance.gameObject);
    }

}