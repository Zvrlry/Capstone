using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    bool canRun = true;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    void Update()
    {
        if (canRun && PlayerController.instance)
        {
            target = FindObjectOfType<PlayerController>().transform;
            canRun = false;
        }
    }

    // LateUpdate is called once per frame after update
    void LateUpdate()
    {
        if (target)
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
        else
        {
            Debug.Log("Can't Find Target");
        }
    }
}
