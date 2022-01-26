using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    public static BattleManager instance;
    private bool battleActive;
    public GameObject battleScene;
    public Transform[] playerPositions;
    public Transform[] enemyPositions;
    public BattleChar[] playerPrefabs;
    public BattleChar[] enemyPrefabs;

    public List<BattleChar> activeBattlers = new List<BattleChar>();

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            BattleStart(new string[] { "Goblin", "Flower Snake", "Stinger", "Stinger", "Goblin" });
        }
    }

    public void BattleStart(string[] enemiesToSpawn)
    {

        var xCamPos = Camera.main.transform.position.x;
        var yCamPos = Camera.main.transform.position.y;
        var zCamPos = transform.position.z;

        if (!battleActive)
        {
            battleActive = true;
            GameManager.instance.battleActive = true;
            transform.position = new Vector3(xCamPos, yCamPos, zCamPos);
            battleScene.SetActive(true);

            for (int i = 0; i < playerPositions.Length; i++)
            {
                if (GameManager.instance.playerStats[i].gameObject.activeInHierarchy)
                {
                    for (int j = 0; j < playerPrefabs.Length; j++)
                    {
                        if (playerPrefabs[j].charName == GameManager.instance.playerStats[i].charName)
                        {
                            BattleChar newPlayer = Instantiate(playerPrefabs[j], playerPositions[i].position, playerPositions[i].rotation);
                            newPlayer.transform.parent = playerPositions[i];
                            activeBattlers.Add(newPlayer);

                            CharStats player = GameManager.instance.playerStats[i];
                            activeBattlers[i].currentHp = player.currentHP;
                            activeBattlers[i].maxHp = player.maxHP;
                            activeBattlers[i].maxHp = player.maxHP;
                            activeBattlers[i].currentMp = player.currentMP;
                            activeBattlers[i].maxMp = player.maxMP;
                            activeBattlers[i].strength = player.strength;
                            activeBattlers[i].defense = player.defence;
                            activeBattlers[i].weaponPower = player.weaponPower;
                            activeBattlers[i].armorPower = player.armorPower;
                        }
                    }
                }
            }

            for (int i = 0; i < enemiesToSpawn.Length; i++)
            {
                if (enemiesToSpawn[i] != "")
                {
                    for (int j = 0; j < enemyPrefabs.Length; j++)
                    {
                        if (enemyPrefabs[j].charName == enemiesToSpawn[i])
                        {
                            BattleChar newEnemy = Instantiate(enemyPrefabs[j], enemyPositions[i].position, enemyPositions[i].rotation);
                            newEnemy.transform.parent = enemyPositions[i];
                            activeBattlers.Add(newEnemy);
                        }
                    }
                }
            }
        }
    }
}
