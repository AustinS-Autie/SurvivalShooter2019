using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public PlayerHealth playerHealth2;
    public float restartDelay;



    Animator anim;
	float restartTimer;

    PlayerHealth defeatedPlayer;
    PlayerHealth defeatedPlayer2;


    void Awake()
    {
        anim = GetComponent<Animator>();
        restartDelay = 7f;
        defeatedPlayer = playerHealth;
        defeatedPlayer2 = playerHealth2;
    }


    void Update()
    {

        if (Time.timeScale < 1)
            Time.timeScale += Time.deltaTime / 10;


        if (playerHealth.currentHealth <= 0f &&
            (GameObject.Find("Player 2") == null || playerHealth2.currentHealth <= 0f))
        {
            anim.SetTrigger("GameOver");
            anim.speed = 0.8f;

            restartTimer += Time.deltaTime;

            if (restartTimer >= restartDelay)
            {
                SceneManager.LoadScene(0);
                Time.timeScale = 1;
            }
                
        }
    }
}
