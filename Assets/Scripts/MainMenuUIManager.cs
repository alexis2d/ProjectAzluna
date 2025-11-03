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

}