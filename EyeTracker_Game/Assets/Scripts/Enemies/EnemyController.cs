using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public Sprite idleSprite;
    public Sprite chargingSprite;
    public Sprite shootingSprite;
    public Transform spawnPoint;
    public EnemySpawner spawner;

    private SpriteRenderer sr;

    public float idleTime = 1.5f;
    public float chargeTime = 1f;
    public float shootTime = 0.3f;

    private EnemyState currentState;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(StateLoop());
    }

    IEnumerator StateLoop()
    {
        while (true)
        {
            // IDLE
            SetState(EnemyState.Idle);
            yield return new WaitForSeconds(idleTime);

            // CHARGING
            SetState(EnemyState.Charging);
            yield return new WaitForSeconds(chargeTime);

            // SHOOTING
            SetState(EnemyState.Shooting);
            Shoot();
            yield return new WaitForSeconds(shootTime);
        }
    }

    void SetState(EnemyState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case EnemyState.Idle:
                sr.sprite = idleSprite;
                break;

            case EnemyState.Charging:
                sr.sprite = chargingSprite;
                break;

            case EnemyState.Shooting:
                sr.sprite = shootingSprite;
                break;
        }
    }

    void Shoot()
    {
        Debug.Log("Enemy fired!");
        // Later: damage player, trigger effects, etc.
    }

    public void Die()
    {
        spawner.FreePoint(spawnPoint);
        Destroy(gameObject);
    }
}

enum EnemyState
{
    Idle,
    Charging,
    Shooting
}