using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public RectTransform crosshair;
    public Canvas canvas;

    public LineRenderer line;
    public float maxDistance = 100f;

    public Transform laserOrigin;
    public float damageCooldown = 0.2f;  // damage every 0.2 seconds
    private float damageTimer = 0f;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            line.enabled = true;
            damageTimer -= Time.deltaTime;  // decrease timer
            UpdateLaser();
        }
        else
        {
            line.enabled = false;
            damageTimer = 0f;  // reset timer when not shooting
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
            // Check if we hit an enemy
            EnemyController enemy = hit.collider.GetComponent<EnemyController>();
            if (enemy != null && damageTimer <= 0f)
            {
                enemy.TakeDamage(1);       // 1 damage per tick
                damageTimer = damageCooldown;
            }
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