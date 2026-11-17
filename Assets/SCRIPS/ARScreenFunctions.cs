using UnityEngine;
using UnityEngine.SceneManagement;

public class ARScreenFunctions : MonoBehaviour
{
    [SerializeField] private string menuSceneName = "MainMenu";

    public void ReturnToMainMenu()
    {
        Debug.Log("jala o no ");
        SceneManager.LoadSceneAsync(menuSceneName, LoadSceneMode.Single);
        
    }
}
