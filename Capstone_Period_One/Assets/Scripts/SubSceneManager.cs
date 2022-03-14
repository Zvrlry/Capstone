using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSceneManager : MonoBehaviour
{

    public Transform otherPosition;
    private PlayerController player;
    bool canRun = true;
    public void Update()
    {
        if (canRun && PlayerController.instance)
        {
            player = FindObjectOfType<PlayerController>();
            canRun = false;
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.transform.position = otherPosition.transform.position;
        }
    }


}
