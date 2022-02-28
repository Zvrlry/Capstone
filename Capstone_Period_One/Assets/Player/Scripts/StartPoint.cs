using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{

    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");

        player.transform.position = this.transform.position;
    }

  
}
