using System;
using System.Collections;
using System.Threading;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{

    public ProcGenWalker pgw;
    public GameObject bombPrefab;
    private GameObject thisBomb;

    public float bombSpawnInterval = 5f;
    private float spawnIntervalTimer;

    private Color immuneTeam;
    private bool hasImmunity = false;
    private bool spawnPaused = false;

    void Update()
    {
        if (!spawnPaused)
        {
            spawnIntervalTimer += Time.deltaTime;
            if (spawnIntervalTimer >= bombSpawnInterval)
            {
                spawnIntervalTimer = 0f;
                SpawnBombPowerup();
            }  
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
        Bomb b = thisBomb.GetComponent<Bomb>();
        b.Init(this);
    }

    public void BombPickup(Color c)
    {
        immuneTeam = c;
        hasImmunity = true;
        spawnIntervalTimer = 0f;
        PauseSpawn();
        StartExplosion();
        thisBomb = null;
        StartCoroutine(ResetImmunity());
    }

    IEnumerator ResetImmunity()
    {
        yield return new WaitForSeconds(15f);
        hasImmunity = false;
        UnpauseSpawn();
    }

    public void StartExplosion()
    {
        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        foreach (GameObject obj in TurretBehavior.billions.ToArray()) {

            
        }
        yield return new WaitForSeconds(1f);
    }

    public bool CheckImmunity(Color c)
    {
        return hasImmunity && SameColor(c, immuneTeam);
    }

    public bool SameColor(Color one, Color two)
    {
        return Mathf.Abs(one.r - two.r) < 0.05f && Mathf.Abs(one.g - two.g) < 0.05f && Mathf.Abs(one.b - two.b) < 0.05f;
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

    private void PauseSpawn()
    {
        spawnPaused = true;
    }

    private void UnpauseSpawn()
    {
        spawnPaused = false;
    }


}
