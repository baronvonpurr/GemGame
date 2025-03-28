using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string StartScene;
    public void StartGame()
    {
        SceneManager.LoadScene(StartScene);
        SceneManager.LoadScene("BaseScene", LoadSceneMode.Additive);
    }
}
