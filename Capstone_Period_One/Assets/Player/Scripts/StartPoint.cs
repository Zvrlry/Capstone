using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{

    public GameObject player;

    void Start()
    {
        player.transform.position = this.transform.position;
    }


}
