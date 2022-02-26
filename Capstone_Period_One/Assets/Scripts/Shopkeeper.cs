using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : MonoBehaviour
{

    private bool canOpen;     

    public string[] ItemsForSale = new string[40];

    public GameObject interactText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canOpen && Input.GetButtonDown("Fire1") && PlayerController.instance.canMove && !Shop.instance.shopMenu.activeInHierarchy)
        {
            interactText.SetActive(false);
            Shop.instance.itemsForSale = ItemsForSale; 

            Shop.instance.OpenShop();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            interactText.SetActive(true);
            canOpen = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            interactText.SetActive(false);
            canOpen = false; 
        }
    }
}
