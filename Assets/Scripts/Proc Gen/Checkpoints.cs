using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    [SerializeField] float checkPointTimeExtension = 5f;
    [SerializeField] float obstacleDecreaseSpawnTime = .2f;

    const string playerString = "Player";
    GameManager gameManager;
    ObstacleSpawner obstacleSpawner;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        obstacleSpawner = FindFirstObjectByType<ObstacleSpawner>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerString))
        {
            obstacleSpawner.DecreaseObstacleSpawnTime(obstacleDecreaseSpawnTime);
            gameManager.increaseTime = checkPointTimeExtension;
        }
    }
}
