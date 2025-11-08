using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;
    public static PlayerData playerData = null;
    public static LevelData levelData = null;
    private static bool loading = false;

    public void Awake()
    {
        Application.quitting += Quit;

        if (instance == null || loading == true)
        {
            instance = this;
            LoadData();

            if (SceneManager.GetActiveScene().buildIndex == 1 && loading == true)
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
            }

        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public static void Quit()
    {
        Application.quitting -= Quit;
    }

    public void LoadGame()
    {
        loading = true;
        SceneManager.LoadScene(1);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (levelData != null)
        {
            ApplyLevelData(levelData);
            levelData = null;
        }

        loading = false;
    }

    public void NewGame()
    {
        DeleteData();
        LoadGame();
    }

    private void LoadData()
    {
        GameData data = SaveManager.LoadGame();
        if (data != null)
        {
            playerData = data.playerData;
            levelData = data.levelData;
        }
    }

    private void DeleteData()
    {
        playerData = null;
        levelData = null;
    }

     private void ApplyLevelData(LevelData levelData)
    {
        StoryController.Instance.LoadStory(levelData.storyId);
    }

    
}
