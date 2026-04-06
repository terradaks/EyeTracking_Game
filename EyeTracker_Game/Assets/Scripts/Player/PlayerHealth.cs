using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 4;
    int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(int amount) //made public for testing purposes
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyLaser"))
        {
            ChangeHealth(-1);
            Destroy(other.gameObject);
        }
    }
    void GameOver()
    {
        Debug.Log("Game Over!");
        // SceneManager.LoadScene("GameOver");
    }
}
