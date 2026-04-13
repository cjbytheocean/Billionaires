using UnityEngine;

public class PowerupManager : MonoBehaviour
{

    public ProcGenWalker pgw;
    public GameObject bombPrefab;

    public float bombSpawnInterval = 5f;
    private float spawnIntervalTimer;

    void Update()
    {
        
    }

    // Actually spawns bomb
    void SpawnBombPowerup()
    {
        
    }

    // Helper method, gets valid spawn position
    Vector2 GetPosition()
    {
        return Vector2.zero;
        
    }

    // Helper method, checks if position is far enough away from floor tiles
    bool ValidPosition()
    {
        return false;
    }


   
}
