using UnityEngine;

public class BackgroundManager : MonoBehaviour
{

    private static BackgroundManager instance;
    public static BackgroundManager Instance { get { return instance; } }
    [SerializeField]
    private SpriteRenderer backgroundRenderer;

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

    public void SetBackground(string backgroundName)
    {
        Sprite backgroundSprite = Resources.Load<Sprite>($"Backgrounds/{backgroundName}");
        if (backgroundSprite != null)
        {
            backgroundRenderer.sprite = backgroundSprite;
        }
        else
        {
            Debug.LogWarning($"Background sprite not found: {backgroundName}");
        }
    }
}