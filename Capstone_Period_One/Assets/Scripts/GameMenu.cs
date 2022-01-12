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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if(theMenu.activeInHierarchy)
            {
                // theMenu.SetActive(false);
                //GameManager.instance.gameMenuOpen = false;

                CloseMenu();
            } else
            {
                theMenu.SetActive(true);
                UpdateMainStats();
                GameManager.instance.gameMenuOpen = true;
            }
        }
    }

    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.playerStats;

        for(int i = 0; i < playerStats.Length; i++)
        {
            if(playerStats[i].gameObject.activeInHierarchy)
            {
                charStatHolder[i].SetActive(true);

                nameTest[i].text = playerStats[i].charName;
                hptext[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
                mpText[i].text = "MP: " + playerStats[i].currentMP + "/" + playerStats[i].maxMP;
                lvltext[i].text = "Lvl: " + playerStats[i].playerLevel;
                expText[i].text = "" + playerStats[i].currentEXP + "/" + playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].value = playerStats[i].currentEXP;
                charImage[i].sprite = playerStats[i].charImage;
            } else{
                charStatHolder[i].SetActive(false);
            }
        }
    }
    public void ToggleWindow(int windowNumber)
    {
        UpdateMainStats();

        for(int i = 0; i < windows.Length; i++)
        {
            if(i == windowNumber)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            } else
            {
                windows[i].SetActive(false);
            }
        }
    }

    public void CloseMenu()
    {
        for(int i = 0; i < windows.Length; i++)
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

        for(int i = 0; i < statusButton.Length; i++)
        {
            statusButton[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            statusButton[i].GetComponentInChildren<Text>().text = playerStats[i].charName;
        } 
    }

    public void StatusChar(int selected)
    {
        statusName.text = playerStats[selected].charName;
        StatusHP.text = "" + playerStats[selected].currentHP + "/" + playerStats[selected].maxHP;
        StatusMP.text = "" + playerStats[selected].currentHP + "/" + playerStats[selected].maxMP;
        statusStr.text = playerStats[selected].strength.ToString();
        StatusDef.text = playerStats[selected].defence.ToString();
        if (playerStats[selected].equippedWeapon != "")
        {
            statusWpnEpn.text = playerStats[selected].equippedWeapon;
        }
        statusWpnPwr.text = playerStats[selected].weaponPower.ToString();
        if (playerStats[selected].equippedArmor != "")
        {
            statusArmrEqpd.text = playerStats[selected].equippedArmor;
        }
        statusArmrEqpd.text = playerStats[selected].armor.ToString();
        statusExp.text = (playerStats[selected].expToNextLevel[playerStats[selected].playerLevel] - playerStats[selected].currentEXP).ToString();
        statusImage.sprite = playerStats[selected].charImage;
    }

}
