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
        quitButton = root.Q<Button>("QuitButton");
        quitButton.clicked += QuitClicked;
    }

    private void PlayButtonClicked()
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