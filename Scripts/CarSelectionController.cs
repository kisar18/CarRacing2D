using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarSelectionController : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public Transform spawnPoint;
    public Button[] carButtons;
    private GameObject selectedCarPrefab;
    public string level1SceneName = "Level1";

    void Start()
    {
        for (int i = 0; i < carButtons.Length; i++)
        {
            int index = i;
            carButtons[i].onClick.AddListener(() => OnCarButtonClicked(index));
        }
    }

    void OnCarButtonClicked(int carIndex)
    {
        // Set the selected car prefab
        selectedCarPrefab = carPrefabs[carIndex];

        // Save the selected car name using PlayerPrefs
        PlayerPrefs.SetString("SelectedCar", selectedCarPrefab.name);

        LoadSelectedLevel();
    }

    void LoadSelectedLevel()
    {
        string selectedLevel = PlayerPrefs.GetString("SelectedLevel", "Level1");
        // Load the specified scene
        SceneManager.LoadScene(selectedLevel);
    }
}
