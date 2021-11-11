using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSceneManager : MonoBehaviour
{

    public Transform otherPosition;
    private PlayerController player;

    public void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.transform.position = otherPosition.transform.position;
        }
    }


}
