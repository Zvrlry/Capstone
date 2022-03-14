using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbScript : MonoBehaviour
{
    public AreaExit areaExit;
    
    public void Start()
    {
        areaExit = GameObject.Find("Area Exit").GetComponent<AreaExit>();
    }

   public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            areaExit.orbCollected += 1;
            Destroy(this.gameObject);
        }
    }
}
