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
        }
    }
}
