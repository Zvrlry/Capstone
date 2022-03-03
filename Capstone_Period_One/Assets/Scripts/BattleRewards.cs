using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleRewards : MonoBehaviour
{
    public static BattleRewards instance;
    public Text xpTxt, itemTxt;
    public GameObject rewardsScreen;
    public string[] rewardItems;
    public int xpEarned;

    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {   // Testing
        //   if (Input.GetKeyDown(KeyCode.Y))
        //   {
        //       OpenRewardScreen(50, new string[] { "Iron Sword", "Iron Armor" });
        //  }
    }

    public void OpenRewardScreen(int xp, string[] rewards)
    {
        xpEarned = xp;
        rewardItems = rewards;

        xpTxt.text = "Everyone earned " + xp + " xp!";
        itemTxt.text = "";

        for (int i = 0; i < rewardItems.Length; i++)
        {
            itemTxt.text += rewardItems[i] + "\n";
        }

        rewardsScreen.SetActive(true);
    }

    public void CloseRewardScreen()
    {
        for (int i = 0; i < GameManager.instance.playerStats.Length; i++)
        {
            if (GameManager.instance.playerStats[i].gameObject.activeInHierarchy)
            {
                GameManager.instance.playerStats[i].AddExp(xpEarned);
            }
        }

        for (int i = 0; i < rewardItems.Length; i++)
        {
            GameManager.instance.AddItem(rewardItems[i]);
        }

        rewardsScreen.SetActive(false);
        GameManager.instance.battleActive = false;
    }
}
