using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject container;
    private bool isActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isActive)
        {
            isActive = true;
            container.SetActive(true);
            Time.timeScale = 0f;
        } else if (Input.GetKeyDown(KeyCode.Escape) && isActive)
        {
            isActive = false;
            container.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void ResumeButton()
    {
        container.SetActive(false);
            Time.timeScale = 1f;
    }
    
    public void MainMenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("bruno_scene");
    }

}