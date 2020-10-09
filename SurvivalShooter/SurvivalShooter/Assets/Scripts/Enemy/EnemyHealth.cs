using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;

    public float maxDamageTime = 100f;      //added variable, see FlashOnDamage()
    public float damageTime = 0;            //added variable
    public int modelToggle;                 //added variable

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;

    [SerializeField]                    
    Image healthBar;                        //added healthbar veriable


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
        
        
    }

    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }

        FlashOnDamage();
        UpdateHUD();
    }

    void UpdateHUD()
    {
        healthBar.fillAmount = (float)(currentHealth / (startingHealth * 1.0f) );
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead)
            return;

        enemyAudio.Play ();

        currentHealth -= amount;
            
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        damageTime = maxDamageTime;

        if(currentHealth <= 0)
        {
            Death ();
        }
    }

    void FlashOnDamage()
    {
        if (transform.localScale.x != 1)
            transform.localScale = new Vector3(1, 1, 1);

        if (damageTime > 0)
        {
            damageTime -= 1;

            if (modelToggle == 1)
            {
                transform.localScale = new Vector3(0, 0, 0);
                modelToggle = 0;
            }
            else
            {
                modelToggle = 1;
            }


        }


    }

    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }


    public void StartSinking ()
    {
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }
}
