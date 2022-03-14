using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStarter : MonoBehaviour
{
    public BattleType[] potentialBattles;
    private bool isArea;
    public float timeBetweenBattles;
    private float betweenBattleCounter;
    public bool activeOnEnter, activateOnStay, activateOnExit, deactivateAfterStarting;

    // Start is called before the first frame update
    void Start()
    {
        betweenBattleCounter = Random.Range(timeBetweenBattles * .5f, timeBetweenBattles * 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isArea && PlayerController.instance.canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                betweenBattleCounter -= Time.deltaTime;
            }

            if (betweenBattleCounter <= 0)
            {
                betweenBattleCounter = Random.Range(timeBetweenBattles * .5f, timeBetweenBattles * 1.5f);
                StartCoroutine(StartBattleCo());
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (activeOnEnter)
            {
                StartCoroutine(StartBattleCo());
            }
            else
            {
                isArea = true;
                GameMenu.instance.EnterBattleZone();
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (activateOnExit)
            {
                StartCoroutine(StartBattleCo());
            }
            else if(isArea == true)
            {
                GameMenu.instance.ExitBattleZone();
                isArea = false;
            }
            else
            {
                
               
                isArea = false;
               
            }
        }
    }

    public IEnumerator StartBattleCo()
    {
        GameManager.instance.battleActive = true;
        int selectedBattle = Random.Range(0, potentialBattles.Length);
        BattleManager.instance.rewardItems = potentialBattles[selectedBattle].rewardItems;
        BattleManager.instance.rewardXp = potentialBattles[selectedBattle].rewardXP;

        yield return new WaitForSeconds(.5f);

        // spawning enemies corresponding to the selected battle
        BattleManager.instance.BattleStart(potentialBattles[selectedBattle].enemies);

        if (deactivateAfterStarting)
        {
            gameObject.SetActive(false);
        }
    }
}
