using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class MainMenuUIManager : MonoBehaviour
{

    [SerializeField]
    private UIDocument uiDocument;
    private static MainMenuUIManager instance;
    public static MainMenuUIManager Instance { get { return instance; } }
    private VisualElement root;
    private Button playButton;
     private Button newGameButton;
    private Button quitButton;

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

        playButton = root.Q<Button>("PlayButton");
        playButton.clicked += PlayButtonClicked;
        newGameButton = root.Q<Button>("NewGameButton");
        newGameButton.clicked += NewGameButtonClicked;
        if (SaveManager.HasSave() == true)
        {
            playButton.text = "Continue";
            newGameButton.style.display = DisplayStyle.Flex;
        }
        quitButton = root.Q<Button>("QuitButton");
        quitButton.clicked += QuitClicked;
    }

    private void PlayButtonClicked()
    {
        LevelManager.instance.LoadGame();
    }

    private void NewGameButtonClicked()
    {
        LevelManager.instance.NewGame();
    }

    private void QuitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}