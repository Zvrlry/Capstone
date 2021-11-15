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

    // Start is called before the first frame update
    void Start()
    {
        OpenStatus(); // Load Game with stats
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
    }

    public void CloseMenu()
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }

        theMenu.SetActive(false);

        GameManager.instance.gameMenuOpen = false;
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
        StatusMP.text = "MP: " + playerStats[selected].currentHP + "/" + playerStats[selected].maxMP;
        statusStr.text = "Strength: " + playerStats[selected].strength.ToString();
        StatusDef.text = "Defense: " + playerStats[selected].defence.ToString();
        if (playerStats[selected].equippedWeapon != "")
        {
            statusWpnEpn.text = playerStats[selected].equippedWeapon;
        }
        statusWpnPwr.text = "Weapon Power: " + playerStats[selected].weaponPower.ToString();
        if (playerStats[selected].equippedArmor != "")
        {
            statusArmrEqpd.text = "Equipped Armor" + playerStats[selected].equippedArmor;
        }
        statusArmrPwr.text = "Armor Power: " + playerStats[selected].armor.ToString();
        statusExp.text = "Exp To Next Level: " + (playerStats[selected].expToNextLevel[playerStats[selected].playerLevel] - playerStats[selected].currentEXP).ToString();
        statusImage.sprite = playerStats[selected].charImage;
    }

}
