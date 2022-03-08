using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{

    public GameObject player;
    public bool canRun = true;


    void Update()
    {
        if (canRun && PlayerController.instance)
        {
            player = GameObject.Find("Player(Clone)");
            player.transform.position = this.transform.position;
            canRun = false;
        }
    }


}
