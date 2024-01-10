using System.Net.Mime;
using System.Diagnostics;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCarController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float horizontalMoveSpeed = 0.01f;
    public float respawnDelay = 4f;
    public float randomXRange = 2.0f;
    public float minDistanceBetweenCars = 1f;
    public GameObject playerCar;
    public float initialSpawnDelay = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerCar = GameObject.FindGameObjectWithTag("PlayerCar");

        StartCoroutine(SpawnCarsWithDelay());
    }

    IEnumerator SpawnCarsWithDelay()
    {
        yield return new WaitForSeconds(initialSpawnDelay);
        SetInitialPosition();

        StartCoroutine(MoveAndRespawn());
    }

    IEnumerator MoveAndRespawn()
    {
        while (true)
        {
            // Move the car down
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

            if (SceneManager.GetActiveScene().name == "Level3")
            {
                if (playerCar != null)
                {
                    float targetX = playerCar.transform.position.x;
                    float translationX = targetX - transform.position.x;
                    transform.Translate(new Vector3(translationX, 0, 0) * horizontalMoveSpeed * Time.deltaTime);
                }
            }

            // Check if the car is completely below the bottom edge of the screen
            if (transform.position.y < -6f)
            {
                // Set the car's position with some space between cars
                SetInitialPosition();

                if (SceneManager.GetActiveScene().name == "Level1")
                {
                    moveSpeed += 0.2f;
                }

                // Wait for a delay before moving again
                StartCoroutine(RespawnDelay());
            }

            yield return null;
        }
    }

    void SetInitialPosition()
    {
        // Randomize x position within the specified range
        float randomX = Random.Range(-randomXRange, randomXRange);
        // Set the car's position
        transform.position = new Vector3(randomX, 6f, 0f);
        return;


    }
    IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
    }
}
