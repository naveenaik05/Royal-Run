
using System.Collections.Generic;
using UnityEngine;

public class LevelGenarator : MonoBehaviour
{

    [Header("References")]
    [SerializeField] GameObject[] chunkPrefab;
    [SerializeField] GameObject checkPointChunkPrefab;
    [SerializeField] CameraController cameraController;
    [SerializeField] Transform chunkParent;

    [Header("Dependencies")]
    [SerializeField] ScoreManager scoreManager;

    [Header("Level Settings")]
    [SerializeField] int startingChunksAmount = 12;

    [Tooltip("Do not change chunck lenght value until prefab values reflects")]
    [SerializeField] float chunkLength = 10f;
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float minMoveSpeed = 2f;
    [SerializeField] float maxMoveSpeed = 20f;
    [SerializeField] float minGravityZ = -22f;
    [SerializeField] float maxGravityZ = -2f;
    [SerializeField] int interval = 8; 
    int chunkCnt = 0;
    

    List<GameObject> chunks = new List<GameObject>();
    void Start()
    {
        SpwanStartingChunks();
    }

    void Update()
    {
        MoveChunks();
    }

    public void ChangeChunkMoveSpeed(float speedAmout)
    {
        float newMoveSpeed = moveSpeed + speedAmout;
        newMoveSpeed = Mathf.Clamp(newMoveSpeed,minMoveSpeed,maxMoveSpeed);

        if(newMoveSpeed != moveSpeed)
        {
            moveSpeed = newMoveSpeed;

            float newGravityZ= Physics.gravity.z - speedAmout;
            newGravityZ = Mathf.Clamp(newGravityZ,minGravityZ,maxGravityZ);
            Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y,newGravityZ);
            cameraController.ChangeCameraFOV(speedAmout);
        }


    }

    void SpwanStartingChunks()
    {
        for (int i = 0; i < startingChunksAmount; i++)
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        float spawnPositionZ = CalculateSpawnPositionZ();

        Vector3 chunkSpawnPos = new Vector3(transform.position.x, transform.position.y, spawnPositionZ);
        GameObject chunkToSpawn = ChooseChunkToSpawn();

        GameObject newChunkGo = Instantiate(chunkToSpawn, chunkSpawnPos, Quaternion.identity, chunkParent);
        chunks.Add(newChunkGo);
        Chunk newChunk = newChunkGo.GetComponent<Chunk>();
        newChunk.Init(this, scoreManager);
    }

    private GameObject ChooseChunkToSpawn()
    {
        GameObject chunkToSpawn;
        if (chunkCnt % interval == 0 && chunkCnt != 0   )
        {
            chunkToSpawn = checkPointChunkPrefab;
            chunkCnt = 0;
        }
        else
        {
            chunkToSpawn = chunkPrefab[Random.Range(0, chunkPrefab.Length)];
            chunkCnt++;
        }

        return chunkToSpawn;
    }

    float CalculateSpawnPositionZ()
    {
        float spawnPositionZ;

        if (chunks.Count == 0)
        {
            spawnPositionZ = transform.position.z;
        }
        else
        {
            spawnPositionZ = chunks[chunks.Count -1].transform.position.z + chunkLength;
        }

        return spawnPositionZ;
    }

    void MoveChunks()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(-transform.forward * Time.deltaTime * moveSpeed);

            if(chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength)
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                SpawnChunk();
            }  
        }
    }

}
