using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private UIDocument uiDocument;
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }
    private VisualElement root;

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
    }

    public void ShowDialogue(Dialogue dialogue)
    {
        Label dialogueLabel = root.Q<Label>("DialogueText");
        dialogueLabel.text = dialogue.GetDialogueText();
        Label speakerLabel = root.Q<Label>("Speaker");
        speakerLabel.text = dialogue.GetSpeaker().GetName();
        Choice[] choices = dialogue.GetChoices();
        root.Q<VisualElement>("DialogueBubble").style.display = DisplayStyle.Flex;
    }

}