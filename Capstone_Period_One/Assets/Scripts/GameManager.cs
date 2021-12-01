using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public static GameManager instance;
    public CharStats[] playerStats;

    [Header("Bool's")]
    public bool gameMenuOpen;
    public bool dialogActive;
    public bool fadingBetweenAreas;

    [Header("Items")]
    public string[] itemsHeld;
    public int[] numberOfItems;
    public Item[] referenceItems;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMenuOpen || dialogActive || fadingBetweenAreas)
        {
            PlayerController.instance.canMove = false;
        }
        else
        {
            PlayerController.instance.canMove = true;
        }
    }

    public Item GetItemDetails(string itemToGrab)
    {

        // search for the item in array
        for (int i = 0; i < referenceItems.Length; i++)
        {
            if (referenceItems[i].itemName == itemToGrab)
            {
                // if found item then return the proper item
                return referenceItems[i];
            }
        }

        // if no item returned, return nothing at end of loop
        return null;
    }

    public void SortItems()
    {
        bool itemsAfterSpace = true;

        while (itemsAfterSpace)
        {
            itemsAfterSpace = false;
            for (int i = 0; i < itemsHeld.Length - 1; i++)
            {
                if (itemsHeld[i] == "")
                {
                    itemsHeld[i] = itemsHeld[i + 1];
                    itemsHeld[i + 1] = "";

                    numberOfItems[i] = numberOfItems[i + 1];
                    numberOfItems[i + 1] = 0;

                    if (itemsHeld[i] != "")
                    {
                        itemsAfterSpace = true;
                    }
                }
            }
        }
    }
}
