using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject theMenu;
    public GameObject[] windows;

    [SerializeField] private CharStats[] playerStats;

    public Text[] nameTest, hptext, mpText, lvltext, expText;
    public Slider[] expSlider;
    public Image[] charImage;
    public GameObject[] charStatHolder;
    public GameObject[] statusButton;

    public Text statusName, StatusHP, StatusMP, statusStr, StatusDef, statusWpnEpn, statusWpnPwr, statusArmrEqpd, statusArmrPwr, statusExp;
    public Image statusImage;

    [Header("Items")]
    public ItemButton[] itemButtons;
    public string selectedItem;
    public Item activeItem;
    public Text itemName, itemDescription, useButtonText;
    [HideInInspector] public static GameMenu instance;

    public GameObject itemCharChoiceMenu;
    public Text[] itemCharChoiceNames;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        // Load Game with stats
        OpenStatus();
        ShowItems();
    }

    // Update is called once per frame
    void Update()
    {
        //if escape clicked
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (theMenu.activeInHierarchy) // if the menu is open in hierarchy
            {
                CloseMenu(); // close the menu
            }
            else if (!theMenu.activeInHierarchy) // if the menu isnt active in hierarchy
            {
                theMenu.SetActive(true); // open menu
                UpdateMainStats(); // update the stats of the player
                GameManager.instance.gameMenuOpen = true; // set bool for menu true in game manager
            }
        }
    }

    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.playerStats; // player stats script

        for (int i = 0; i < playerStats.Length; i++)
        {
            // if index of player is active in hierarchy
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                // set active the index of the char info game object
                charStatHolder[i].SetActive(true);

                // setting UI text
                nameTest[i].text = "Name: " + playerStats[i].charName;
                hptext[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
                mpText[i].text = "MP: " + playerStats[i].currentMP + "/" + playerStats[i].maxMP;
                lvltext[i].text = "Lvl: " + playerStats[i].playerLevel;
                expText[i].text = "" + playerStats[i].currentEXP + "/" + playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].value = playerStats[i].currentEXP;
                charImage[i].sprite = playerStats[i].charImage;
            }
            else
            {
                // unset active the index of the char info game object 
                charStatHolder[i].SetActive(false);
            }
        }
    }
    public void ToggleWindow(int windowNumber)
    {
        UpdateMainStats();

        for (int i = 0; i < windows.Length; i++)
        {
            // if the window is equal to i; for example, the stats window is = to 0
            if (i == windowNumber)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            }
            else
            {
                windows[i].SetActive(false);
            }
        }

        itemCharChoiceMenu.SetActive(false);
    }

    public void CloseMenu()
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }

        theMenu.SetActive(false);

        GameManager.instance.gameMenuOpen = false;

        itemCharChoiceMenu.SetActive(false);
    }

    public void OpenStatus()
    {
        UpdateMainStats();

        StatusChar(0);

        // setting players name to the buttons in order
        for (int i = 0; i < statusButton.Length; i++)
        {
            statusButton[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            statusButton[i].GetComponentInChildren<Text>().text = playerStats[i].charName;
        }
    }

    // setting the main screen with the stats of the char
    public void StatusChar(int selected)
    {
        statusName.text = "Name: " + playerStats[selected].charName;
        StatusHP.text = "HP: " + playerStats[selected].currentHP + "/" + playerStats[selected].maxHP;
        StatusMP.text = "MP: " + playerStats[selected].currentMP + "/" + playerStats[selected].maxMP;
        statusStr.text = "Strength: " + playerStats[selected].strength.ToString();
        StatusDef.text = "Defense: " + playerStats[selected].defence.ToString();
        if (playerStats[selected].equippedWeapon != "")
        {
            statusWpnEpn.text = "Equipped Weapon: " + playerStats[selected].equippedWeapon;
        }
        statusWpnPwr.text = "Weapon Power: " + playerStats[selected].weaponPower.ToString();
        if (playerStats[selected].equippedArmor != "")
        {
            statusArmrEqpd.text = "Equipped Armor: " + playerStats[selected].equippedArmor;
        }
        statusArmrPwr.text = "Armor Power: " + playerStats[selected].armorPower.ToString();
        statusExp.text = "Exp To Next Level: " + (playerStats[selected].expToNextLevel[playerStats[selected].playerLevel] - playerStats[selected].currentEXP).ToString();
        statusImage.sprite = playerStats[selected].charImage;
    }


    public void ShowItems()
    {
        GameManager.instance.SortItems();

        // setting value of button to its value
        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i;

            // if there is a name
            if (GameManager.instance.itemsHeld[i] != "")
            {
                // setting image
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                itemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }

    public void SelectItem(Item newItem)
    {
        activeItem = newItem;

        if (activeItem.isItem)
        {
            useButtonText.text = "Use";
        }
        else if (activeItem.isWeapon || activeItem.isArmour)
        {
            useButtonText.text = "Equip";
        }

        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.description;
    }


    public void DiscardItem()
    {
        if (activeItem != null)
        {
            GameManager.instance.RemoveItem(activeItem.itemName);
        }
    }

    public void OpenItemCharChoice()
    {
        itemCharChoiceMenu.SetActive(true);

        for (int i = 0; i < itemCharChoiceNames.Length; i++)
        {
            itemCharChoiceNames[i].text = GameManager.instance.playerStats[i].charName;
            itemCharChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.instance.playerStats[i].gameObject.activeInHierarchy);
        }
    }

    public void CloseItemCharChoice()
    {
        itemCharChoiceMenu.SetActive(false);
    }

    public void UseItem(int selectChar)
    {
        activeItem.Use(selectChar);
        CloseItemCharChoice();
    }
}
