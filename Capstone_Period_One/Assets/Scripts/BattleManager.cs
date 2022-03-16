using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{

    public static BattleManager instance;
    public bool battleActive;
    public GameObject battleScene;
    public Transform[] playerPositions;
    public Transform[] enemyPositions;
    public BattleChar[] playerPrefabs;
    public BattleChar[] enemyPrefabs;

    public List<BattleChar> activeBattlers = new List<BattleChar>();

    public int currentTurn;
    public bool turnWaiting;
    public GameObject uiButtonsHolder;
    public BattleMove[] movesList;
    public GameObject enemyAttackEffect;
    public GameObject playerAttackEffect;
    public DamageNumber damageNumber;

    [Header("UI Elements")]
    public Text[] playerName;
    public Text[] playerHP;
    public Text[] playerMP;

    [Header("Target Menu")]
    public GameObject targetMenu;
    public BattleTargetButton[] targetButtons;

    [Header("Magic Menu")]
    public GameObject magicMenu;
    public BattleMagicSelect[] magicButtons;

    [Header("Battle Notice")]
    public BattleNotification battleNotice;

    [Header("Flee")]
    public int chanceToFlee = 35;
    private bool isFleeing;

    [Header("Scene Transition")]
    public string gameOverScene;

    [Header("Rewards")]
    public int rewardXp;
    public string[] rewardItems;

    [Header("Player Turns")]
    public GameObject arrowOne;
    public GameObject arrowTwo;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        // DontDestroyOnLoad(gameObject);
        arrowTwo.SetActive(false);
        arrowOne.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Test Battle
        // if (Input.GetKeyDown(KeyCode.T))
        // {
        //     BattleStart(new string[] { "Goblin", "Flower Snake", "Stinger" });
        // }

        if (battleActive)
        {
            if (turnWaiting)
            {
                if (activeBattlers[currentTurn].isPlayer)
                {
                    uiButtonsHolder.SetActive(true);
                }
                else
                {
                    uiButtonsHolder.SetActive(false);
                    // enemy should attack
                    StartCoroutine(EnemyMoveCo());
                }
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                NextTurn();
            }
        }


        if(currentTurn == 0)
        {
            arrowOne.SetActive(true);
            arrowTwo.SetActive(false);
        }
        else if (currentTurn == 1)
        {
            arrowOne.SetActive(false);
            arrowTwo.SetActive(true);
        }
        else
        {
            arrowOne.SetActive(false);
            arrowTwo.SetActive(false);
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
            turnWaiting = true;
            currentTurn = Random.Range(0, activeBattlers.Count);

            UpdateUIStats();
        }
    }


    public void NextTurn()
    {
        currentTurn++;
        if (currentTurn >= activeBattlers.Count)
        {
            currentTurn = 0;
        }

        turnWaiting = true;
        UpdateBattle();
        UpdateUIStats();
    }

    public void UpdateBattle()
    {
        bool allEnemiesDead = true;
        bool allPlayersDead = true;

        for (int i = 0; i < activeBattlers.Count; i++)
        {
            if (activeBattlers[i].currentHp < 0)
            {
                activeBattlers[i].currentHp = 0;
            }

            if (activeBattlers[i].currentHp == 0)
            {
                // handle dead battler'
                if (activeBattlers[i].isPlayer)
                {
                    // setting dead sprite
                    activeBattlers[i].theSprite.sprite = activeBattlers[i].deadSprite;
                }
                else
                {
                    activeBattlers[i].EnemyFade();
                }
            }
            else
            {
                if (activeBattlers[i].isPlayer)
                {
                    allPlayersDead = false;
                    activeBattlers[i].theSprite.sprite = activeBattlers[i].aliveSprite;
                }
                else
                {
                    allEnemiesDead = false;
                }
            }
        }

        if (allEnemiesDead || allPlayersDead)
        {
            if (allEnemiesDead)
            {
                // end battle in victory
                StartCoroutine(EndBattleCo());
            }
            else
            {
                // end battle in defeat
                StartCoroutine(GameOverCo());
            }

            // battleScene.SetActive(false);
            // GameManager.instance.battleActive = false;
            // battleActive = false;
        }
        else
        {
            while (activeBattlers[currentTurn].currentHp == 0)
            {
                currentTurn++;
                if (currentTurn >= activeBattlers.Count)
                {
                    currentTurn = 0;
                }
            }
        }
    }

    public IEnumerator EnemyMoveCo()
    {
        turnWaiting = false;
        yield return new WaitForSeconds(1f);
        EnemyAttack();
        yield return new WaitForSeconds(1f);
        NextTurn();
    }

    public void EnemyAttack()
    {
        List<int> players = new List<int>();
        for (int i = 0; i < activeBattlers.Count; i++)
        {
            // if is player and current player has health above 0
            if (activeBattlers[i].isPlayer && activeBattlers[i].currentHp > 0)
            {
                players.Add(i);
            }
        }

        int selectedTarget = players[Random.Range(0, players.Count)];

        // deal damage
        int selectAttack = Random.Range(0, activeBattlers[currentTurn].movesAvailable.Length);
        int movePower = 0;

        for (int i = 0; i < movesList.Length; i++)
        {
            if (movesList[i].moveName == activeBattlers[currentTurn].movesAvailable[selectAttack])
            {
                // init effect at transform of enemy
                Instantiate(movesList[i].theEffect, activeBattlers[selectedTarget].transform.position, activeBattlers[selectedTarget].transform.rotation);
                movePower = movesList[i].movePower;
            }
        }

        // spawning particle system indicator at enemy position 
        Instantiate(enemyAttackEffect, activeBattlers[currentTurn].transform.position, activeBattlers[currentTurn].transform.rotation);

        DealDamage(selectedTarget, movePower);
    }

    public void DealDamage(int target, int movePower)
    {
        float attackPower = activeBattlers[currentTurn].strength + activeBattlers[currentTurn].weaponPower;
        float defensePower = activeBattlers[target].defense + activeBattlers[target].armorPower;

        float damageCalc = ((attackPower / defensePower) * movePower * Random.Range(.9f, 1.1f));
        int damageToGive = Mathf.RoundToInt(damageCalc);

        Debug.Log(activeBattlers[currentTurn].charName + " is dealing " + damageCalc + " (" + damageToGive + ") damage to " + activeBattlers[target].charName);
        activeBattlers[target].currentHp -= damageToGive;

        Instantiate(damageNumber, activeBattlers[target].transform.position, activeBattlers[target].transform.rotation).SetDamage(damageToGive);

        UpdateUIStats();
    }

    public void UpdateUIStats()
    {
        for (int i = 0; i < playerName.Length; i++)
        {
            if (activeBattlers.Count > i)
            {
                if (activeBattlers[i].isPlayer)
                {
                    BattleChar playerData = activeBattlers[i];

                    playerName[i].gameObject.SetActive(true);
                    // setting proper text strings
                    playerName[i].text = playerData.charName;
                    playerHP[i].text = Mathf.Clamp(playerData.currentHp, 0, int.MaxValue) + "/" + playerData.maxHp;
                    playerMP[i].text = Mathf.Clamp(playerData.currentMp, 0, int.MaxValue) + "/" + playerData.maxMp;
                }
                else
                {
                    playerName[i].gameObject.SetActive(false);
                }
            }
            else
            {
                playerName[i].gameObject.SetActive(true);
            }
        }
    }

    public void PlayerAttack(string moveName, int selectedTarget)
    {
        int movePower = 0;
        for (int i = 0; i < movesList.Length; i++)
        {
            if (movesList[i].moveName == moveName)
            {
                // init effect at transform of enemy
                Instantiate(movesList[i].theEffect, activeBattlers[selectedTarget].transform.position, activeBattlers[selectedTarget].transform.rotation);
                movePower = movesList[i].movePower;
            }
        }

        Instantiate(playerAttackEffect, activeBattlers[currentTurn].transform.position, activeBattlers[currentTurn].transform.rotation);
        DealDamage(selectedTarget, movePower);
        NextTurn();
        uiButtonsHolder.SetActive(false);
        targetMenu.SetActive(false);
    }

    public void OpenTargetMenu(string moveName)
    {
        targetMenu.SetActive(true);

        List<int> Enemies = new List<int>();

        for (int i = 0; i < activeBattlers.Count; i++)
        {
            // if activeBattler is a enemy
            if (!activeBattlers[i].isPlayer)
            {
                Enemies.Add(i);
            }
        }

        for (int i = 0; i < targetButtons.Length; i++)
        {
            if (i < Enemies.Count && activeBattlers[Enemies[i]].currentHp > 0)
            {
                targetButtons[i].gameObject.SetActive(true);
                targetButtons[i].moveName = moveName;
                targetButtons[i].activeBattlerTarget = Enemies[i];
                targetButtons[i].targetName.text = activeBattlers[Enemies[i]].charName;
            }
            else
            {
                targetButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void OpenMagicMenu()
    {
        magicMenu.SetActive(true);

        for (int i = 0; i < magicButtons.Length; i++)
        {
            if (activeBattlers[currentTurn].movesAvailable.Length > i)
            {
                magicButtons[i].gameObject.SetActive(true);

                magicButtons[i].spellName = activeBattlers[currentTurn].movesAvailable[i];
                magicButtons[i].nameText.text = magicButtons[i].spellName;

                for (int j = 0; j < movesList.Length; j++)
                {
                    if (movesList[j].moveName == magicButtons[i].spellName)
                    {
                        magicButtons[i].spellCost = movesList[j].moveCost;
                        magicButtons[i].costText.text = magicButtons[i].spellCost.ToString();
                    }
                }
            }
            else
            {
                magicButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void Flee()
    {
        int fleeSuccess = Random.Range(0, 100);

        if (fleeSuccess < chanceToFlee)
        {
            isFleeing = true;
            StartCoroutine(EndBattleCo());
        }
        else
        {
            NextTurn();
            battleNotice.theText.text = "Couldn't escape!";
            battleNotice.Activate();
        }
    }

    public IEnumerator EndBattleCo()
    {
        // end battle
        GameMenu.instance.canOpen = true;
        battleActive = false;
        uiButtonsHolder.SetActive(false);
        targetMenu.SetActive(false);
        magicMenu.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < activeBattlers.Count; i++)
        {
            // if activeBattler is a player
            if (activeBattlers[i].isPlayer)
            {
                for (int j = 0; j < GameManager.instance.playerStats.Length; j++)
                {
                    if (activeBattlers[i].charName == GameManager.instance.playerStats[j].charName)
                    {
                        GameManager.instance.playerStats[j].currentHP = activeBattlers[i].currentHp;
                        GameManager.instance.playerStats[j].currentMP = activeBattlers[i].currentMp;
                    }
                }
            }

            Destroy(activeBattlers[i].gameObject);
        }
        battleScene.SetActive(false);
        activeBattlers.Clear();
        currentTurn = 0;
        if (isFleeing)
        {
            GameManager.instance.battleActive = false;
            isFleeing = false;
        }
        else
        {
            BattleRewards.instance.OpenRewardScreen(rewardXp, rewardItems);
        }
    }

    public IEnumerator GameOverCo()
    {
        // end battle
        battleActive = false;
        GameManager.instance.battleActive = false;
        yield return new WaitForSeconds(1f);
        battleScene.SetActive(false);
        activeBattlers.Clear();
        SceneManager.LoadScene(gameOverScene);
    }
}
