using UnityEngine;

public class HealthTester : MonoBehaviour
{
    public PlayerHealth playerHealth;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            playerHealth.ChangeHealth(-1);
        }
    }
}