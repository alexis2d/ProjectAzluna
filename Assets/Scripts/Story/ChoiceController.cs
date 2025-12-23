using UnityEngine;

public class ChoiceController : MonoBehaviour
{

    private static ChoiceController instance;
    public static ChoiceController Instance { get { return instance; } }

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

    public void ChoiceForLumiAskingOthersOrTellingAboutHerself()
    {
        // Implementation for handling the choice
    }

}