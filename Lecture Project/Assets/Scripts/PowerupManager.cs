using System;
using System.Threading;
using Unity.Collections;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{

    public ProcGenWalker pgw;
    public GameObject bombPrefab;
    private GameObject thisBomb;

    public float bombSpawnInterval = 5f;
    private float spawnIntervalTimer;

    void Update()
    {
        spawnIntervalTimer += Time.deltaTime;
        if (spawnIntervalTimer >= bombSpawnInterval)
        {
            spawnIntervalTimer = 0f;
            SpawnBombPowerup();
        }
    }

    // Actually spawns bomb
    void SpawnBombPowerup()
    {
        if (thisBomb != null)
        {
            return;
        }

        Vector2 position = GetPosition();

        if (position == Vector2.zero)
        {
            Debug.Log("failed");
            return;
        }

        thisBomb = Instantiate(bombPrefab, position, Quaternion.identity);
        Debug.Log("spawned at:" + position);
    }

    // Helper method, gets valid spawn position
    Vector2 GetPosition()
    {
        for (int i = 0; i < 250; i++)
        {
            int x = UnityEngine.Random.Range((int) pgw.mapMinimum.x, (int) pgw.mapMaximum.x);
            int y = UnityEngine.Random.Range((int) pgw.mapMinimum.y, (int) pgw.mapMaximum.y);

            if (ValidPosition(x, y, 1))
            {
                return new Vector2(x, y);
            }  
        }
        return Vector2.zero;
    }

    // Helper method, checks if position is far enough away from floor tiles
    bool ValidPosition(int x, int y, int b)
    {
        if (pgw.gridHandler[x, y] != ProcGenWalker.Grid.FLOOR)
        {
            return false;
        }

        for (int a = -b; a <= b; a++)
        {
            for (int c = -b; c <= b; c++)
            {
                int newX = x + a;
                int newY = y + c;

                if (newX < 0 || newY < 0 || newX >= pgw.gridHandler.GetLength(0) || newY >= pgw.gridHandler.GetLength(1))
                {
                    continue;
                }

                if (pgw.gridHandler[newX, newY] == ProcGenWalker.Grid.WALL)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
