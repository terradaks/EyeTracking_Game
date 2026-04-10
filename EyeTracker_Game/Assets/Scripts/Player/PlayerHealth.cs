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
    void Update() //Restart Game this is temporary, as we may want a button instead
    {
        if (Input.GetKeyDown(KeyCode.R) && currentHealth <= 0)
        {
            Restart();
        }
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
    void Restart()//temporary 
    {
        Time.timeScale = 1f; // Unpauses the game when restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //void OnTriggerEnter(Collider other) Commented out as we dont have a laser prefab, Health is currently controlled by EnemyController.cs
    //{
    //    if (other.CompareTag("EnemyLaser"))
    //    {                  
    //        ChangeHealth(-1);
    //        Destroy(other.gameObject);
    //    }
    //}

    void GameOver() // Uncomment when we have a game over screen, currently just logs to console and pauses the game
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0f; // Pauses everything when death is reached

        // gameOverScreen.SetActive(true); // two options, either an object or a scene
        // SceneManager.LoadScene("GameOver"); // Loads the game over scene, make sure to create one and name it "GameOver", may be better to transition to a start screen if needed
    }
}
