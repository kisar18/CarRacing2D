using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public Button quitButton;
    public Button mainMenuButton;
    public Button retryButton;

    void Start()
    {
        quitButton.onClick.AddListener(QuitButtonClicked);
        mainMenuButton.onClick.AddListener(MainMenuButtonClicked);

        if (SceneManager.GetActiveScene().name == "GameOverScene")
        {
            retryButton.onClick.AddListener(RetryButtonClicked);
        }
    }

    void QuitButtonClicked()
    {
        Application.Quit();
    }

    void MainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    void RetryButtonClicked()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("SelectedLevel", "Level1"));
    }
}
