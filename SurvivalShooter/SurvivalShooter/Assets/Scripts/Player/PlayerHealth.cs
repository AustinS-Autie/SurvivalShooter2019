using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;

    public AudioSource lowHealth;
    
    [SerializeField]
    public AudioClip damageClip;                            //added variable, see PlayLowHealth()
    [SerializeField]
    public AudioClip deathClip;                             //moved variable 
    [SerializeField]
    public AudioClip lowHealthClip;                         //added variable

    [SerializeField]
    Camera mainCamera;                                      //added camera reference


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;

        lowHealth = GetComponent<AudioSource>();
    }


    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;

        PlayLowHealth();

        if(currentHealth<0 && mainCamera.orthographicSize>0.1)                     //added this condition for the camera
            mainCamera.orthographicSize -= 0.0035f;                                //and this command, zooms the camera in

    }

    public void PlayLowHealth()
    {


        if (currentHealth>0 && currentHealth < startingHealth / 4 && lowHealth.isPlaying==false)
        {
            if (lowHealth.clip != lowHealthClip)
                lowHealth.clip = lowHealthClip;

            lowHealth.Play();
        }
    }

    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        playerAudio.clip = damageClip;
        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();
        playerShooting.StopGunAudio();

        Time.timeScale = 0.5f;
        anim.SetTrigger ("Die");
       


        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;

    }


    public void RestartLevel ()
    {
        mainCamera.orthographicSize = 1f;
        Time.timeScale = 1f;
        SceneManager.LoadScene (0);
    }

    public float GetPlayerHealth()
    {
        return currentHealth;
    }
}
