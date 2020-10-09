using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float restartDelay;


    Animator anim;
	float restartTimer;


    void Awake()
    {
        anim = GetComponent<Animator>();
        restartDelay = 20f;
    }


    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
            anim.speed = 0.55f;          //added this line

            restartTimer += Time.deltaTime;

			if (restartTimer >= restartDelay + 1) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                restartTimer = 0;
            }
        }
    }
}
