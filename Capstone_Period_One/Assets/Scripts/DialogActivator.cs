using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{

    public string[] lines;

    private bool canActivate;

    public bool isPerson = true;

    public bool shouldActivateQuest;
    public string questToMark;
    public bool markComplete;
    public GameObject uiCanvas;
    public GameObject interactText;
    [HideInInspector]
    public bool canRun = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canActivate && Input.GetButtonDown("Fire1") && !DialogManager.instance.dialogBox.activeInHierarchy)
        {
            interactText.SetActive(false);
            DialogManager.instance.ShowDialog(lines, isPerson);
            DialogManager.instance.ShouldActivateQuestAtEnd(questToMark, markComplete);
        }

        if(canRun && GameMenu.instance)
        {
            InteractTextSet();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            interactText.SetActive(true);
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            interactText.SetActive(false);
            canActivate = false;
        }
    }
    public void InteractTextSet()
    {
        uiCanvas = GameObject.Find("UI Canvas(Clone)");
        interactText = uiCanvas.transform.GetChild(0).gameObject;
        canRun = false;
    }

}
