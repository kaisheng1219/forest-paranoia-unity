using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    [SerializeField] private Transform sweepTransform;
    [SerializeField] private GameObject radarPingBoss;

    private const float rotationSpeed = 360f;
    private const float radarDistance = 150f;
    private List<Collider> colliders;

    private float previousRotation, currentRotation;
    private RaycastHit hit;

    private void Start()
    {
        colliders = new();
    }

    void Update()
    {
        previousRotation = (sweepTransform.eulerAngles.y % 360) - 180;
        sweepTransform.eulerAngles -= new Vector3(0, 0, rotationSpeed * Time.deltaTime);
        currentRotation = (sweepTransform.eulerAngles.y % 360) - 180;

        // Half rotation check
        if (previousRotation < 0 && currentRotation >= 0)
        {
            colliders.Clear();
        }

        if (Physics.Raycast(transform.position, GetVectorFromAngle(sweepTransform.eulerAngles.y), out hit, radarDistance, 1 << 7))
        {
            if (!colliders.Contains(hit.collider))
            {
                colliders.Add(hit.collider);

                if (hit.collider.CompareTag("Boss"))
                {
                    RadarPing radarPingObject = Instantiate(radarPingBoss, hit.collider.transform.position, Quaternion.Euler(90, 0, 0)).GetComponent<RadarPing>();
                    radarPingObject.SetColor(Color.red);
                } 
                else if (hit.collider.CompareTag("Enemy"))
                {
                    RadarPing radarPingObject = Instantiate(radarPingBoss, hit.point, Quaternion.Euler(90, 0, 0)).GetComponent<RadarPing>();
                    radarPingObject.SetColor(Color.yellow);
                }
                else if (hit.collider.name.Contains("Cabin"))
                {
                    RadarPing radarPingObject = Instantiate(radarPingBoss, hit.point, Quaternion.Euler(90, 0, 0)).GetComponent<RadarPing>();
                    radarPingObject.SetColor(Color.blue);
                }
            } 
        }
    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = Mathf.Deg2Rad * angle;
        return new Vector3(Mathf.Cos(angleRad), 0, -Mathf.Sin(angleRad));
    }
}
