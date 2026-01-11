using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private string mainsceneName;
    [SerializeField] private string creditSceneName;
    public void LoadMainScene()
    {
        SceneManager.LoadScene(mainsceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }

  public void LoadCreditsScene()
    {
        SceneManager.LoadScene(creditSceneName);
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}