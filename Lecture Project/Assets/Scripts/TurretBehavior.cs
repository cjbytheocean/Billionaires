using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    public Transform turret;
    public Color billionaireColor;
    public static List<GameObject> billions = new List<GameObject>(); 
    float storedAngle;

    public GameObject blaster;
    public float blasterInterval = 1.0f;
    public float blasterDistance = 3f;
    public float rotationSpeed = 180f;
    public Transform blasterFirePoint;
    float blasterFireTimer;

    public bool isBillionaire;
    public bool isBillion;

    public BillionaireExperience shooterBase;

    public ParticleSystem particles;

    void Awake()
    {
        billions.Add(gameObject); 

        if (particles == null)
        {
            particles = GetComponent<ParticleSystem>();
        }
    }

    void OnDestroy()
    {
        billions.Remove(gameObject);
    }

    void LateUpdate()
    {
        GameObject t = ClosestBillion();

        if (t != null)
        {
            if (isBillionaire)
            {
                RotateBillionaireTurret(t.transform);
            }
            else
            {
                RotateBillionTurret(t.transform); 
            }
            float dist = Vector3.Distance(transform.position, t.transform.position);

            if (dist <= blasterDistance)
            {
                blasterFireTimer += Time.deltaTime;

                if (blasterFireTimer >= blasterInterval)
                {
                    blasterFireTimer = 0;
                    GameObject billionBlaster = Instantiate(blaster, blasterFirePoint.position, blasterFirePoint.rotation);

                    BlasterBehavior bb = billionBlaster.GetComponent<BlasterBehavior>();
                    bb.blasterColor = billionaireColor;
                    bb.isBillionaireBlaster = isBillionaire;

                    bb.opp = shooterBase;
   
                    SpriteRenderer sr = billionBlaster.GetComponent<SpriteRenderer>();
                    if (sr != null)
                    {
                        sr.color = billionaireColor;
                    }
                }
            }
            else
            {
                blasterFireTimer = 0;
            }
        }
        else
        {
            turret.localRotation = Quaternion.Euler(0, 0, storedAngle);
        }
    }

    public void StartParticles(bool s)
    {
        if (!isBillionaire)
        {
            return;
        }

        if (s)
        {
            particles.gameObject.SetActive(true);
            particles.Clear(true);
            particles.Play();
        }
        else
        {
            particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    GameObject ClosestBillion()
    {
        GameObject closestBillion = null;
        float dist = Mathf.Infinity;

        foreach (GameObject billion in billions)
        {
            TurretBehavior tb = billion.GetComponent<TurretBehavior>();

            if (billion == gameObject || billion == null)
            {
                continue;
            }

            if (Vector4.Distance(tb.billionaireColor, billionaireColor) < 0.01f)
            {
                continue;
            }

            float dst = Vector3.Distance(transform.position, billion.transform.position);

            if (dst < dist)
            {
                dist = dst;
                closestBillion = billion;
            }
        }
        return closestBillion;
    }

    void RotateBillionTurret(Transform tb)
    {
        Vector2 pointDirection = tb.position - transform.position;
        float angle = Mathf.Atan2(pointDirection.y, pointDirection.x) * Mathf.Rad2Deg;
        storedAngle = angle + 90f;
        turret.localRotation = Quaternion.Euler(0, 0, angle + 90f);
    }

    void RotateBillionaireTurret(Transform tb)
    {
        Vector2 pointDirection = tb.position - transform.position;
        float angle = Mathf.Atan2(pointDirection.y, pointDirection.x) * Mathf.Rad2Deg;
        float billionAngle = angle + 90f;
        storedAngle = billionAngle;
        
        Quaternion turretRotation = Quaternion.Euler(0, 0, billionAngle);
        turret.localRotation = Quaternion.RotateTowards(turret.localRotation, turretRotation, rotationSpeed * Time.deltaTime);
    }
}