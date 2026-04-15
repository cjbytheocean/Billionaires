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
        BillionaireExperience highestRank = null;

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

            BillionaireExperience bn = tb.shooterBase;

            if (bn == null)
            {
                continue;
            }

            if (highestRank == null || bn.rank > highestRank.rank)
            {
                highestRank = bn;
            }
        }

        if (highestRank == null)
        {
            return;
        }

        highestRank.rank = Mathf.Max(0, highestRank.rank - 1);
        highestRank.RefreshRank();
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
        // This is reused from another program I wrote, helps ensure that powerup is spawned in camera view.
        Camera c = Camera.main;

        for (int i = 0; i < 250; i++)
        {
            Vector3 v = new Vector3(UnityEngine.Random.Range(0.1f, 0.9f), UnityEngine.Random.Range(0.1f, 0.9f), Mathf.Abs(c.transform.position.z));
            Vector3 conversion = c.ViewportToWorldPoint(v);

            int x = Mathf.RoundToInt(conversion.x);
            int y = Mathf.RoundToInt(conversion.y); 

            if (x < 0 || y < 0 || x >= pgw.gridHandler.GetLength(0) || y >= pgw.gridHandler.GetLength(1))
            {
                continue;
            }

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