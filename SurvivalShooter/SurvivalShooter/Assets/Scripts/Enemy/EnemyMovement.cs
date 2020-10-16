using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;
    GameObject target;


    void Awake ()
    {
        target = GameObject.Find("Player 1");

        if (GameObject.Find("Player 2") != null && Mathf.Round(Random.value) == 1)
            target = GameObject.Find("Player 2");

        player = target.transform;
        playerHealth = player.GetComponent<PlayerHealth>();

        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }


    void Update ()
    {
        if (target.GetComponent<PlayerHealth>().currentHealth<=0)
            ResetTarget();

        
        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination (player.position);
        }
        else
        {
            nav.enabled = false;
        }
    }

    void ResetTarget()
    {
        if (GameObject.Find("Player 2") != null)
        {
            if (target.name=="Player 2")
                target = GameObject.Find("Player 1");
            else
                target = GameObject.Find("Player 2");
        }



        player = target.transform;
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    public GameObject GetTarget()
    {
        return target;
    }
}
