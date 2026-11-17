using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    float aspectRatio = (float)Screen.width / Screen.height;
    private void Start()
    {
        Debug.Log(aspectRatio);
        LoadAppropriateMenu();
    }

    private void LoadAppropriateMenu()
    {

        float aspectRatio = (float)Screen.width / Screen.height;
        
        if (aspectRatio < 1.4f)
            SceneManager.LoadSceneAsync("MainMenuTablets", LoadSceneMode.Additive);
       else  if (aspectRatio < 1.8f)
            SceneManager.LoadSceneAsync("MainMenuPhones", LoadSceneMode.Additive);
        else 
            SceneManager.LoadSceneAsync("MainMenuLarge", LoadSceneMode.Additive);
    }

    public void LoadMinigame(string sceneName)
    {
        float aspectRatio = (float)Screen.width / Screen.height;

        if (aspectRatio < 1.4f)
            SceneManager.UnloadSceneAsync("MainMenuTablets");
        else if (aspectRatio < 1.8f)
            SceneManager.UnloadSceneAsync("MainMenuPhones");
        else 
            SceneManager.UnloadSceneAsync("MainMenuLarge");

        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
/*using UnityEngine;
using UnityEngine.SceneManagement;

public class ARScreenFunctions : MonoBehaviour
{
    [SerializeField] private string menuSceneName = "MainMenu";

    public void ReturnToMainMenu()
    {
        SceneManager.LoadSceneAsync(menuSceneName, LoadSceneMode.Single);
    }
}*/