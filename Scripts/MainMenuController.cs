using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public Button quitButton;
    public Button infoButton;
    void Start()
    {
        // Assign functions to buttons
        level1Button.onClick.AddListener(Level1ButtonClicked);
        level2Button.onClick.AddListener(Level2ButtonClicked);
        level3Button.onClick.AddListener(Level3ButtonClicked);
        quitButton.onClick.AddListener(QuitButtonClicked);
        infoButton.onClick.AddListener(InfoButtonClicked);
    }

    void Level1ButtonClicked()
    {
        PlayerPrefs.SetString("SelectedLevel", "Level1");
        SceneManager.LoadScene("CarSelectionScene");
    }

    void Level2ButtonClicked()
    {
        PlayerPrefs.SetString("SelectedLevel", "Level2");
        SceneManager.LoadScene("CarSelectionScene");
    }

    void Level3ButtonClicked()
    {
        PlayerPrefs.SetString("SelectedLevel", "Level3");
        SceneManager.LoadScene("CarSelectionScene");
    }

    void QuitButtonClicked()
    {
        Application.Quit();
    }

    void InfoButtonClicked()
    {
      SceneManager.LoadScene("InfoScene");
    }
}
