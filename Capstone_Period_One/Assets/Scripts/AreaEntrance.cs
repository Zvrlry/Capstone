using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour {

    public string transitionName;
	bool canRun = true;


	void Start()
	{
		GameManager.instance.canSpawn = true;
	}
	// Update is called once per frame
	void Update () {
		//This if Statement allows time for everything to load in without wasting to much power
		if (canRun && PlayerController.instance)
		{
			if (GameManager.instance.isLoad == false)
			{
				if (GameManager.instance.canSpawn == true)
				{
					PlayerController.instance.transform.position = transform.position;
					GameManager.instance.fadingBetweenAreas = false;
					canRun = false;
				}
			}
		}
	}
}


// Contains If Statement for later use
//	if (transitionName == PlayerController.instance.areaTransitionName){}