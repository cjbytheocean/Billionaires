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

    public GameObject explosionPrefab;

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

    void SpawnBombPowerup()
    {
        if (thisBomb != null)
        {
            return;
        }

        Vector2 position = GetPosition();

        if (position == Vector2.zero)
        {
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

        Vector3 bombPosition = thisBomb.transform.position;
        StartExplosion();
        StartCoroutine(StartExplosionVisuals(bombPosition));


        thisBomb = null;
        StartCoroutine(ResetImmunity());
    }

    IEnumerator ResetImmunity()
    {
        yield return new WaitForSeconds(15f);
        hasImmunity = false;
        Debug.Log("immunity over");

        foreach (GameObject billion in TurretBehavior.billions.ToArray())
        {
            if (billion == null)
            {
                continue;
            }

            TurretBehavior tb = billion.GetComponent<TurretBehavior>();
            if (tb != null && tb.isBillionaire)
            {
                tb.StartParticles(false);
            }
        }
        UnpauseSpawn();
    }

    public void StartExplosion()
    {
        StartCoroutine(Explosion());
    }

    IEnumerator StartExplosionVisuals(Vector3 p)
    {
        float explosionDuration = 10f;
        float explosionTimer = 0f;
        GameObject explosionAid = Instantiate(explosionPrefab, p, Quaternion.identity);

        Debug.Log("explosion called");

        while (explosionTimer < explosionDuration)
        {
            explosionTimer += Time.deltaTime;

            if (explosionAid != null)
            {
                explosionAid.transform.localScale += Vector3.one * 10f * Time.deltaTime;
            }

            yield return null;
        }

        if (explosionAid != null)
        {
            Destroy(explosionAid);
        }

        Debug.Log("explosion ended");
    }

    IEnumerator Explosion()
    {
        foreach (GameObject billion in TurretBehavior.billions.ToArray()) {

            if (billion == null)
            {
                continue;
            }

            TurretBehavior tb = billion.GetComponent<TurretBehavior>();

            if (tb == null)
            {
                continue;
            }

            bool immune = SameColor(tb.billionaireColor, immuneTeam);

            if (immune)
            {
                if (tb.isBillionaire)
                {
                    tb.StartParticles(true);
                }
            }
            else
            {
                if (tb.isBillionaire)
                {
                    tb.StartParticles(false); 
                }
                else
                {
                    if (tb.particles != null)
                    {
                        tb.particles.transform.parent = null;
                        tb.particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                    }
                    //Destroy(billion);
                    StartCoroutine(DestroyBillionsDelay(billion, 8f));
                }
            }
        }
        RankDown();
        yield return new WaitForSeconds(1f);
    }

    IEnumerator DestroyBillionsDelay(GameObject g, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (g != null)
        {
            TurretBehavior.billions.Remove(g);
            Destroy(g);
        } 
    }

    void RankDown()
    {
        TurretBehavior highestRank = null;

        foreach (GameObject billion in TurretBehavior.billions)
        {
            if (billion == null)
            {
                continue;
            }

            TurretBehavior tb = billion.GetComponent<TurretBehavior>();

            if (tb == null)
            {
                continue;
            }

            if (!tb.isBillionaire)
            {
                
                continue;
            }

            if (SameColor(tb.billionaireColor, immuneTeam))
            {
                
                continue;
            }

            if (tb.shooterBase == null) // if it is null it crashes
            {
                continue;
            }

            if (tb.shooterBase.rank <= 0)
            {
                continue;
            }

            if (highestRank == null || tb.shooterBase.rank > highestRank.shooterBase.rank)
            {
                highestRank = tb;
            }
        }

        if (highestRank == null)
        {
            return;
        }

        highestRank.shooterBase.rank = Mathf.Max(0, highestRank.shooterBase.rank - 1);
        highestRank.shooterBase.RefreshRank();
    }

    public bool CheckImmunity(Color c)
    {
        return hasImmunity && SameColor(c, immuneTeam);
    }

    public bool SameColor(Color one, Color two)
    {
        return Mathf.Abs(one.r - two.r) < 0.05f && Mathf.Abs(one.g - two.g) < 0.05f && Mathf.Abs(one.b - two.b) < 0.05f;
    }

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

        BillionSpawner[] s = FindObjectsByType<BillionSpawner>(FindObjectsSortMode.None);

        foreach (BillionSpawner sp in s)
        {
            sp.CancelInvoke(nameof(BillionSpawner.BillionSpawn));
        }
    }

    private void UnpauseSpawn()
    {
        spawnPaused = false;

        BillionSpawner[] s = FindObjectsByType<BillionSpawner>(FindObjectsSortMode.None);

        foreach (BillionSpawner sp in s)
        {
            sp.InvokeRepeating(nameof(BillionSpawner.BillionSpawn), sp.spawnInterval, sp.spawnInterval);
        }
    }
}