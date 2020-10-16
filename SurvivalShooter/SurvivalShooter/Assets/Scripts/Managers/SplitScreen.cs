using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreen : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;
    Rect camView1;
    Rect camView2;
    GameObject player2;
    // Start is called before the first frame update
    void Start()
    {
        player2 = GameObject.Find("Player 2");
        player2.SetActive(false);


        camView1 = new Rect(0, 0, 0.5f, 1);
        camView2 = new Rect(0.5f, 0, 1, 1);

        cam1.rect = new Rect(0, 0, 1, 1);
        cam2.rect = camView2;
        cam2.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
    if(Input.GetKeyDown(KeyCode.Space) )
        {
            if (player2.activeInHierarchy == false)
            {
                player2.SetActive(true);
                cam2.gameObject.SetActive(true);
                cam1.rect = camView1;
            }
            else
            {
                player2.SetActive(false);
                cam2.gameObject.SetActive(false);
                cam1.rect = new Rect(0, 0, 1, 1);
            }
        }        
    }
}
