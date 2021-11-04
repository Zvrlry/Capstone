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
            BattleStart(new string[] { "Goblin", "Stinger" });
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
            transform.position = new Vector3(xCamPos, yCamPos, zCamPos);
            battleScene.SetActive(true);

            for (int i = 0; i < playerPositions.Length; i++)
            {
                /*
                if (GameManager.instance.playerStat[i].gameObject.activeInHierarchy)
                {
                    for(int j = 0; j < playerPrefabs.length; j++)
                    {
                        if(playerPrefabs[j].charName == GameManager.instance.playerStats[i].charName)
                        {
                            BattleChar.newPlayer = Instantiate(playerPrefabs[j], playerPositions[i].position, playerPositions[i].rotation);
                            newPlayer.transform.parent = playerPositions[i];
                        }
                    }
                }
                */
            }
        }
    }
}
