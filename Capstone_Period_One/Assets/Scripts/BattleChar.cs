using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleChar : MonoBehaviour
{
    public bool isPlayer;
    public string[] movesAvailable;
    public string charName;
    public int currentHp, maxHp, currentMp, maxMp, strength, defense, weaponPower, armorPower;
    public bool hasDied;
}
