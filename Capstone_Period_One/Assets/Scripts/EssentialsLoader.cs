using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{

    public GameObject player;
    public GameObject UIScreen;
    public GameObject gameManager;



    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log(PlayerController.instance);

        if (!PlayerController.instance)
        {
            PlayerController.instance = Instantiate(player).GetComponent<PlayerController>();
            PlayerController.instance.transform.position = this.transform.position;
        }

        if (!GameMenu.instance)
        {
            GameMenu.instance = Instantiate(UIScreen).GetComponent<GameMenu>();
        }

        if (GameManager.instance == null)
        {
            GameManager.instance = Instantiate(gameManager).GetComponent<GameManager>();
        }

        // if (BattleManager.instance == null)
        // {
        //     BattleManager.instance = Instantiate(battleManager).GetComponent<BattleManager>();
        // }


    }
}
