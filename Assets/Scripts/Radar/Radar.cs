using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    [SerializeField] private Transform sweepTransform;
    [SerializeField] private GameObject radarPingBoss;

    private const float rotationSpeed = 220f;
    private const float radarDistance = 150f;
    private List<Collider> colliders;

    private float previousRotation, currentRotation;

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

        RaycastHit hit;

        if (Physics.Raycast(transform.position, GetVectorFromAngle(sweepTransform.eulerAngles.y), out hit, radarDistance))
        {
            if (!colliders.Contains(hit.collider))
            {
                colliders.Add(hit.collider);
                if (hit.collider.name == "Boss")
                {
                    RadarPing radarPingObject = Instantiate(radarPingBoss, hit.point, Quaternion.Euler(90, 0, 0)).GetComponent<RadarPing>();
                    radarPingObject.SetColor(Color.red);
                } 
                else if (hit.collider.name.Contains("Enemy"))
                {
                    RadarPing radarPingObject = Instantiate(radarPingBoss, hit.point, Quaternion.Euler(90, 0, 0)).GetComponent<RadarPing>();
                    radarPingObject.SetColor(Color.yellow);
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
