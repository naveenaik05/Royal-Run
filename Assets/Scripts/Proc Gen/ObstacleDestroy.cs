using UnityEngine;

public class ObstacleDestroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log(other.gameObject.name + "Destroyed");
        Destroy(other.gameObject);
    }
}
