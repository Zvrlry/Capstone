using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{

    public GameObject player;
    public GameObject UIScreen;
    public GameObject gameManager;
    public GameObject battleManager;



    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.instance == null)
        {
            PlayerController.instance = Instantiate(player).GetComponent<PlayerController>();
        }

        if (GameManager.instance == null)
        {
            GameManager.instance = Instantiate(gameManager).GetComponent<GameManager>();
        }

        if (BattleManager.instance == null)
        {
            BattleManager.instance = Instantiate(battleManager).GetComponent<BattleManager>();
        }

        if (GameMenu.instance == null)
        {
            GameMenu.instance = Instantiate(UIScreen).GetComponent<GameMenu>();
        }
    }
}
