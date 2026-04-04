using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public RectTransform crosshair;
    public Canvas canvas;

    public LineRenderer line;
    public float maxDistance = 100f;

    public Transform laserOrigin;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            line.enabled = true;
            UpdateLaser();
        }
        else
        {
            line.enabled = false;
        }
    }

    void UpdateLaser()
    {
        // Convert crosshair UI position to screen space
        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, crosshair.position);

        // Screen to world ray
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        Vector3 endPoint;

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            endPoint = hit.point;
        }
        else
        {
            endPoint = ray.origin + ray.direction * maxDistance;
        }

        // Draw from origin to target
        line.SetPosition(0, laserOrigin.position);
        line.SetPosition(1, endPoint);
    }
}