using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{

    GameObject battleScene;

    public GameObject battleMusic;
    public GameObject normalMusic;

    // Start is called before the first frame update
    void Start()
    {
      
       
        battleMusic.SetActive(false);
        normalMusic.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        battleScene = GameObject.Find("BattleScene");

        if (battleScene != null)
        {
            
            battleMusic.SetActive(true);
            normalMusic.SetActive(false);
        }

        if (battleScene == null)
        {
            battleMusic.SetActive(false);
            normalMusic.SetActive(true);
        }
    }
}
