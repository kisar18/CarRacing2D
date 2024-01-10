using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 150f;
    private int direction;
    public float levelDuration = 30f;
    public Slider progressBar;
    public GameObject[] carPrefabs;
    public Transform spawnPoint;
    private GameObject selectedCarPrefab;
    private float elapsedTime = 0f;
    float border = 1.9f;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on the player GameObject!");
            return;
        }

        if (spawnPoint == null)
        {
            spawnPoint = GameObject.Find("SpawnPoint").transform;
        }

        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }

        // Retrieve the selected car name from PlayerPrefs
        string selectedCarName = PlayerPrefs.GetString("SelectedCar", "4x4_green");

        // Find the selected car prefab based on its name
        GameObject selectedCarPrefab = Array.Find(carPrefabs, car => car.name == selectedCarName);

        // Spawn the selected car
        SpawnSelectedCar(selectedCarPrefab);
    }

    void Update()
    {
        HandleInput();

        // Update elapsed time
        elapsedTime += Time.deltaTime;

        // Calculate progress as a percentage
        float progress = Mathf.Clamp01(elapsedTime / levelDuration);

        progressBar.value = progress;

        // Check if the level has been completed
        if(progress >= 1f)
        {
          Time.timeScale = 0f;
          LevelCompleted();
        }
    }

    void HandleInput()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    OnTouchStart(touch.position);
                    break;

                case TouchPhase.Moved:
                    OnTouchMove(touch.position);
                    break;

                case TouchPhase.Ended:
                    OnTouchEnd();
                    break;
            }
        }
        
        //Unity testing with mouse
        else if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            OnMouseClick(mousePosition);
        }

        else if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            OnMouseDrag(mousePosition);
        }

        else if (Input.GetMouseButtonUp(0))
        {
            OnMouseRelease();
        }
        
    }
    
    void OnMouseClick(Vector2 mousePosition)
    {
        // Handle mouse click (similar to OnTouchStart)
        OnTouchStart(mousePosition);
    }

    void OnMouseDrag(Vector2 mousePosition)
    {
        // Handle mouse drag (similar to OnTouchMove)
        OnTouchMove(mousePosition);
    }

    void OnMouseRelease()
    {
        // Handle mouse release (similar to OnTouchEnd)
        OnTouchEnd();
    }
    

    void OnTouchStart(Vector2 touchPosition)
    {
        // Check if the touch is on the left or right side of the screen
        if (touchPosition.x < Screen.width / 2)
        {
            // Left side of the screen
            SetDirection(-1); // Move the car to the left
        }
        else
        {
            // Right side of the screen
            SetDirection(1); // Move the car to the right
        }
    }

    void OnTouchMove(Vector2 touchPosition)
    {
      float targetX = Camera.main.ScreenToWorldPoint(touchPosition).x;

      // Clamp the target position within the specified border
      float minX = -border;
      float maxX = border;
      targetX = Mathf.Clamp(targetX, minX, maxX);

      if(rb.position.x >= border) 
      {
        rb.position = new Vector2(border, -3.95f);
      }
      else if(rb.position.x <= -border) 
      {
        rb.position = new Vector2(-border, -3.95f);
      }
    }

    void OnTouchEnd()
    {
        SetDirection(0); // Stop the car when the touch ends
        if(rb.position.x >= border) 
        {
          rb.position = new Vector2(border, -3.95f);
        }
        else if(rb.position.x <= -border) 
        {
          rb.position = new Vector2(-border, -3.95f);
        }
    }

    void SetDirection(int newDirection)
    {
      direction = newDirection;

      // Update the velocity of the Rigidbody to move the car
      float moveAmount = direction * moveSpeed;

      rb.velocity = Vector2.zero;
      rb.velocity = new Vector2(moveAmount, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyCar"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        LoadGameOverScene();
    }

    void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    void LevelCompleted()
    {
        SceneManager.LoadScene("LevelCompletedScene");
    }

    void SpawnSelectedCar(GameObject carPrefab)
    {
        if (carPrefab == null)
        {
            Debug.LogError("Selected car prefab is null!");
            return;
        }

        // Destroy the previous car if any
        foreach (Transform child in spawnPoint)
        {
            Destroy(child.gameObject);
        }

        Instantiate(carPrefab, spawnPoint.position, spawnPoint.rotation, spawnPoint);
    }
}
