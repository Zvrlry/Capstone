using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{

    [Header("Name and Experience")]
    public string charName;
    public int playerLevel = 1;
    public int currentEXP;
    public int[] expToNextLevel;
    public int maxLevel = 10000000;
    public int baseEXP = 1000;

    [Header("HP and MP")]
    public int currentHP;
    public int maxHP = 100;
    public int[] mpLvlBonus;
    public int currentMP;
    public int maxMP;

    [Header("Strength, Defence, Weapon, and Armor")]
    public int strength;
    public int defence;
    public int weaponPower;
    public int armorPower;
    public string equippedWeapon;
    public string equippedArmor;
    public Sprite charImage;

    // Start is called before the first frame update
    void Start()
    {
        expToNextLevel = new int[maxLevel]; // setting the index of exp to next level to our max level
        expToNextLevel[1] = baseEXP;

        for (int i = 2; i < expToNextLevel.Length; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.70f); // multiplying each level by 1.75
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddExp(int expToAdd)
    {
        currentEXP += expToAdd;

        if (playerLevel < maxLevel)
        {
            if (currentEXP > expToNextLevel[playerLevel])
            {
                currentEXP -= expToNextLevel[playerLevel]; // subtracting current exp

                playerLevel++; // adding one more player level

                // determine whether to add to strength or defence based on odd or even
                if (playerLevel % 2 == 0)
                {
                    strength++; // even
                }
                else
                {
                    defence++; // odd
                }

                maxHP = Mathf.FloorToInt(maxHP * 1.1f);
                currentHP = maxHP;

                maxMP += mpLvlBonus[playerLevel];
                currentMP = maxMP;
            }
        }
        if (playerLevel >= maxLevel)
        {
            currentEXP = 0;
        }
    }
}
