using System.Collections.Generic;
using UnityEngine;

public class RadarPulse : MonoBehaviour
{
    [SerializeField] private Transform radarPulse;
    [SerializeField] private GameObject radarPingPrefab;

    private const float RangeSpeed = 150f;
    private float range, rangeMax;
    private List<Collider> hittedObjects;

    private void Awake()
    {
        rangeMax = 300f;
        hittedObjects = new();
    }

    private void Update()
    {
        range += RangeSpeed * Time.deltaTime;
        if (range > rangeMax)
        {
            range = 0;
            hittedObjects.Clear();
        }
        radarPulse.localScale = new Vector3(range, range);

        Collider[] collidersHitArray = Physics.OverlapSphere(radarPulse.position, range/2f);
        foreach (Collider collider in collidersHitArray)
        {
            if (collider != null && !hittedObjects.Contains(collider))
            {
                hittedObjects.Add(collider);
                if (collider.CompareTag("Boss"))
                {
                    RadarPing radarPingObject = Instantiate(radarPingPrefab, collider.transform.position, Quaternion.Euler(90, 0, 0)).GetComponent<RadarPing>();
                    radarPingObject.SetColor(Color.red);
                }
                else if (collider.CompareTag("Enemy"))
                {
                    RadarPing radarPingObject = Instantiate(radarPingPrefab, collider.transform.position, Quaternion.Euler(90, 0, 0)).GetComponent<RadarPing>();
                    radarPingObject.SetColor(Color.yellow);
                }
                else if (collider.CompareTag("Cabin"))
                {
                    RadarPing radarPingObject = Instantiate(radarPingPrefab, collider.transform.position, Quaternion.Euler(90, 0, 0)).GetComponent<RadarPing>();
                    radarPingObject.SetColor(new Color(0, 1, 1));
                }
                else if (collider.CompareTag("Escape"))
                {
                    RadarPing radarPingObject = Instantiate(radarPingPrefab, collider.transform.position, Quaternion.Euler(90, 0, 0)).GetComponent<RadarPing>();
                    radarPingObject.SetColor(Color.white);
                }
            }
        }
    }
}
