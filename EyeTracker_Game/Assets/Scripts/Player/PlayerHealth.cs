using UnityEngine;
using UnityEngine.SceneManagement; //Added for scene management, currently commented out as we dont have a game over scene yet
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 4;
    int currentHealth;
    public GameObject gameOverScreen;
    public CameraShake cameraShake;
    public DamageOverlay damageOverlay;

    void Start()
    {
        currentHealth = maxHealth;
    }
   

    public void ChangeHealth(int amount) //made public for testing purposes
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);

        if (amount < 0) // player took damage
        {
            if (cameraShake != null)
                cameraShake.Shake();

            if (damageOverlay != null)
                damageOverlay.Flash();
        }

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }
    public void Restart()
    {
        Time.timeScale = 1f; // Unpauses the game when restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0f; // Pauses everything when death is reached

        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
    }
}
