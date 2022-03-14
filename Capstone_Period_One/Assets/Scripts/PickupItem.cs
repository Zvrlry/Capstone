using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private bool canPickup;



    [HideInInspector]
    public GameObject uiCanvas;
    public GameObject interactText;

    public bool canRun = true;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canPickup && Input.GetButtonDown("Fire1") && PlayerController.instance.canMove)
        {
            GameManager.instance.AddItem(GetComponent<Item>().itemName);
            Destroy(gameObject);
        }


        if (canRun && GameMenu.instance)
        {
            InteractText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            interactText.SetActive(true);
            canPickup = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            interactText.SetActive(false);
            canPickup = false;
        }
    }

    public void InteractText()
    {
        uiCanvas = GameObject.Find("UI Canvas(Clone)");
        interactText = uiCanvas.transform.GetChild(0).gameObject;
        canRun = false;
    }
}
